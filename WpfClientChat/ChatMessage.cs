using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WpfClientChat
{
    public enum TypeMessage
    {
        Login,
        Logout,
        Message
    }
    public class ChatMessage
    {
        public TypeMessage MessageType;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Images { get; set; }
        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(m))
                {
                    int int_value = (int)MessageType;
                    writer.Write(int_value);
                    writer.Write(UserId);
                    writer.Write(UserName);
                    writer.Write(Text);
                    writer.Write(Images);
                }
                return m.ToArray();
            }
        }
        public static ChatMessage Desserialize(byte[] data)
        {
            ChatMessage message = new ChatMessage();
            using(MemoryStream m = new MemoryStream(data))
            {
                using(BinaryReader reader = new BinaryReader(m))
                {
                    message.MessageType = (TypeMessage)reader.ReadInt32();
                    message.UserId = reader.ReadString();
                    message.UserName = reader.ReadString();
                    message.Text = reader.ReadString();
                    message.Images = reader.ReadString();
                }
            }
            return message;
        }
    }
}
