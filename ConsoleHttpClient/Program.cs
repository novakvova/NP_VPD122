using ConsoleHttpClient.dto;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ConsoleHttpClient
{
    class Program
    {
        static private string _urlServer = "https://bv012.novakvova.com";
        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            var user = new RegisterUserDTO();
            Console.Write("Прізвище: ");
            user.SecondName = "Мельник";//Console.ReadLine();
            Console.Write("Ім'я: ");
            user.FirstName = "Катерина";//Console.ReadLine();
            Console.Write("Пошта: ");
            user.Email = "kateryna1@gmail.com";//Console.ReadLine();
            Console.Write("Телефон: ");
            user.Phone = "096 85 45 785";//Console.ReadLine();
            Console.Write("Пароль: ");
            user.Password = "123456";//Console.ReadLine();
            user.ConfirmPassword = user.Password;
            Console.Write("Image: ");
            string image = Console.ReadLine();
            user.Photo = ImageToBase64(image);
            
            
            RegisterUser(user);
            ReadData();
        }

        private static string ImageToBase64(string path)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        private static void ReadData()
        {
            WebRequest request = 
                WebRequest.Create($"{_urlServer}/api/Account/users");
            request.Method = "GET";
            request.ContentType= "application/json";
            try
            {
                var response = request.GetResponse();
                using(var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    //Console.WriteLine(data);
                    var users = JsonConvert.DeserializeObject<List<UserItemDTO>>(data);
                    foreach(var user in users)
                    {
                        Console.WriteLine("Email: {0}", user.Email);
                        Console.WriteLine("Phone: {0}", user.Phone);
                        Console.WriteLine("Photo: {0}", user.Photo);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
        private static void RegisterUser(RegisterUserDTO registerUser)
        {
            string json = JsonConvert.SerializeObject(registerUser);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            WebRequest request =
                WebRequest.Create($"{_urlServer}/api/account/register");
            request.Method = "POST";
            request.ContentType = "application/json";
            using(Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                var response = request.GetResponse();
                using(var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    Console.WriteLine(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
    }
}

