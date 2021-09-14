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
                Console.WriteLine("DNS Watcher - Watches DNS for changes.");
                Console.WriteLine();
                Console.WriteLine("Please provide the domain(s) and arguments. Seperate by spaces");
                Console.WriteLine("--www, -w          Include www subdomain for all provided domains.");
                Console.WriteLine("--check-once, -o   Only get IP's one time. Do not keep watching.");
                Console.WriteLine();
                Console.Write("Command: ");

                var input = Console.ReadLine();
                Main(input.Split(" "));
                return;
            }

            ProcessDomainInput(args);
        }

        

        private static void ProcessDomainInput(string[] args)
        {
            var hostnames = args.ToList();

            var once = hostnames.Contains("--check-once") || hostnames.Contains("-o");
            hostnames.Remove("--check-once");
            hostnames.Remove("-o");

            // Prefix www
            if (hostnames.Contains("--www") || hostnames.Contains("-w"))
            {
                hostnames.Remove("--wwww");
                hostnames.Remove("-w");

                var prefixedHostnames = new List<string>();

                foreach (var hostname in hostnames)
                {
                    prefixedHostnames.Add($"www.{hostname}");
                }

                hostnames.AddRange(prefixedHostnames);
            }

            if(!hostnames.Any())
            {
                Console.WriteLine("No domains provided");
                return;
            }

            ServicePointManager.DnsRefreshTimeout = 0;
            CheckHostnameAsync(hostnames, once);
        }

        private static void CheckHostnameAsync(List<string> hostnames, bool once = false)
        {
            _padLength = hostnames.Max(h => h.Length);

            foreach (var hostname in hostnames)
            {
                PrintIp(hostname);
            }

            if (once) return;

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
