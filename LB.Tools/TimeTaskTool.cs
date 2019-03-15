using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;


namespace LB.Tools
{
    public class TimeTask
    {
        public event System.Timers.ElapsedEventHandler ExecuteTask;

        //private readonly Time_Task _task = null;
        private System.Timers.Timer _timer = null;
        private int _interval = 1000;

        public int Interval
        {
            set
            {
                _interval = value;
            }
            get
            {
                return _interval;
            }
        }


        public void Start()
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
                _timer.Start();
            }
        }

        protected void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (null != ExecuteTask)
            {
                ExecuteTask(sender, e);
            }
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
