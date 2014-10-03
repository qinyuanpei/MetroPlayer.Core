using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.ZPlayer
{
    /// <summary>
    /// 这里定义播放器的三种状态
    /// </summary>
    public static class  PlayState
    {
        /// <summary>
        /// 播放
        /// </summary>
        public static int mPlay=0;
        /// <summary>
        /// 暂停
        /// </summary>
        public static int mPause = 1;
        /// <summary>
        /// 停止
        /// </summary>
        public static int mStop=2;
    }
}
