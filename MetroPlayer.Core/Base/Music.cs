using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.Base
{
    /// <summary>
    /// 基础数据结构类
    /// </summary>
    public class Music
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Music()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Title">歌曲标题</param>
        /// <param name="Url">歌曲路径</param>
        /// <param name="Like">喜欢次数</param>
        /// <param name="Listen">收听次数</param>
        /// <param name="Type">歌曲类型</param>
        public Music(string Title,string Url,int Like,int Listen,MusicType Type)
        {
            //音乐的标题
            this.Title = Title;
            //音乐的路径
            this.Url = Url;
            //音乐被喜欢的次数
            this.Like = Like;
            //音乐被收听的次数
            this.Listen = Listen;
            //音乐的类型
            this.Type = Type;
        }

        /// <summary>
        /// 歌曲标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 歌曲路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 喜欢次数
        /// </summary>
        public int Like { get; set; }
        /// <summary>
        /// 收听次数
        /// </summary>
        public int Listen { get; set; }
        /// <summary>
        /// 歌曲类型
        /// </summary>
        public MusicType Type { get; set; }


    }
}
