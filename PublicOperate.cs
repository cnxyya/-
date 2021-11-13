using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace SMOperateS
{
    public class PublicOperate
    {
        /// <summary>
        /// 重启软件
        /// </summary>
        public static void RestartApp() {
            string strAppFileName = Process.GetCurrentProcess().MainModule.FileName;
            Process myNewProcess = new Process();
            myNewProcess.StartInfo.FileName = strAppFileName;
            myNewProcess.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            myNewProcess.Start();
            Environment.Exit(0);
        }
        /// <summary>
        /// 读取配置文件所有信息
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <returns></returns>
        public static string GetFileContents(string path) {
            string res = null;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        res += line;
                    }
                }
            }
            catch {
                return "error";
            }
            return res;
        }
    }
}