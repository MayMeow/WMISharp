using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Newtonsoft.Json;

namespace WMITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MCloud PC Info v0.0.3");
            Console.WriteLine("(c) Martin Kukolos 2017");
            Console.WriteLine("[ DISKS ]");
            getDiskSerial();
            Console.WriteLine("[ NETWORK ADAPTERS ]");
            MotherBoard();
            Console.ReadLine();
        }

        /// <summary>
        /// Returns all disks in computer with serial numbers
        /// </summary>
        static void getDiskSerial()
        {
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            var result = moSearcher.Get();

            foreach (ManagementObject wm_HD in result)
            {
                Console.WriteLine(wm_HD["Model"].ToString() + " - " + wm_HD["SerialNumber"].ToString() + " - " + wm_HD["Status"].ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void MotherBoard ()
        {
            ManagementObjectSearcher mmSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");

            foreach (ManagementObject mm in mmSearcher.Get())
            {                
                if (mm["MACAddress"] != null)
                {
                    IPAddress ipa = new IPAddress();
                    ipa.Description = mm["Description"].ToString();
                    ipa.Mac = mm["MACAddress"].ToString();
                    Console.WriteLine(JsonConvert.SerializeObject(ipa));
                }         
            }          
        }
    }
}
