using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Вкажіть ip сервеа: ");
IPAddress ip = IPAddress.Parse(Console.ReadLine());
Console.WriteLine("Вкажіть порт сервера: ");
int port = int.Parse(Console.ReadLine());

IPEndPoint serverEndPoint = new IPEndPoint(ip, port);

Socket sender = new Socket(ip.AddressFamily, 
    SocketType.Stream, 
    ProtocolType.Tcp);

try
{
    sender.Connect(serverEndPoint);

    byte[] buffer = Encoding.UTF8.GetBytes("Привіт це Вова");
    int byteSent = sender.Send(buffer); //Надсилаємо дані на сервер
    byte[] resultByte = new byte[1024];
    int byteResult = sender.Receive(resultByte); //Оримуємо дані із сервера
    Console.WriteLine("Відповідь від сервера: {0}", 
        Encoding.UTF8.GetString(resultByte, 0, byteResult));
    sender.Shutdown(SocketShutdown.Both);
    sender.Close();
}
catch(Exception ex)
{
    Console.WriteLine("Промика роботи із сервером {0}", ex.ToString());
}