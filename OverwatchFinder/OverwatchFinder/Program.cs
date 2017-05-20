using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PcapDotNet.Core;

namespace OverwatchFinder
{
    class Program
    {
        static Sniffer Sniffer;
        static string ConfigFileName = "config.txt";
        
        [STAThread] //Required to safely access the clipboard
        static void Main(string[] args)
        {
            //Choose the network device to use and construct a sniffer instance
            Sniffer = new Sniffer(ChooseDevice());
            Sniffer.Start();

            //Do nothing while we are listening (until we have received the necessary packets)
            while (Sniffer.Running)
            {
                System.Threading.Thread.Sleep(100);
            }

            //Sniffer.URL will be empty in case of failure
            if (string.IsNullOrEmpty(Sniffer.URL))
            {
                Exit("URL not found.");
            }

            Console.WriteLine("URL: {0}", Sniffer.URL);
            DemoAnalyzer.StartDownload(Sniffer.URL);
            while(!DemoAnalyzer.Finished)
                System.Threading.Thread.Sleep(100);

            SuspectFinder.Start();
            while (!SuspectFinder.SuspectFound)
                System.Threading.Thread.Sleep(100);
            
            if (Ask("Copy SteamID to clipboard?", true))
            {
                Clipboard.SetText(SuspectFinder.SuspectSteamURL);
            }
            if (Ask("Open SteamID in browser?", true))
            {
                Process.Start(SuspectFinder.SuspectSteamURL);
            }
            if(Ask("Delete downloaded files?", false)){
                DemoAnalyzer.DeleteFiles();
            }
            Exit();
        }

        /// <summary>
        /// Ask a simple yes/no dialog question.
        /// </summary>
        /// <param name="question">The question to ask (including '?' if wanted)</param>
        /// <param name="defaultAnswer">The default answer to the question (true = yes, false = no).</param>
        /// <returns>The users answer, or false in case of invalid input</returns>
        static bool Ask(string question, bool defaultAnswer)
        {
            Console.Write("{0} ({1}/{2}): ", question, (defaultAnswer ? "Y" : "y"), (!defaultAnswer ? "N" : "n"));
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return defaultAnswer;
            }

            if(input.ToLower() == "y")
            {
                return true;
            }

            if(input.ToLower() == "n")
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Displays a dialog to choose a network device.
        /// </summary>
        /// <returns>The chosen network device.</returns>
        static PacketDevice ChooseDevice()
        {
            //Get all available network devices.
            var Devices = LivePacketDevice.AllLocalMachine;

            //Read config if exists to auto choose a device.
            if(File.Exists(ConfigFileName))
            {
                string configDevice = File.ReadAllText(ConfigFileName);
                var device = Devices.Where(x => x.Name.Equals(configDevice));
                if(device.Count() == 1)
                {
                    Console.WriteLine("Automatically choosing device based on config.txt.");
                    Console.WriteLine("Delete that file to reenable manual device choice.");
                    return device.First();
                }else
                {
                    Console.WriteLine("Device from config was not found.");
                    File.Delete(ConfigFileName);
                }
            }

            //Matches some (all?) device descriptions to simplify the strings.
            Regex deviceDescriptionMatcher = new Regex(@"Network adapter '(.*?)' on local host");

            //Halt execution if no devices present.
            if (Devices.Count == 0)
            {
                Exit("No network devices found.");
            }

            //Output all device descriptions (i.e. friendly names)
            for (int i = 0; i < Devices.Count; i++)
            {
                var dev = Devices[i];
                if (dev.Description != null)
                {
                    string desc = dev.Description;

                    //Simplify string if verbose description present.
                    var match = deviceDescriptionMatcher.Match(desc);
                    if(match.Success)
                    {
                        desc = match.Groups[1].Value;
                    }

                    Console.WriteLine("({0}): {1}", i, desc);
                }
            }

            //Let the user choose a device through it's index in the collection.
            Console.Write("Choose device: ");
            var input = Console.ReadLine();
            int index;

            if (!int.TryParse(input, out index) || index < 0 || index >= Devices.Count)
            {
                Exit("Invalid input.");
            }

            if(Ask("Define this device as default?", false))
            {
                if(File.Exists(ConfigFileName))
                {
                    File.Delete(ConfigFileName);
                }
                File.WriteAllText(ConfigFileName, Devices[index].Name);
            }
            return Devices[index];
        }

        /// <summary>
        /// Prints a reason (optional) and then exits the application after a key press.
        /// </summary>
        /// <param name="reason">The reason to display before exiting.</param>
        static void Exit(string reason = "")
        {
            if(Sniffer != null && Sniffer.Running) Sniffer.Stop();

            if(!string.IsNullOrEmpty(reason))
            {
                Console.WriteLine(reason);
            }
            
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
