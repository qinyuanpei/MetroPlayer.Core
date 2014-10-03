using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.Lrc
{
    /// <summary>
    /// 提供同步歌词和桌面歌词的支持
    /// 作者：秦元培
    /// 时间：2014年2月1日
    /// 更新：
    /// 
    /// </summary>
  /// <summary>
    /// 歌词类
    /// </summary>
    public class Lrc
    {
        //私有字段
        private string _Url;//歌词路径
        private ArrayList LrcList;//歌词文本列表
        private string _EncodeString;//编码字符，包括"gb2312","utf-8"两种
        private System.Text.Encoding Encoding;//字符编码


        //定义两种歌词编码格式
        public static string Encode_Type_UTF8="utf-8";
        public static string Encode_Type_GB2312="gb2312";


        //属性
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public string EncodeString
        {
            get { return _EncodeString; }
            set { _EncodeString = value; }
        }

        //构造函数
        public Lrc(string MusicUrl, string EncodeString)
        {
            this._Url = MusicUrl;
            this._EncodeString = EncodeString;
            //通过编码字符确定打开歌词文件的编码类型
            if (_EncodeString == Lrc.Encode_Type_GB2312)
            {
                //设置为gb2312
                this.Encoding = System.Text.Encoding.GetEncoding("gb2312");
            }
            else if (_EncodeString == Lrc.Encode_Type_UTF8)
            {
                //设置为utf-8
                this.Encoding = System.Text.Encoding.GetEncoding("utf-8");
            }
        }
        //成员函数
        public void OpenLrcFile()
        {
            //声明歌词文本列表并清除，以便于当歌词出现乱码时，重新读取歌词文件
            LrcList = new ArrayList();
            LrcList.Clear();
            //根据mp3的路径获取歌词的路径
            string LrcUrl = _Url.Replace(".mp3", ".lrc");
            //歌词名称，为下载歌词做准备
            string LrcName = _Url.Substring(_Url.LastIndexOf("\\") + 1, _Url.LastIndexOf(".") - _Url.LastIndexOf("\\") - 1);
            FileInfo f = new FileInfo(LrcUrl);
            if (f.Exists)
            {
                StreamReader sr = new StreamReader(LrcUrl, Encoding);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    LrcList.Add(line);
                }
            }
        }

        public string ShowLrc(double PositionTime)
        {
            string LrcText = string.Empty;
            if (LrcList.Count == 0)
            {
                LrcText = "";//当网络未连接时，LrcList将为null，此时将不显示歌词。
            }
            else
            {
                string TimeFormat = string.Format("00");//制定时间为00的形式
                //这里记得要做类型转换，不然会出现意想不到的结果哦
                string ss = ((int)PositionTime % 60).ToString(TimeFormat);//获取秒
                string mm = ((int)(PositionTime / 60)).ToString(TimeFormat);//获取分
                string Time = mm + ":" + ss;//组合得到"xx:xx"的形式,
                //注意！由于算法的缺陷，时间只能精确到秒(xx:xx)，而歌词中的时间为毫秒(xx:xx.xx)
                for (int i = 0; i < LrcList.Count; i++)
                {
                    //如果该行包含当前时间字符串或者当前时间小于当前行时间，则显示对应的文本值
                    if (LrcList[i].ToString().IndexOf(Time) > 0 )
                    {
                        string MatchText = LrcList[i].ToString();//取得该行歌词文本
                        int MatchLength = MatchText.Length;
                        int Filter = MatchText.LastIndexOf("]");//找到最后一个]的位置，截取歌词文本
                        LrcText = MatchText.Substring(Filter + 1, MatchLength - Filter - 1);
                        break;
                    }
                }
            }
            return LrcText;

        }

    }
    
}
