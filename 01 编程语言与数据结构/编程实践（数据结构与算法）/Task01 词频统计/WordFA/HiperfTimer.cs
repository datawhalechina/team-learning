using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WordFA
{
    internal class HiperfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpFrequency);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long _startTime, _stopTime;
        private readonly long _freq;
        public HiperfTimer()
        {
            _startTime = 0;
            _stopTime = 0;
            if (QueryPerformanceFrequency(out _freq) == false)
            {
                throw new Win32Exception();
            }
        }
        public void Start()
        {
            Thread.Sleep(0);
            QueryPerformanceCounter(out _startTime);
        }
        public void Stop()
        {
            QueryPerformanceCounter(out _stopTime);
        }


        /// <summary>
        /// 获取从Start()->Stop()中间的精确时间间隔
        /// 单位：秒
        /// 计数次数/计数频率
        /// </summary>
        public double Duration_second
        {
            get
            {
                return (double)(_stopTime - _startTime) / (double)_freq;
            }
        }


        /// <summary>
        /// 获取从Start()->Stop()中间的精确时间间隔
        /// 单位：毫秒
        /// 计数次数/计数频率
        /// </summary>
        public double Duration_ms
        {
            get
            {
                return (double)(_stopTime - _startTime) * 1000 / (double)_freq;
            }
        }
    }
}
