using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace LB.Tools
{
    internal class HardTime
    {
        private long baseTime = 0L;
        private long elapsedTime = 0L;
        private long ticksPerSecond = 0L;

        public HardTime()
        {
            if (!QueryPerformanceFrequency(ref this.ticksPerSecond))
            {
                throw new ApplicationException("Timer: Performance Frequency Unavailable");
            }
            this.Reset();
        }

        public int GetAbsoluteIntTime()
        {
            long performanceCount = 0L;
            QueryPerformanceCounter(ref performanceCount);
            return (int)(performanceCount % this.ticksPerSecond);
        }

        public double GetAbsoluteTime()
        {
            long performanceCount = 0L;
            QueryPerformanceCounter(ref performanceCount);
            return (((double)performanceCount) / ((double)this.ticksPerSecond));
        }

        public double GetElapsedTime()
        {
            long performanceCount = 0L;
            QueryPerformanceCounter(ref performanceCount);
            double num2 = ((double)(performanceCount - this.elapsedTime)) / ((double)this.ticksPerSecond);
            this.elapsedTime = performanceCount;
            return num2;
        }

        public double GetTime()
        {
            long performanceCount = 0L;
            QueryPerformanceCounter(ref performanceCount);
            return (((double)(performanceCount - this.baseTime)) / ((double)this.ticksPerSecond));
        }

        [SuppressUnmanagedCodeSecurity, DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long PerformanceCount);
        [SuppressUnmanagedCodeSecurity, DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);
        public void Reset()
        {
            long performanceCount = 0L;
            QueryPerformanceCounter(ref performanceCount);
            this.baseTime = performanceCount;
            this.elapsedTime = 0L;
        }
    }
}
