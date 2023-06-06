using Logistic.DAL.Interfaces;
using Newtonsoft.Json;

namespace Logistic.DAL
{
    public class JsonRepository<TEntity> : ISerializeRepository<TEntity>
        where TEntity : class
    {
        public string path;
        private string entityType;

        public JsonRepository()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string rootDirectory = currentDirectory;

            while (!string.IsNullOrEmpty(rootDirectory) && !File.Exists(Path.Combine(rootDirectory, "Logistic.sln")))
            {
                rootDirectory = Directory.GetParent(rootDirectory)?.FullName;
            }

            //path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            path = rootDirectory + "\\Data\\Json\\";
            entityType = typeof(TEntity).Name;
        }

        public string Create(List<TEntity> entities)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var jsonName = entityType + "_" + dateTime + ".json";

            string savePath = path + jsonName;

            var json = JsonConvert.SerializeObject(entities, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(savePath))
            {
                sw.WriteLine(json);
            }

            return jsonName;
        }

        public List<TEntity> Read(string filename)
        {
            string readPath = path + filename;

            if(filename.Contains('\\')|| filename.Contains('/'))
            {
                readPath = filename;
            }

            using (StreamReader sr = new StreamReader(readPath))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TEntity>>(json);
            }
        }
    }
}
