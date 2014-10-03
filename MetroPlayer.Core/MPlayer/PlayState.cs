using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.MPlayer
{
    /// <summary>
    /// 定义播放器状态枚举变量
    /// </summary>
    public enum PlayState : byte
    {
            /// <summary>
            /// 播放
            /// </summary>
            Playing = 1,
            /// <summary>
            /// 暂停
            /// </summary>
            Puased=2,
            /// <summary>
            /// 停止
            /// </summary>
            Stopped=3,
    }
}
