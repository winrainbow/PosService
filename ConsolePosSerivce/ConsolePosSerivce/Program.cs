using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePosSerivce
{
    class Program
    {
        private static string serviceName = "PosService";
        static void Main(string[] args)
        {

            /* if (args.Length > 0 && args[0] == "s")
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new PosService() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Console.WriteLine("这是Windows应用程序");
                Console.WriteLine("请选择，[1]安装服务 [2]卸载服务 [3]退出");
                var rs = int.Parse(Console.ReadLine());
                switch (rs)
                {
                    case 1:
                        //取当前可执行文件路径，加上"s"参数，证明是从windows服务启动该程序
                        var path = Process.GetCurrentProcess().MainModule.FileName + " s";
                        Process.Start("sc", "create myserver binpath= \"" + path + "\" displayName= 我的服务 start= auto");
                        Console.WriteLine("安装成功");
                        Console.Read();
                        break;
                    case 2:
                        Process.Start("sc", "delete myserver");
                        Console.WriteLine("卸载成功");
                        Console.Read();
                        break;
                    case 3: break;
                }

            }*/
            if (args.Length > 0)
            {
                try
                {
                    ServiceBase[] serviceToRun = new ServiceBase[] { new PosService() };
                    ServiceBase.Run(serviceToRun);
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(@"D:\Log.txt", "\nService Start Error：" + DateTime.Now.ToString() + "\n" + ex.Message);
                }
            }
            else
            {
                StartLabel:
                Console.WriteLine("\n\n请选择你要执行的操作：\n\n [ 1 ]:自动部署服务\n\n [ 2 ]:安装服务\n\n [ 3 ]:卸载服务\n\n [ 4 ]:查看服务状态\n\n [ 5 ]:退出");
                Console.WriteLine("\n******************************************");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.NumPad1 || key == ConsoleKey.D1)
                {
                    // TODO 部署服务
                    if (ServiceInstallHelper.IsServiceExisted(serviceName))
                    {
                        Console.WriteLine("服务存在");
                        ServiceInstallHelper.ConfigService(serviceName, false);
                    }
                    if (!ServiceInstallHelper.IsServiceExisted(serviceName))
                    {
                        Console.WriteLine("服务不存在");
                        ServiceInstallHelper.ConfigService(serviceName, true);
                    }
                    ServiceInstallHelper.StartService(serviceName);
                    goto StartLabel;
                }
                else if (key == ConsoleKey.NumPad2 || key == ConsoleKey.D2)
                {
                    // TODO 安装服务
                    if (!ServiceInstallHelper.IsServiceExisted(serviceName))
                    {
                        ServiceInstallHelper.ConfigService(serviceName, true);
                    }
                    else
                    {
                        Console.WriteLine("\n服务已存在......");
                    }
                    goto StartLabel;
                }
                else if (key == ConsoleKey.NumPad3 || key == ConsoleKey.D3)
                {
                    if (ServiceInstallHelper.IsServiceExisted(serviceName))
                    {
                        ServiceInstallHelper.ConfigService(serviceName, false);
                    }
                    else
                    {
                        Console.WriteLine("\n服务不存在......");
                    }

                    // TODO 卸载服务
                    goto StartLabel;
                }
                else if (key == ConsoleKey.NumPad4 || key == ConsoleKey.D4)
                {
                    if (!ServiceInstallHelper.IsServiceExisted(serviceName))
                    {
                        Console.WriteLine("\n服务不存在......");
                    }
                    else
                    {
                        Console.WriteLine("\n服务状态：" + ServiceInstallHelper.GetServiceStatus(serviceName).ToString());
                    }
                    //TODO 查看服务状态
                    goto StartLabel;
                }
                else if (key == ConsoleKey.NumPad5 || key == ConsoleKey.D5)
                {
                }
                else
                {
                    Console.WriteLine("\n请输入一个有效数字");
                    goto StartLabel;
                }
            }
        }
    }
}
