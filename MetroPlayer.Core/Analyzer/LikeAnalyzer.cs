using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.PlayList;
using MetroPlayer.Core.Base;

namespace MetroPlayer.Core.Analyzer
{
    /// <summary>
    /// 基于歌曲喜欢度的机器分析方法
    /// </summary>
    public class LikeAnalyzer
    {
        /// <summary>
        /// 私有字段Musics
        /// </summary>
        private List<Music> Musics;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="PlayList">播放列表</param>
        public LikeAnalyzer(PlayList.XmlPlayList PlayList)
        {
            this.Musics = PlayList.GetMusics();
        }
        /// <summary>
        /// 根据喜欢度排序
        /// </summary>
        /// <returns></returns>
        public List<Music> SortByLike()
        {
            Musics.Sort(new LikeCompare());
            return Musics;
        }

        class LikeCompare : IComparer<Music>
        {
            public int Compare(Music m, Music n)
            {
                return n.Like.CompareTo(m.Like);
            }
        }
    }
}
