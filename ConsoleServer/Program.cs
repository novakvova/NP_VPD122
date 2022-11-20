using System.Net;

Console.WriteLine("Ваш PC "+Dns.GetHostName());
IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

foreach(var ip in ipHostInfo.AddressList)
{
    Console.WriteLine("IP "+ip.ToString());
}