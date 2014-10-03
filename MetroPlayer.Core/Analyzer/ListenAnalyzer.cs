using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.Base;

namespace MetroPlayer.Core.Analyzer
{
    /// <summary>
    /// 基于歌曲收听次数的机器分析
    /// </summary>
    public  class ListenAnalyzer
    {
        /// <summary>
        /// 私有字段Musics
        /// </summary>
        private List<Music> Musics;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="PlayList">播放列表</param>
        public ListenAnalyzer(PlayList.XmlPlayList PlayList)
        {
            this.Musics = PlayList.GetMusics();
        }
        /// <summary>
        /// 根据收听次数排序
        /// </summary>
        /// <returns></returns>
        public List<Music> SortByListen()
        {
            Musics.Sort(new ListenCompare());
            return Musics;
        }

        class ListenCompare : IComparer<Music>
        {
            public int Compare(Music m, Music n)
            {
                return n.Listen.CompareTo(m.Listen);
            }
        }
    }
}
