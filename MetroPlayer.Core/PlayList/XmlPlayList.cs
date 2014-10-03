using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MetroPlayer.Core.Analyzer;
using MetroPlayer.Core.Base;
using System.IO;

namespace MetroPlayer.Core.PlayList
{
    /// <summary>
    /// 提供对播放列表的支持，本类基于Xml构建
    /// </summary>
    public class XmlPlayList
    {
        /// <summary>
        /// Xml文件路径
        /// </summary>
        private string XmlFilePath;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="XmlFilePath"></param>
        public XmlPlayList(string XmlFilePath)
        {
            this.XmlFilePath = XmlFilePath;
        }

        /// <summary>
        /// 当文件不存在时，创建新的播放列表文件
        /// </summary>
        public void CreatNewFile()
        {
            if (!File.Exists(XmlFilePath))
            {
                XDocument Document = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("PlayList")
                );
                Document.Save(XmlFilePath);
            }
        }

        /// <summary>
        /// 返回全部的歌曲
        /// </summary>
        /// <returns></returns>
        public List<Music> GetMusics()
        {
            List<Music> AllMusic = new List<Music>();
            //加载文件
            XElement Xe = XElement.Load(this.XmlFilePath);
            //读取列表
            IEnumerable<XElement> Elements = from Musics in Xe.Elements("Music") select Musics;
            foreach (XElement Element in Elements)
            {
                MusicType Type = GetTypeByValue(Element.Element("Type").Value);
                Music Music = new Music(Element.Element("Title").Value, 
                                        Element.Element("Url").Value,
                                        Convert.ToInt32(Element.Element("Like").Value), 
                                        Convert.ToInt32(Element.Element("Listen").Value),
                                        Type);
                AllMusic.Add(Music);
            }
            return AllMusic;
        }

        /// <summary>
        /// 返回经过排序后的歌曲
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Music> GetMusics(AnalyzerType Type)
        {
            List<Music> Musics = new List<Music>();
            switch (Type)
            {
                case AnalyzerType.Like:
                    LikeAnalyzer LikeAnalyzer = new LikeAnalyzer(this);
                    Musics=LikeAnalyzer.SortByLike();
                    break;
                case AnalyzerType.Listen:
                    ListenAnalyzer ListenAnalyzer = new ListenAnalyzer(this);
                    Musics= ListenAnalyzer.SortByListen();
                    break;
                case AnalyzerType.Total:
                    TotalAnalyzer TotalAnalyzer = new TotalAnalyzer(this);
                    Musics= TotalAnalyzer.SortByTotal();
                    break;
            }
            return Musics;
        }

        /// <summary>
        /// 添加音乐
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Url">地址</param>
        /// <param name="Type">类型</param>
        public void AddMusic(string Title,string Url,MusicType Type)
        {

            XElement Xe = XElement.Load(XmlFilePath);
            XElement Music = new XElement("Music",
                new XElement("Title",Title),
                new XElement("Url",Url),
                new XElement("Like",0),
                new XElement("Listen",0),
                new XElement("Type",GetValueByType(Type)));
            Xe.Add(Music);
            Xe.Save(XmlFilePath);
        }

        /// <summary>
        /// 删除根据满足某个节点值条件的歌曲
        /// </summary>
        /// <param name="Value"></param>
        public void DeleteMusic(string ElementName, string ElementValue)
        {
            XElement Xe = XElement.Load(XmlFilePath);
            IEnumerable<XElement> Elements = from Musics in Xe.Elements("Music") where Musics.Element(ElementName).Value == ElementValue select Musics;                   
            if (Elements.Count() > 0)
            {
                Elements.First().Remove();
            }
            Xe.Save(XmlFilePath);
        }

        /// <summary>
        /// 修改某一首歌的节点值
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="ElementName">节点名</param>
        /// <param name="ElementValue">修改值</param>
        public void ModifyMusic(string Title,string ElementName, int ElementValue)
        {
            //保存原始的节点值
            string Node_Title = string.Empty;
            string Node_Url = string.Empty;
            string Node_Type = string.Empty;
            string Node_Like = string.Empty;
            string Node_Listen = string.Empty;
            //查询
            XElement Xe = XElement.Load(XmlFilePath);
            IEnumerable<XElement> Elements = from Musics in Xe.Elements("Music") where Musics.Element("Title").Value == Title select Musics;
            //得到原始的值
            if (Elements.Count() > 0)
            {
                foreach (XElement E in Elements)
                {
                    Node_Title = E.Element("Title").Value;
                    Node_Url = E.Element("Url").Value;
                    Node_Type = E.Element("Type").Value;
                    Node_Like = E.Element("Like").Value;
                    Node_Listen = E.Element("Listen").Value;
                }
            }
            //修改
            XElement XEl = Elements.First();
            //确定是修改Listen还是很Like节点
            if (ElementName == "Listen")
            {
                //修改Listen的值
                int ListenValue = Convert.ToInt32(Node_Listen) + ElementValue;
                XEl.ReplaceNodes(
                    new XElement("Title", Node_Title),
                    new XElement("Url", Node_Url),
                    new XElement("Type", Node_Type),
                    new XElement("Like", Node_Like),
                    new XElement("Listen", ListenValue));
                Xe.Save(XmlFilePath);
            }
            else
            {
                if (ElementName == "Like")
                {
                    int LikeValue = Convert.ToInt32(Node_Like) + ElementValue;
                    XEl.ReplaceNodes(
                        new XElement("Title", Node_Title),
                        new XElement("Url", Node_Url),
                        new XElement("Type", Node_Type),
                        new XElement("Like", LikeValue),
                        new XElement("Listen",Node_Listen));
                    Xe.Save(XmlFilePath);
                }
            }

        }

        /// <summary>
        /// 返回指定歌曲名称的Music
        /// </summary>
        /// <returns></returns>
        public Music GetMusic(string Title)
        {
            Music m = null; ;
            //加载文件
            XElement Xe = XElement.Load(this.XmlFilePath);
            //读取列表
            IEnumerable<XElement> Elements = from Musics in Xe.Elements("Music")  where Musics.Element("Title").Value==Title select Musics;                        
            foreach (XElement Element in Elements)
            {
                m = new Music(Element.Element("Title").Value, 
                    Element.Element("Url").Value,
                    Convert.ToInt32(Element.Element("Like").Value),
                    Convert.ToInt32(Element.Element("Listen").Value),
                    GetTypeByValue(Element.Element("Type").Value));
            }
            return m;
        }

        /// <summary>
        /// 返回全部的歌曲Title
        /// </summary>
        /// <returns></returns>
        public List<string> GetTitles()
        {
            List<string> AllTitles = new List<string>();
            //加载文件
            XElement Xe = XElement.Load(this.XmlFilePath);
            //读取列表
            IEnumerable<XElement> Elements = from Musics in Xe.Elements("Music") select Musics;
            foreach (XElement Element in Elements)
            {
                AllTitles.Add(Element.Element("Title").Value);
            }
            return AllTitles;
        }

        #region 内部方法
        private MusicType GetTypeByValue(string Value)
        {
            MusicType Type=0;
            switch (Value)
            {
                case"Local":
                    Type = MusicType.Local;
                    break;
                case "Web":
                    Type = MusicType.Web;
                    break;
            }
            return Type;
        }
        private string GetValueByType(MusicType Type)
        {
            string Value = string.Empty;
            switch (Type)
            {
                case MusicType.Local:
                    Value = "Local";
                    break;
                case MusicType.Web:
                    Value = "Web";
                    break;
            }
            return Value;
        }
        #endregion
    }
}
