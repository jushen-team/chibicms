using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ChibiCmsWeb.Helpers
{
    public static class ShellHelper
    {
        /// <summary>
        /// run a cmd command in windows
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string Cmd(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();
            return result;
        }

        /// <summary>
        /// run a bash command in linux
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();
            return result;
        }

        /// <summary>
        /// run a bash command in linux
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string RunCommand(this string cmd)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return cmd.Bash();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return cmd.Cmd();
            }
            throw new Exception("OS Not supporteds");
        }
    }
}





 
