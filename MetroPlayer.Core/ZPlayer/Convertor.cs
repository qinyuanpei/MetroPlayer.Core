using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.ZPlayer.LibZPlay;

namespace MetroPlayer.Core.ZPlayer
{
    /// <summary>
    /// 本类为转换工具类，提供C++到C#的类型转换
    /// </summary>
    public class Convertor
    {
        /// <summary>
        /// 将一个TStreamTime类型转换为字符串
        /// </summary>
        /// <param name="mTime"></param>
        /// <returns></returns>
        public static string ConvertTStreamTimeToString(TStreamTime mTime)
        {
            //从TStreamTime类型中获取毫秒
            uint mSeconds=mTime.sec;
            //计算分钟数
            int mm=(int)(Convert.ToInt32(mSeconds)/60);
            int ss=(int)(Convert.ToInt32(mSeconds)%60);
            //生成两位时间的字符格式
            string mFormat = string.Format("00");
            return mm.ToString(mFormat) + ":" + ss.ToString(mFormat);
        }

        /// <summary>
        /// 将一个int类型转化为TStreamTime
        /// </summary>
        /// <returns></returns>
        public static TStreamTime ConvertIntToTStreamTime(int mPosition)
        {
            TStreamTime mTime = new TStreamTime();
            mTime.sec = Convert.ToUInt32(mPosition);
            return mTime;
        }

        /// <summary>
        /// 将一个TStreamTime转化为int类型
        /// </summary>
        /// <param name="mTime"></param>
        /// <returns></returns>
        public static int ConvertTStreamTimeToInt(TStreamTime mTime)
        {
            return Convert.ToInt32(mTime.sec);
        }
    }
}
