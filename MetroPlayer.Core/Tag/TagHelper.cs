using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Id3Lib;
using Mp3Lib;
using MetroPlayer.Core.ZPlayer.LibZPlay;


namespace MetroPlayer.Core.Tag
{
    /// <summary>
    /// 基于开源Net音频标签库的Mp3标签操作类
    /// 为了减少最终生成库的体积，我希望可以自己编写这个类
    /// 因为这个类库只支持x86架构，为了让程序的扩展性更强
    /// 所以最好的办法就是自己编写这个类
    /// </summary>
    public class TagHelper
    {
        //这里定义标签名称常量
        /// <summary>
        /// Title
        /// </summary>
        public const string Tag_Title = "Title";
        /// <summary>
        /// Author
        /// </summary>
        public const string Tag_Author = "Author";
        /// <summary>
        /// Album
        /// </summary>
        public const string Tag_Album = "Album";
        /// <summary>
        /// Picture
        /// </summary>
        public const string Tag_Picture = "Picture";
        /// <summary>
        /// Year
        /// </summary>
        public const string Tag_Year = "Year";
        /// <summary>
        /// Track
        /// </summary>
        public const string Tag_Track = "Track";
        /// <summary>
        /// Comment
        /// </summary>
        public const string Tag_Comment = "Comment";
        /// <summary>
        /// Composer
        /// </summary>
        public const string Tag_Composer="Composer";
        /// <summary>
        /// Length
        /// </summary>
        public const string Tag_Length = "Length";



        /// <summary>
        /// Mp3文件路径
        /// </summary>
        private String mFileName;

        /// <summary>
        /// Mp3File
        /// </summary>
        private Mp3File mMp3File;

        /// <summary>
        /// TagHandler
        /// </summary>
        private TagHandler mHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mFileName">Mp3文件路径</param>
        public TagHelper(string mFileName)
        {
            this.mFileName = mFileName;
            //创建Mp3File对象
            mMp3File = new Mp3File(mFileName);
            //获取TagHandler
            mHandler = mMp3File.TagHandler;
        }
        
        /// <summary>
        /// 根据标签名返回标签值
        /// </summary>
        /// <param name="mTagName"></param>
        /// <returns></returns>
        public object GetTagByName(string mTagName )
        {
            object mTagValue =null;
            switch (mTagName)
            {
                case TagHelper.Tag_Title:
                    mTagValue = mHandler.Title;
                    break;
                case TagHelper.Tag_Author:
                    mTagValue = mHandler.Artist;
                    break;
                case TagHelper.Tag_Picture:
                    mTagValue = mHandler.Picture;
                    break;
                case TagHelper.Tag_Year:
                    mTagValue = mHandler.Year;
                    break;
                case TagHelper.Tag_Track:
                    mTagValue = mHandler.Track;
                    break;
                case TagHelper.Tag_Comment:
                    mTagValue = mHandler.Comment;
                    break;
                case TagHelper.Tag_Composer:
                    mTagValue = mHandler.Composer;
                    break;
                case TagHelper.Tag_Length:
                    mTagValue = mHandler.Length;
                    break;

            }
            return mTagValue;
        }

        /// <summary>
        /// 暂时无法实现此方法
        /// </summary>
        /// <param name="mTagName"></param>
        public void updateTagByName(string mTagName)
        {
            
        }
    }
}
