using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroPlayer.Core.ZPlayer
{
    /// <summary>
    /// 本类为播放器的均衡器音效类，在这里我们定义下面的音效类型：
    /// 1、Bruce：布鲁斯
    /// 2、Classic：古典
    /// 3、Country：乡村
    /// 4、Dance：舞曲
    /// 5、Jazz：爵士
    /// 6、Electric 电子乐
    /// 7、Pass：怀旧
    /// 8、Opera：歌剧
    /// 9、Rock：摇滚
    /// 10、Voice：人声
    /// 11、Auto：自动匹配
    /// 我们将频率波段分为-12db到+12db，设某一波段对应的范围为[a,b]，则该波段的中值为(a+b)/2。
    /// 此时，该波段范围内的值可以表示为：(a+b)/2+x*(100/24)，其中x为-12db到+12db范围内的任意一个整数
    /// </summary>
    /// 


    
    public static class Equalizer
    {
        /// <summary>
        /// 定义均衡器预设枚举
        /// </summary>
        public enum EqualizerType
        {
            /// <summary>
            /// 布鲁斯
            /// </summary>
            Bruce,
            /// <summary>
            /// 古典
            /// </summary>
            Classic,
            /// <summary>
            /// 乡村
            /// </summary>
            Country,
            /// <summary>
            /// 舞曲
            /// </summary>
            Dance,
            /// <summary>
            /// 爵士
            /// </summary>
            Jazz,
            /// <summary>
            /// 电子乐
            /// </summary>
            Electric,
            /// <summary>
            /// 怀旧
            /// </summary>
            Pass,
            /// <summary>
            /// 歌剧
            /// </summary>
            Opera,
            /// <summary>
            /// 摇滚
            /// </summary>
            Rock,
            /// <summary>
            /// 人声
            /// </summary>
            Voice,
            /// <summary>
            /// 自动
            /// </summary>
            Auto,
        }

        /// <summary>
        /// 定义均衡器默认的频率波段
        /// </summary>
        public static int[] DefaultBand = new int[] { 31,62,125,250,500,1000,2000,4000,8000,16000};

        /// <summary>
        /// 获得各个波段的中值
        /// </summary>
        /// <returns></returns>
        private static int[] GetBandMidValue()
        {
            int[] MidBand = new int[9];
            for (int i = 0; i < MidBand.Length; i++)
            {
                if (i == 0)
                {
                    MidBand[i] = (DefaultBand[i]+0) / 2;
                }
                else
                {
                    MidBand[i] = (DefaultBand[i - 1] + DefaultBand[i]) / 2;
                }
            }
            return MidBand;
        }

        /// <summary>
        /// 布鲁斯音效频率设定
        /// </summary>
        public static int[] Bruce=new int[]{-2,0,2,1,0,0,0,0,-2,-4};

        /// <summary>
        /// 古典音效频率设定
        /// </summary>
        public static int[] Classic=new int[]{10,8,3,1,0,0,1,3,8,10};

        /// <summary>
        /// 乡村音效频率设定
        /// </summary>
        public static int[] Country=new int[]{5,6,2,-5,1,1,-5,3,8,5};

        /// <summary>
        /// 舞曲音效频率设定
        /// </summary>
        public static int[] Dance=new int[]{5,9,12,0,4,-4,-4,8,-2,4};

        /// <summary>
        /// 爵士音效频率设定
        /// </summary>
        public static int[] Jazz=new int[]{-2,5,5,1,-6,3,1,4,6,2};

        /// <summary>
        /// 电子乐音效频率设定
        /// </summary>
        public static int[] Electric=new int[]{-6,1,4,-2,-2,-4,0,0,6,6};

        /// <summary>
        /// 怀旧音效频率设定
        /// </summary>
        public static int[] Pass=new int[]{-4,0,2,1,0,0,0,0,-4,-6};

        /// <summary>
        /// 歌剧音效频率设定
        /// </summary>
        public static int[] Opera=new int[]{0,0,0,4,5,3,6,3,0,0};

        /// <summary>
        /// 摇滚音效频率设定
        /// </summary>
        public static int[] Rock=new int[]{7,7,9,-3,2,-3,-1,6,9,7};

        /// <summary>
        /// 人声音效频率设定
        /// </summary>
        /// 
        public static int[] Voice=new int[]{-6,-2,-4,-8,2,6,8,6,-2,-6};

        /// <summary>
        /// 自动匹配效频率设定
        /// </summary>
        public static int[] Auto=new int[]{5,6,7,0,-1,3,4,5,6,7};

        /// <summary>
        /// 获取指定类型的EQ特效参数
        /// </summary>
        /// <returns></returns>
        public static int[] GetEqulizerBandValue(EqualizerType mType)
        {
            //定义EQ特效频率数组
            int[] mBandValue = new int[9];
            //定义EQ特效预设值数组
            int[] mBandPreValue = new int[9];
            //根据EQ特效类型获取预设值
            switch (mType)
            {
                case EqualizerType.Bruce:
                    mBandPreValue = Bruce;
                    break;
                case EqualizerType.Classic:
                    mBandPreValue = Classic;
                    break;
                case EqualizerType.Country:
                    mBandPreValue = Country;
                    break;
                case EqualizerType.Dance:
                    mBandPreValue = Dance;
                    break;
                case EqualizerType.Electric:
                    mBandPreValue = Electric;
                    break;
                case EqualizerType.Jazz:
                    mBandPreValue = Jazz;
                    break;
                case EqualizerType.Opera:
                    mBandPreValue = Opera;
                    break;
                case EqualizerType.Pass:
                    mBandPreValue = Pass;
                    break;
                case EqualizerType.Rock:
                    mBandPreValue = Rock;
                    break;
                case EqualizerType.Voice:
                    mBandPreValue = Voice;
                    break;
                case EqualizerType.Auto:
                    mBandPreValue = Auto;
                    break;
            }
            //获取频率中值数组
            int[] MidValue=GetBandMidValue();
            //计算频率数组
            for(int i=0;i<MidValue.Length;i++)
            {
                mBandValue[i] = MidValue[i] * mBandPreValue[i] * (100 / 24);
            }
            return mBandValue;
        }
    }
}
 