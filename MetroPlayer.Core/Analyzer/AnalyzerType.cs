using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.Analyzer
{
    /// <summary>
    /// 用户喜好分析器类型枚举：根据喜欢度排序、根据收听次数排序、总体排序
    /// </summary>
    public enum AnalyzerType
    {
        /// <summary>
        /// 喜欢度排序
        /// </summary>
        Like,
        /// <summary>
        /// 听过次数排序
        /// </summary>
        Listen,
        /// <summary>
        /// 总体排序
        /// </summary>
        Total
    }
}
