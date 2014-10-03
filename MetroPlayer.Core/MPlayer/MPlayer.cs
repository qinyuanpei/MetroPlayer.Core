using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroPlayer.Core.Base;

namespace MetroPlayer.Core.MPlayer
{

    /// <summary>
    /// 基于MCI接口的播放器类，目前仅支持mp3和wav两种格式的文件
    /// </summary>
    public class MPlayer
    {
        //定义歌曲地址
        private string Url=string.Empty;
        //定义歌曲结构的实例
        private Media m = new Media();
        
        /// <summary>
        /// MPlayer构造函数
        /// </summary>
        public MPlayer()
        {
            m.Position = 0;
            m.State = PlayState.Stopped;
            m.Volume = 0;
            m.Speed = 1000;
            m.Duration = 0;
        }


        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            set
            {
                Url = string.Empty;
                string Name = value;
                m.Position = 0;
                Url = Url.PadLeft(260, ' ');
                WAPI.GetShortPathName(Name,Url,Url.Length);
                Url= GetFileUrl(Url);
                InitDevice(); 
            }
        }

        /// <summary>
        /// 设备初始化
        /// </summary>
        private void InitDevice()
        {
            string DeviceID = GetDeviceID(Url);//返回类型 
            WAPI.mciSendString("Close All", null, 0, 0);//关闭所有设备
            if (DeviceID != "RealPlay")
            {
                string MciCommand = String.Format("Open {0} type {1} Alias Media", Url, DeviceID);
                WAPI.mciSendString(MciCommand, null, 0, 0);//初始化设备
                m.State = PlayState.Stopped;
            }
        }

        /// <summary>
        /// 获取设备ID
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        private string GetDeviceID(string Url)
        {
            string Result = string.Empty;
            Url = Url.ToUpper().Trim();
            if (Url.Length < 3)
            {
                return Url;
            }
            switch (Url.Substring(Url.Length - 3))
            {
                case "MID":
                    Result = "Sequencer";
                    break;
                case "RMI":
                    Result = "Sequencer";
                    break;
                case "IDI":
                    Result = "Sequencer";
                    break;
                case "WAV":
                    Result = "Waveaudio";
                    break;
                case "ASX":
                    Result = "MPEGVideo2";
                    break;
                case "IVF":
                    Result = "MPEGVideo2";
                    break;
                case "LSF":
                    Result = "MPEGVideo2";
                    break;
                case "LSX":
                    Result = "MPEGVideo2";
                    break;
                case "P2V":
                    Result = "MPEGVideo2";
                    break;
                case "WAX":
                    Result = "MPEGVideo2";
                    break;
                case "WVX":
                    Result = "MPEGVideo2";
                    break;
                case ".WM":
                    Result = "MPEGVideo2";
                    break;
                case "WMX":
                    Result = "MPEGVideo2";
                    break;
                case "WMP":
                    Result = "MPEGVideo2";
                    break;
                case ".RM":
                    Result = "RealPlay";
                    break;
                case "RAM":
                    Result = "RealPlay";
                    break;
                case ".RA":
                    Result = "RealPlay";
                    break;
                case "MVB":
                    Result = "RealPlay";
                    break;
                default:
                    Result = "MPEGVideo";
                    break;
            }
            return Result;
        }

       /// <summary>
       /// 获取当前路径
       /// </summary>
       /// <param name="FileName">文件</param>
       /// <returns></returns>
        private string GetFileUrl(string FileName)
        {
            FileName = FileName.Trim();//去掉空格
            if(FileName.Length > 1)//判断是否为空(name包含'\0')
            {
                FileName = FileName.Substring(0, FileName.Length - 1);//去掉'\0'
            }
            return FileName;
        }

        /// <summary>
        /// 设备是否准备就绪
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            string Ready = new string(' ', 10);
            WAPI.mciSendString("Status Media Ready", Ready, Ready.Length, 0);
            Ready = Ready.Trim();
            if (Ready.Contains("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            if (m.State != PlayState.Playing)
            {
                WAPI.mciSendString("Play Media", null, 0, 0);
                m.State = PlayState.Playing;
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (m.State != PlayState.Stopped)
            {
                WAPI.mciSendString("Stop Media", null, 0, 0);
                m.State = PlayState.Stopped;
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Puase()
        {
            if (m.State == PlayState.Playing)
            {
                WAPI.mciSendString("Pause Media", null, 0, 0);
                m.State = PlayState.Puased;
            }
        }

        /// <summary>
        /// 恢复播放
        /// </summary>
        public void Resume()
        {
            int RefInt = WAPI.mciSendString("Resume Media", null, 0, 0);
            m.State = PlayState.Playing;
        }

        //属性Duration
        public int Duration
        {
            get
            {
                WAPI.mciSendString("Set Media Time Format Milliseconds", null, 0, 0);//设置时间格式单位为毫秒
                m.Duration = GetDuration();
                return m.Duration;
            }
        }

        private int GetDuration()
        {
            string Length = string.Empty;
            Length = Length.PadLeft(20, ' ');//设置定长字符串long是19位,足够表示时间了
            WAPI.mciSendString("Status Media Length", Length, Length.Length, 0);
            Length = Length.Trim();
            if (Length.Length > 1)
            {
                Length = Length.Substring(0, Length.Length - 1);
                return (int)(long.Parse(Length) / 1000);
            }
            return 0;
        }

        //属性State
        public PlayState State
        {
            get
            {
                return m.State;
            }
        }

        public string PositionString
        {
            get
            {
                string mm=(this.Position / 60).ToString("00");
                string ss=(this.Position % 60).ToString("00");
                return mm + ":" + ss;
            }
        }

        public string DurationString
        {
            get
            {
                string mm = (this.Duration / 60).ToString("00");
                string ss = (this.Duration % 60).ToString("00");
                return mm + ":" + ss;
            }
        }
        //设置获取当前时间 
        public int Position
        {
            get
            {
                string Position = string.Empty;
                Position = Position.PadLeft(20, ' ');//long表示范围是19位
                WAPI.mciSendString("Status Media Position", Position, Position.Length, 0);
                Position = Position.Trim();
                if (Position.Length > 1)
                {
                    m.Position = (int)(long.Parse(Position) / 1000);//以秒返回
                }
                return m.Position;
            }
            set
            {
                string Command = String.Format("Seek Media to {0}", value);
                WAPI.mciSendString(Command, null, 0, 0);//使播放停止
                m.State = PlayState.Stopped;
                m.Position = value;
                Play();
            }
        }

        //设置静音
        public void VolumeOff(bool IsOff)
        {
            string SetOnOff = string.Empty;
            if (IsOff)
                SetOnOff = "Off";
            else
                SetOnOff = "On";
            string MciCommand = String.Format("Set Audio Media {0}", SetOnOff);
            WAPI.mciSendString(MciCommand, null, 0, 0);
        }

        //Volume
        public int Volume
        {
            get
            {
                return m.Volume;
            }
            set
            {
                if (value>= 0)
                {
                    m.Volume = value;
                    string MciCommand = String.Format("Set Audio Media Volume To {0}",m.Volume);
                    WAPI.mciSendString(MciCommand, null, 0, 0);
                }
            }
        }
        //定义歌曲结构
        private struct Media
        {
            public int Position;
            public PlayState State;
            public int Volume;
            public int Speed;
            public int Duration;
        }




    }
}
