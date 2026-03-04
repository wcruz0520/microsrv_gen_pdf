using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalService
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //string baseAddress = "http://127.0.0.1:5055/";
            string baseAddress = "http://localhost:5055";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("CrystalService running on " + baseAddress);
                Console.WriteLine("Press ENTER to stop...");
                Console.ReadLine();
            }
        }
    }
}