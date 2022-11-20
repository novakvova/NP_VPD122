using System.Net;

Console.InputEncoding= System.Text.Encoding.UTF8;
Console.OutputEncoding= System.Text.Encoding.UTF8;
Console.WriteLine("Ваш PC "+Dns.GetHostName());
IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
Console.WriteLine("Оберіть IP адреся для вашого сервера");
int i = 0;
foreach(var ip in ipHostInfo.AddressList)
{
    Console.WriteLine($"{i} - "+ip.ToString());
    i++;
}
Console.Write("->_");
IPAddress ip_select = ipHostInfo.AddressList[int.Parse(Console.ReadLine())];

Console.WriteLine("Ви обрали наступний IP " + ip_select);

