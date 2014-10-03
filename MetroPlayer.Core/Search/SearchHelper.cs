using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MetroPlayer.Core.Search
{
    /// <summary>
    /// 在线搜索辅助类
    /// 本搜索辅助类调用了百度音乐未公开的API接口，可以通过搜索获得歌曲的下载地址和歌词的下载地址
    /// </summary>
    class SearchHelper
    {
        /// <summary>
        /// 搜索歌词的方法
        /// </summary>
        /// <param name="mTitle">歌曲名称</param>
        /// <returns></returns>
        public static String SearchLrc(string mTitle)
        {
            string mResult=null;
            
            HttpWebRequest mRequest = null;
            HttpWebResponse mResponse = null;
            //定义查询地址
            string mBaseUrl = string.Format("http://geci.me/api/lyric/{0}",mTitle);
            //发送请求
            mRequest = (HttpWebRequest)WebRequest.Create(mBaseUrl);
            //返回请求
            mResponse = (HttpWebResponse)mRequest.GetResponse();
            //读取返回流
            StreamReader mReader = new StreamReader(mResponse.GetResponseStream());
            //获取返回字符串
            string mXmldetail = mReader.ReadToEnd();
            //结束请求
            mReader.Close();
            mResponse.Close();
            //解析返回结果
            string mAll=mXmldetail;
            if (mAll.IndexOf("[]") < 0)
            {
                int mLrcStart = mAll.IndexOf("lrc") + 7;
                int mLrcEnd = mAll.IndexOf("artist") - 3;
                mResult = mAll.Substring(mLrcStart, mLrcEnd - mLrcStart + 1);
            }
            else
            {
                mResult = "暂时无法搜索到歌词";
            }
            return mResult;
        }


        /// <summary>
        /// 搜索音乐的方法
        /// </summary>
        /// <param name="mTitle">歌曲名称</param>
        /// <returns></returns>
        public static List<SearchResult>  Search(string mTitle)
        {
            //搜索结果集合
            List<SearchResult> mResults = new List<SearchResult>();

            HttpWebRequest mRequest = null;
            HttpWebResponse mResponse = null;
            //定义查询地址
            string mBaseUrl = String.Format("http://box.zhangmen.baidu.com/x?op=12&count=1&title={0}&$$$$$$",mTitle);
            //发送请求
            mRequest = (HttpWebRequest)WebRequest.Create(mBaseUrl);
            //返回请求
            mResponse = (HttpWebResponse)mRequest.GetResponse();
            //读取返回流
            StreamReader mReader = new StreamReader(mResponse.GetResponseStream());
            //获取返回字符串
            string mXmldetail = mReader.ReadToEnd();
            //替换混淆字符串
            mXmldetail = mXmldetail.Replace("<![CDATA[", "");
            mXmldetail = mXmldetail.Replace("]]>", "");
            //结束请求
            mReader.Close();
            mResponse.Close();
            //下面开始做返回值解析，由于返回的是非标准xml，采用字符解析的方式
            //其实最好是使用正则表达式进行截取，只是我的正则实在不怎么好
            //所以请大家谅解
            string mAll = mXmldetail;
            //定义查询Encode和Decode的起始、终止位置
            int mEncodeStart, mEncodeEnd, mDecodeStart, mDecodeEnd;
            //定义查询歌词的起始、终止位置
            int mLrcStart,mLrcEnd;
            //定义要截取的Uri
            string mEncodeUrl, mDecodeUrl;
            //定义要截取的Lrc
            string mLrc1,mLrc2;
            //初始化起始、终止位置
            mEncodeStart = 1; mEncodeEnd = 1; mDecodeStart = 1; mDecodeEnd = 1;
            mLrcStart=1;mLrcEnd=1;
            int i = 1; //设置记录变量i，用于统计数据
            //反复读取寻找歌曲链接
            //貌似就返回5条结果？
            while (i <= 5)
            {
                SearchResult mResult = new SearchResult();

                //查询歌曲Url
                int s1 = mEncodeStart;
                int e1 = mEncodeEnd;
                int s2 = mDecodeStart;
                int e2 = mDecodeEnd;
                //截取 mEncodeUrl
                mEncodeStart = mAll.IndexOf("<encode><![CDATA[") + "<encode><![CDATA[".Length;
                mEncodeEnd = mAll.IndexOf("]]></encode>");
                mEncodeUrl = mAll.Substring(mEncodeStart, mEncodeEnd - mEncodeStart + 1);
                //截取 mDecodeUrl
                mDecodeStart = mAll.IndexOf("<decode><![CDATA[") + "<decode><![CDATA[".Length;
                mDecodeEnd = mAll.IndexOf("]]></decode>");
                mDecodeUrl = mAll.Substring(mDecodeStart, mDecodeEnd - mDecodeStart + 1);
                //连接两个Url
                mResult.MusicUrl = mEncodeUrl + mDecodeUrl;


                //查询歌词Url
                int r1=mLrcStart;
                int r2=mLrcEnd;
                mLrcStart=mAll.IndexOf("<lrcid>")+"<lrcid>".Length;
                mLrcEnd=mAll.IndexOf( "</lrcid>");
                mLrc1 = mAll.Substring(mLrcStart, mLrcEnd - mLrcStart + 1);
                mLrc2 = ((int)Int32.Parse(mLrc1) / 100).ToString(); ;
                mResult.LrcUri = "http://box.zhangmen.baidu.com/bdlrc/" + mLrc2 + "/" + mLrc1 + ".lrc";

                //添加到搜索结果集合
                mResults.Add(mResult);
            }

            return mResults;
        }
    }


    /// <summary>
    /// 描述搜索结果的类
    /// </summary>
    public class SearchResult
    {
        private string mMusicUrl;

        public string MusicUrl
        {
            get { return mMusicUrl; }
            set { mMusicUrl = value; }
        }
        private string mLrcUri;

        public string LrcUri
        {
            get { return mLrcUri; }
            set { mLrcUri = value; }
        }

    }
}
