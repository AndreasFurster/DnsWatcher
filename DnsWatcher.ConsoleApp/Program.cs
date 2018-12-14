using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Collections.Generic;

namespace DnsWatcher.ConsoleApp
{
    class Program
    {
        private static Dictionary<string, string> _currentIps = new Dictionary<string, string>();
        private static int _padLength = 0;

        public static void Main(string[] args)
        {
            if(!args.Any())
            {
                Console.WriteLine("You should provide parameters");
                return;
            }

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
            string ip;
            try
            {
                var ips = Dns.GetHostAddresses(hostname);
                ip = ips.First().ToString();
            }
            catch (Exception e)
            {
                ip = e.Message;
            }

            if (!_currentIps.ContainsKey(hostname) || _currentIps[hostname] != ip)
            {
                Console.WriteLine($"{DateTime.Now} | {hostname.PadRight(_padLength)} -> {ip}");
                _currentIps[hostname] = ip;
            }
        }
    }
}
