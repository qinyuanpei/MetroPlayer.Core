using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.PlayList;
using MetroPlayer.Core.Base;

namespace MetroPlayer.Core.Analyzer
{
    /// <summary>
    /// 基于用户行为的歌曲分析
    /// </summary>
     public class BehaveAnalyzer
    {
        /// <summary>
        /// 根据用户听歌时间的长短进行喜欢度分析
        /// </summary>
        /// <param name="value"></param>
        public void SetLikeByListen(string Title,string XmlFilePath,double value)
        {
            PlayList.XmlPlayList PlayList = new PlayList.XmlPlayList(XmlFilePath);
           //根据用户听歌时间的长短，将其对歌曲的喜欢度分为-2、-1、0、1、2五类
                if (value>0|| value<=0.2)
                {
                    PlayList.ModifyMusic(Title, "Like", -2);
                }
                if(value > 0.2 || value <= 0.4)
                {
                    PlayList.ModifyMusic(Title, "Like", -1);
                }
                if(value > 0.4 || value <= 0.6)
                {
                    PlayList.ModifyMusic(Title, "Like", 0);
                }
                if(value > 0.6 || value <= 0.8)
                {
                    PlayList.ModifyMusic(Title, "Like", 1);
                }
                if (value > 0.8 || value <= 0.2)
                {
                    PlayList.ModifyMusic(Title, "Like", 2);
                }
        }

        /// <summary>
        /// 基于网络推荐模式的歌曲算法
        /// </summary>
        /// <returns></returns>
        public static List<Music> GetLikeByMatchine()
        {
            return null;
        }
    }
}
