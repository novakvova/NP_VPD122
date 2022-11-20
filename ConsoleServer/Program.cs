using System.Net;
using System.Net.Sockets;
using System.Text;

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

Console.Write("Вкажіть порт на якому працює ваш сокет(1098)->_");
int port = int.Parse(Console.ReadLine());

IPEndPoint endPoint = new IPEndPoint(ip_select, port);
Socket server = new Socket(ip_select.AddressFamily,
        SocketType.Stream,
        ProtocolType.Tcp);
try
{
    server.Bind(endPoint);
    server.Listen(1000);
    while(true)
    {
        Console.WriteLine("Очікуємо підклюення клієнтів");
        Socket client = server.Accept(); //метод визивається, якщо до нього підключаєтсья клієнт

        Console.WriteLine("Client info {0}", client.RemoteEndPoint.ToString());

        byte[] dataClient = new byte[1024];
        //приняли дані від клєнта
        client.Receive(dataClient);
        Console.WriteLine("Client info: {0}", Encoding.UTF8.GetString(dataClient));
        byte[] bytes= new byte[1024];
        bytes = Encoding.UTF8.GetBytes("Сервер ваш запит приняв");
        //відпрвили дані клєнту
        client.Send(bytes);
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
catch(Exception ex)
{
    Console.WriteLine("Проблема запуска сервака {0}", ex.Message);
}

