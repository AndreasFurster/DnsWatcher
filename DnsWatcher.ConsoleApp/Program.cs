using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Net.Sockets;

namespace DnsWatcher.ConsoleApp
{
    class Program
    {
        private static Dictionary<string, string> _currentIps = new Dictionary<string, string>();
        private static int _padLength = 0;

        public static void Main(string[] args)
        {
            // Dev
            //args = new string[]
            //{
            //    "google.com",
            //    "google.nl",
            //    "-w"
            //};

            if (!args.Any())
            {
                Console.WriteLine("You should provide parameters");
                return;
            }

            var hostnames = args.ToList();

            // Prefix www
            if (hostnames.Contains("-w"))
            {
                hostnames.Remove("-w");
                var prefixedHostnames = new List<string>();

                foreach (var hostname in hostnames)
                {
                    prefixedHostnames.Add($"www.{hostname}");
                }

                hostnames.AddRange(prefixedHostnames);
            }

            ServicePointManager.DnsRefreshTimeout = 0;
            CheckHostnameAsync(hostnames);
        }

        private static void CheckHostnameAsync(List<string> hostnames)
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
                ip = string.Join(", ", ips.Select(ip => ip.ToString().PadRight(15)));
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
