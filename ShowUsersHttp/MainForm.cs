using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Net;

namespace ShowUsersHttp
{
    public partial class MainForm : Form
    {
        private string _urlServer = "https://bv012.novakvova.com";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lvUsers.Clear();
            lvUsers.LargeImageList = new ImageList();
            lvUsers.LargeImageList.ImageSize = new Size(200, 150);
            var users = ReadData();
            foreach(var user in users) {
                string id = user.Id.ToString();
                string image = "no_image.jpg";

                using (WebClient client = new WebClient())
                {
                    string url = $"{_urlServer}{user.Photo}";
                    //byte[] data = client.DownloadData(user.Image);
                    using (Stream stream = client.OpenRead(url))
                    {
                        //pictureBox1.Image = Image.FromStream((Stream)stream);
                        //break;

                        lvUsers.LargeImageList.Images.Add(id,Image.FromStream(stream));
                        ListViewItem item = new ListViewItem();
                        item.Text = user.FirstName + " " + user.SecondName;
                        item.Tag = user;
                        item.ImageKey = id;
                        
                        lvUsers.Items.Add(item);
                        
                    }
                }
            }
        }

        private List<UserItemDTO> ReadData()
        {
            WebRequest request =
                WebRequest.Create($"{_urlServer}/api/Account/users");
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    //Console.WriteLine(data);
                    var users = JsonConvert.DeserializeObject<List<UserItemDTO>>(data);
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}