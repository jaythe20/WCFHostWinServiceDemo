using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ServiceClient.CalcClient;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)

        {
            try
            {
                CalculatorServiceClient cs = new CalculatorServiceClient();
                double result = cs.Add(10.0, 20.0);
                Console.WriteLine("Result" + result);
                double result2 = cs.Divide(10.0, 0.0);

                //string path = Path.Combine(Path.GetTempPath(), "AdminChecker.exe");

                ////var code = String.Format(@"if (File.Exists(@""{0}""))
                ////                {{
                ////                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                ////                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                ////                    p.StartInfo.FileName = @""{0}"";
                ////                    p.Start();
                ////                }}", path);
                //var res = cs.RunAsAdmin(path);

                //if (res != null)
                //    Console.WriteLine("Error "+res.ToString());

                Console.WriteLine("Result2" + result2);


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error "+ex.Message);
                Console.ReadLine();
            }




        }
    }
}
