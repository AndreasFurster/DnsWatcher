using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace DnsWatcher.ConsoleApp
{
    class Program
    {
        private static Dictionary<string, string> _currentIps = new Dictionary<string, string>();
        private static int _padLength = 0;

        public static void Main(string[] args)
        {
            ServicePointManager.DnsRefreshTimeout = 0;

            CheckHostnameAsync(args);
        }

        private static void CheckHostnameAsync(string[] hostnames)
        {
            _padLength = hostnames.Max(h => h.Length);

            foreach (var hostname in hostnames)
            {
                PrintIp(hostname);
            }

            Thread.Sleep(3000);
            CheckHostnameAsync(hostnames);
        }

        private static void PrintIp(string hostname)
        {
            string ip = null;
            try
            {
                var ips = Dns.GetHostAddresses(hostname);
                ip = ips.First().ToString();
            }
            catch (Exception e)
            {
                ip = e.Message;
            }

            Console.WriteLine($"{DateTime.Now} | {hostname.PadRight(_padLength)} -> {ip}");

            if (!_currentIps.ContainsKey(hostname))
            {
                _currentIps.Add(hostname, ip);
            }
            else if (_currentIps[hostname] != ip)
            {
                MessageBox((IntPtr)0, ip, $"Ip of {hostname} changed...", 0);
                _currentIps[hostname] = ip;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);
    }
}
