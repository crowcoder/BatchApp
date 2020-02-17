using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;

namespace BatchApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                Environment.Exit(-1);
            }

            byte[] xml = await File.ReadAllBytesAsync(args[0]);

            XmlSerializer ser = new XmlSerializer(typeof(FruitModel), new XmlRootAttribute { ElementName = "Fruit" });

            using (MemoryStream ms = new MemoryStream(xml))
            {
                FruitModel model = ser.Deserialize(ms) as FruitModel;
                JsonSerializerOptions jsonOpts = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(model, typeof(FruitModel));
                await File.WriteAllTextAsync(args[1], json);
            }
        }
    }
}
