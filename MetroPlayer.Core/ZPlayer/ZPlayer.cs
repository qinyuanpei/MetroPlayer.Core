using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.Base;
using MetroPlayer.Core.ZPlayer;
using MetroPlayer.Core.ZPlayer.LibZPlay;


namespace MetroPlayer.Core.ZPlayer
{

    /// <summary>
    /// 基于LibZPlay封装的ZPlayer
    /// </summary>
    public class ZPlayer
    {
        /// <summary>
        /// ZPlayer实例
        /// </summary>
        private static ZPlayer mInstance;

        /// <summary>
        /// ZPlay
        /// </summary>
        private LibZPlay.ZPlay mZPlay=null;

        /// <summary>
        /// 播放器状态
        /// </summary>
        private TStreamStatus mStatus;

        /// <summary>
        /// 播放器进度
        /// </summary>
        private TStreamTime mPosition;

        /// <summary>
        /// 歌曲信息
        /// </summary>
        private TStreamInfo mInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected ZPlayer()
        {
            if(mZPlay==null)
            {
                mZPlay=new LibZPlay.ZPlay();
            }
        }

        /// <summary>
        /// 单例模式下的获取ZPlayer的方法
        /// </summary>
        /// <returns></returns>
        public static ZPlayer getInstance()
        {
            if (mInstance == null)
                mInstance = new ZPlayer();
            return mInstance;
        }

        /// <summary>
        /// 播放音乐的方法
        /// </summary>
        public void Play()
        {
            mZPlay.StartPlayback();
        }

        /// <summary>
        /// 设置音乐资源
        /// </summary>
        /// <param name="mFileName">文件路径</param>
        public void SetResource(string mFileName)
        {
            mZPlay.OpenFile(mFileName, LibZPlay.TStreamFormat.sfAutodetect);
        }

        /// <summary>
        /// 停止音乐的方法
        /// </summary>
        public void Stop()
        {
            mZPlay.StopPlayback();
        }

        /// <summary>
        /// 暂停音乐的方法
        /// </summary>
        public void Pause()
        {
            mZPlay.PausePlayback();
        }

        /// <summary>
        /// 恢复音乐的方法                                                                     
        /// </summary>
        public void Resume()
        {
            mZPlay.ResumePlayback();
        }
        /// <summary>
        /// 定位向前播放方法
        /// </summary>
        /// <param name="mTime"></param>
        public void SeekForward(int mValue)
        {
            TStreamTime mTime = new TStreamTime();
            mTime.sec = Convert.ToUInt32(mValue);
            mZPlay.Seek(TTimeFormat.tfSecond, ref mTime,TSeekMethod.smFromCurrentForward);
        }

        /// <summary>
        /// 定位向后播放方法
        /// </summary>
        /// <param name="mTime"></param>
        public void SeekBackward(int mValue)
        {
            TStreamTime mTime = new TStreamTime();
            mTime.sec = Convert.ToUInt32(mValue);
            mZPlay.Seek(TTimeFormat.tfSecond, ref mTime, TSeekMethod.smFromCurrentBackward);
        }

        /// <summary>
        /// 设置播放器声音大小其值介于0到100之间
        /// </summary>
        public void SetPlayerVolume(int mLValue,int mRValue)
        {
            mZPlay.SetPlayerVolume(mLValue, mRValue);
        }

        /// <summary>
        /// 设置设备声音大小其值介于0到100之间
        /// </summary>
        /// <param name="mLValue"></param>
        /// <param name="mRValue"></param>
        public void SetOutputVolume(int mLValue,int mRValue)
        {
            mZPlay.SetMasterVolume(mLValue, mRValue);
        }

        /// <summary>
        /// 设置均衡器效果
        /// </summary>
        public void SetEqualizer(Equalizer.EqualizerType mType)
        {
            mZPlay.EnableEqualizer(true);
            mZPlay.SetEqualizerPoints(ref Equalizer.DefaultBand,10);
            int[] mEqualizerValue = Equalizer.GetEqulizerBandValue(mType);
            for (int i = 0; i < mEqualizerValue.Length; i++)
            {
                mZPlay.SetEqualizerBandGain(i, mEqualizerValue[i]);
            }
        }

        /// <summary>
        /// 是否启用均衡器效果
        /// </summary>
        /// <param name="isEqualizer"></param>
        public void EnableEqualizer(Boolean isEqualizer)
        {
            if (isEqualizer)
            {
                mZPlay.EnableEqualizer(true);
            }
            else
            {
                mZPlay.EnableEqualizer(false);
            }
        }

        /// <summary>
        /// 返回播放器状态
        /// </summary>
        /// <returns></returns>
        public int GetStatus()
        {
            int mResult = 0;
            //获取播放器当前状态
            mZPlay.GetStatus(ref mStatus);
            //对状态进行分析
            if (mStatus.fPlay)
            {
                mResult = PlayState.mPlay;
            }
            else if (mStatus.fPause)
            {
                mResult = PlayState.mPause;
            }
            else if (mZPlay.StopPlayback())
            {
                mResult = PlayState.mStop;
            }
            return mResult;
        }

        /// <summary>
        /// 返回播放器的当前字符型时间
        /// </summary>
        /// <returns></returns>
        public string GetPositionString()
        {
            mZPlay.GetPosition(ref mPosition);
            return Convertor.ConvertTStreamTimeToString(mPosition);
        }

        /// <summary>
        /// 返回播放器的当前整型时间
        /// </summary>
        /// <returns></returns>
        public int GetPosition()
        {
            mZPlay.GetPosition(ref mPosition);
            return Convertor.ConvertTStreamTimeToInt(mPosition);
        }

        /// <summary>
        /// 返回比特率
        /// </summary>
        /// <returns></returns>
        public int GetBitrate()
        {
            return mZPlay.GetBitrate(true);
        }

        /// <summary>
        /// 返回歌曲整型总长度
        /// </summary>
        /// <returns></returns>
        public int GetDuration()
        {
            mZPlay.SetSettings(TSettingID.sidAccurateLength, 1);
            mZPlay.GetStreamInfo(ref mInfo);
            return Convertor.ConvertTStreamTimeToInt(mInfo.Length);
        }

        /// <summary>
        /// 返回当前歌曲字符型总长度
        /// </summary>
        /// <returns></returns>
        public string GetDurationString()
        {
            mZPlay.SetSettings(TSettingID.sidAccurateLength, 1);
            mZPlay.GetStreamInfo(ref mInfo);
            return Convertor.ConvertTStreamTimeToString(mInfo.Length);
        }

        /// <summary>
        /// 设置歌曲速率正常值为100
        /// </summary>
        /// <param name="mRate"></param>
        public void SetRate(int mRate)
        {
            mZPlay.SetRate(mRate);
        }

        /// <summary>
        /// 绘制声波图像
        /// </summary>
        public void DrawSoundWavGraphics()
        {

        }

        /// <summary>
        /// 返回某个音乐文件的格式
        /// </summary>
        /// <param name="mFileName">文件路径</param>
        /// <returns></returns>
        public MusicFormat GetMusicFormat(string mFileName)
        {
            MusicFormat mFormat = MusicFormat.Mp3;
            switch (mZPlay.GetFileFormat(mFileName))
            {
                case TStreamFormat.sfMp3:
                    mFormat = MusicFormat.Mp3;
                    break;
                case TStreamFormat.sfOgg:
                    mFormat = MusicFormat.Ogg;
                    break;
                case TStreamFormat.sfWav:
                    mFormat = MusicFormat.Wav;
                    break;
                case TStreamFormat.sfPCM:
                    mFormat = MusicFormat.PCM;
                    break;
                case TStreamFormat.sfFLAC:
                    mFormat = MusicFormat.Flac;
                    break;
            }
            return mFormat;
        }

        /// <summary>
        /// 音乐混音其值介于0到100之间
        /// </summary>
        public void MixSound(Boolean isMix, int mLValue, int mRValue)
        {
            if (isMix)
            {
                mZPlay.MixChannels(true, Convert.ToUInt32(mLValue), Convert.ToUInt32(mRValue));
            }
            else
            {
                mZPlay.MixChannels(false, Convert.ToUInt32(mLValue), Convert.ToUInt32(mRValue));
            }
        }

    }
}
