using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.Search
{
    /// <summary>
    /// 歌曲类,提供豆瓣音乐的信息实体描述
    /// </summary>
    public class ChannelSong
    {

            /// <summary>
            /// 封面图片
            /// </summary>
            public Uri Picture { get; set; }
            /// <summary>
            /// 唱片标题
            /// </summary>
            public string AlbumTitle { get; set; }
            /// <summary>
            /// 唱片路径
            /// </summary>
            public string Album { get; set; }
            /// <summary>
            /// 发行公司
            /// </summary>
            public string Company { get; set; }
            /// <summary>
            /// 平均评分
            /// </summary>
            public double Rating { get; set; }
            /// <summary>
            /// 发行时间
            /// </summary>
            public string PublicTime { get; set; }
            /// <summary>
            /// 不知道是什么东西的ID
            /// </summary>
            public string Ssid { get; set; }
            /// <summary>
            /// 当前用户是否喜欢
            /// </summary>
            public bool Like { get; set; }
            /// <summary>
            /// 歌手
            /// </summary>
            public string Artist { get; set; }
            /// <summary>
            /// 歌曲路径
            /// </summary>
            public Uri Url { get; set; }
            /// <summary>
            /// 歌曲名称
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 普通音乐应该是""，广告应该是"T"
            /// </summary>
            public string SubType { get; set; }
            /// <summary>
            /// 歌曲ID
            /// </summary>
            public string SongID { get; set; }
            /// <summary>
            /// 不知道是什么东西的长度
            /// </summary>
            public double Length { get; set; }
            /// <summary>
            /// 专辑ID
            /// </summary>
            public string AlbumID { get; set; }
        }
}
