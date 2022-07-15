using System;
using System.Text;
using System.Threading;
using System.Management;
using System.Security;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;



namespace WMITest
{
    class Program
    {
        static void Main(string[] args)
        {

            string computer;
            string domain = "";
            string username = "Administrator";

            string password;

            Console.WriteLine("Enter PC IP Address:");
            computer = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter Password:");
            password = Console.ReadLine();
            Console.Clear();

            try
            {
                ConnectionOptions connection = new ConnectionOptions();
                connection.Username = username;
                connection.Password = password;
                connection.Authority = "ntlmdomain:" + domain;

                ManagementScope scope = new ManagementScope(
                    "\\\\" + computer + "\\root\\cimv2", connection);
                scope.Connect();

                //ObjectQuery query = new ObjectQuery(
                //    "Select * from Win32_Process");
                ObjectQuery query = new ObjectQuery("select * from Win32_Process where Name = '" + "notepad" + ".exe'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);

                // Get object
                ManagementObjectCollection objCollection = searcher.Get();
                foreach (ManagementObject obj in objCollection)
                {
                    obj.InvokeMethod("Terminate", null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}