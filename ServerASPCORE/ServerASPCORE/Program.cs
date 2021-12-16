using ClassLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerASPCORE
{
  public class Program
  {
    public static string LoginsFileName = "users.json";
    public static LoginMasClass storedLogins = new LoginMasClass(LoginsFileName);
    public static List<ClassLib.MessageClass> listOfMessages = new List<ClassLib.MessageClass>(); 


    public static void Main(string[] args)
    {
            listOfMessages.Clear();
            ClassLib.MessageClass mes = new ClassLib.MessageClass();
            mes.userName = "System";
            mes.messageText = "Server is running...";
            mes.timeStamp = DateTime.Now.ToString();
            listOfMessages.Add(mes);
            
            
            storedLogins.LoadLogins();
            //storedLogins.RegUser("rusal", "1234");

            CreateHostBuilder(args).Build().Run();

            storedLogins.SaveLogins();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
