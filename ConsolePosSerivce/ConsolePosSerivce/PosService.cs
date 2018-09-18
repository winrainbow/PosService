using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePosSerivce
{
    partial class PosService : ServiceBase
    {
        System.Timers.Timer timer;
        public PosService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.IO.File.AppendAllText(@"D:\Log.txt", " Service Start :" + DateTime.Now.ToString());
            /*timer = new System.Timers.Timer();
            string interval = "5000";
            WriteLog("interval = " + int.Parse(interval));
            timer.Interval = int.Parse(interval);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
            WriteLog("服务开始");*/

        }

        protected override void OnStop()
        {
            System.IO.File.AppendAllText(@"D:\Log.txt", " Service Start :" + DateTime.Now.ToString());
            /*timer.Enabled = false;
            WriteLog("服务停止");*/
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)

        {
            WriteLog("服务正在运行");

        }



        
        protected void WriteLog(string str)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";
            System.IO.StreamWriter sw = null;
            if (!System.IO.File.Exists(filePath))
            {
                sw = File.CreateText(filePath);
            }
            else
            {
                sw = File.AppendText(filePath);
            }
            sw.Write(DateTime.Now.ToString() + "::: " + str + Environment.NewLine);
            sw.Close();
        }
    }
}
