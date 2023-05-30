using System.Xml;
using System.Xml.Serialization;
using Logistic.DAL.Interfaces;

namespace Logistic.DAL
{
    public class XmlRepository<TEntity> : ISerializeRepository<TEntity>
        where TEntity : class
    {
        private string path;
        private string entityType;

        public XmlRepository()
        {
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            path += "\\Data\\Xml\\";

            entityType = typeof(TEntity).Name;
        }

        public string Create(List<TEntity> entities)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var xmlName = entityType + "_" + dateTime + ".xml";

            string savePath = path + xmlName;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TEntity>));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
            };
            using var writer = XmlWriter.Create(savePath, settings);
            xmlSerializer.Serialize(writer, entities);

            return xmlName;
        }

        public List<TEntity> Read(string filename)
        {
            var serializer = new XmlSerializer(typeof(List<TEntity>));
            var readPath = path + filename;
            if (filename.Contains('\\') || filename.Contains('/'))
            {
                readPath = filename;
            }
            using var stream = new FileStream(readPath, FileMode.Open);
            return (List<TEntity>)serializer.Deserialize(stream);
        }
    }
}
