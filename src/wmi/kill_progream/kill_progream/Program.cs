using System;
using System.Text;
using System.Threading;
using System.Management;
using System.Security;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using System.Diagnostics;

namespace WMITest
{
    class Program
    {
         // This is just a sample script. Paste your real code (javascript or HTML) here.  
static void Main(string[] args)
        {

            string computer;
            string domain = "";
            string username = "Administrator";

            string password;

            Console.WriteLine("Enter PC IP Address:");
            //computer = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter Password:");
            password = Console.ReadLine();
            Console.Clear();

            try
            {


                string serviceName = "ServiceName";
                string IP = "192.168.60.159"; // remote IP  
                ConnectionOptions connectoptions = new ConnectionOptions();
                connectoptions.Username = username;
                connectoptions.Password = password;
                ManagementScope scope1 = new ManagementScope("\\\\192.168.60.159\\root\\cimv2");
                scope1.Options = connectoptions;
                //WMI query to be executed on the remote machine  
                SelectQuery query1 = new SelectQuery("select * from Win32_Service where name = '" + serviceName + "'");
                using (ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(scope1, query1))
                {
                    ManagementObjectCollection collection = searcher1.Get();
                    foreach (ManagementObject service in collection.OfType<ManagementObject>())
                    {
                        if (service["started"].Equals(true))
                        {
                            //Start the service  
                            service.InvokeMethod("StopService", null);

                        }
                        else
                        {
                            //Stop the service  
                            service.InvokeMethod("StartService", null);

                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }




            ConnectionOptions connection = new ConnectionOptions();
                connection.Username = username;
                connection.Password = password;
                connection.Authority = "ntlmdomain:" + domain;

                ManagementScope scope = new ManagementScope(
                    "\\\\" + "192.168.60.159" + "\\root\\cimv2", connection);
                scope.Connect();
                
                ObjectQuery query = new ObjectQuery(
                    "Select * From Win32_Process");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, query);



                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_Service instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Name: {0}", queryObj["Name"]);
                   
                    if (queryObj["Name"].Equals("notepad.exe"))
                    {
                        Process[] processList = Process.GetProcessesByName("notePad");

                        // 메모장이 실행되어 있지 않을 경우 실행 됩니다.
                        while (processList.Length < 1)
                        {
                            // 메모장 프로세스를 확인 합니다.
                            processList = Process.GetProcessesByName("notePad");
                            Console.WriteLine("Process Search..");
                        }

                        // 메모장 프로세스를 종료 시킵니다.
                        processList[0].Kill();
                    }
                }
                 
                Console.ReadLine();
            }
            catch (ManagementException err)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + err.Message);
            }
            catch (System.UnauthorizedAccessException unauthorizedErr)
            {
                Console.WriteLine("Connection error (user name or password might be incorrect): " + unauthorizedErr.Message);
            }
        }

    }
}