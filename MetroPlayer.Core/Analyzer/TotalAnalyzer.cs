using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.Analyzer;
using MetroPlayer.Core.Base;

namespace MetroPlayer.Core.Analyzer
{
    /// <summary>
    /// 机器综合分析
    /// </summary>
    public class TotalAnalyzer
    {
        /// <summary>
        /// 私有字段Musics
        /// </summary>
        private List<Music> Musics;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="PlayList">播放列表</param>
        public TotalAnalyzer(PlayList.XmlPlayList PlayList)
        {
            this.Musics = PlayList.GetMusics();
        }
        /// <summary>
        /// 综合排序
        /// </summary>
        /// <returns></returns>
        public List<Music> SortByTotal()
        {
            Musics.Sort(new TotalCompare());
            return Musics;
        }

        class TotalCompare : IComparer<Music>
        {
            public int Compare(Music m, Music n)
            {
                if (n.Like.Equals(m.Like))//如果喜欢次数相同,就按照收听次数排序
                {
                    return n.Listen.CompareTo(m.Listen);
                }
                else if (n.Listen.Equals(m.Listen))//如果收听次数相同，就按照喜欢次数进行排序
                {
                    return n.Like.CompareTo(m.Like);
                }
                else//如果都不相同,就取平均数进行排序
                {
                    int Average_n=(n.Like+n.Listen)/2;
                    int Average_m=(m.Like+m.Listen)/2;
                    return Average_n.CompareTo(Average_m);
                }

            }
        }
    }
}
