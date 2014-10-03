using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
/// <summary>
/// 从4.0版本起在System.Runtime.Serialization空间下增加对Json的支持
/// 如果不想使用此类进行解析，可以使用Newtonsoft.Json.dll类库，该库已经集成在项目中
/// </summary>
using System.Runtime.Serialization.Json;

namespace MetroPlayer.Core.Search
{

    /// <summary>
    /// 本类为豆瓣电台的API接口，可以提供公共频道的音乐资源
    /// </summary>
    public  class DouBanHelper
    {
        //定义下列公共频道
        public static Channel Channel_HuaYu = new Channel("华语", 1);
        public static Channel Channel_OuMei = new Channel("欧美", 2);
        public static Channel Channel_QiLing = new Channel("七零", 3);
        public static Channel Channel_BaLing = new Channel("八零", 4);
        public static Channel Channel_JiuLing = new Channel("九零", 5);
        public static Channel Channel_YueYu = new Channel("粤语", 6);
        public static Channel Channel_YaoGun = new Channel("摇滚", 7);
        public static Channel Channel_MinYao = new Channel("民谣", 8);
        public static Channel Channel_QingYinYue = new Channel("轻音乐", 9);
        public static Channel Channel_DianYing = new Channel("电影", 10);
        public static Channel Channel_JueShi = new Channel("爵士", 13);
        public static Channel Channel_DianZi = new Channel("电子", 14);
        public static Channel Channel_ShuoChang = new Channel("说唱", 15);
        public static Channel Channel_RB = new Channel("R&B", 16);
        public static Channel Channel_RiYu = new Channel("日语", 17);
        public static Channel Channel_HanYu = new Channel("韩语", 18);
        public static Channel Channel_Puma = new Channel("Puma", 19);
        public static Channel Channel_Gril = new Channel("女声", 20);
        public static Channel Channel_FaYu = new Channel("法语", 22);
        public static Channel Channel_DouBan = new Channel("豆瓣音乐人", 28);


        /// <summary>
        /// 根据返回的json解析指定频道的歌曲列表
        /// </summary>
        /// <returns></returns>
        public static List<ChannelSong> getChannelSong(Channel mChannel)
        {
            string Result = string.Empty;
            //请求json
            string RequestUrl = String.Format("http://douban.fm/j/mine/playlist?channel={0}&type=n&uid=0&n", mChannel.ChannelID);
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(RequestUrl);
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            StreamReader Stream = new StreamReader(Response.GetResponseStream());
            Result = Stream.ReadToEnd();
            //结束请求
            Stream.Close();
            Response.Close();
            Request.Abort();
            //对json进行解析
            DataContractJsonSerializer Json = new DataContractJsonSerializer(typeof(List<ChannelSong>));
            MemoryStream Object = new MemoryStream(Encoding.Unicode.GetBytes(Result));
            return (List<ChannelSong>)Json.ReadObject(Object);
        }

        /// <summary>
        /// 豆瓣音乐频道描述类
        /// </summary>
        public class Channel
        {
            public Channel(string mChannelName,int mChannelID)
            {
                this.mChannelName = mChannelName;
                this.mChannelID = mChannelID;
            }

            private String mChannelName;
            public String ChannelName
            {
                get { return mChannelName; }
                set { mChannelName = value; }
            }

            private int mChannelID;
            public int ChannelID
            {
                get { return mChannelID; }
                set { mChannelID = value; }
            }
        }
        
    }
}
