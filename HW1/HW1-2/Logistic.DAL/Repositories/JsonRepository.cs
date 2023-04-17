using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Logistic.Models.Interfaces;
using Logistic.DAL.Interfaces;
using Newtonsoft.Json;

namespace Logistic.DAL
{
    public class JsonRepository<TEntity> : ISerializeRepository<TEntity>
        where TEntity : class
    {
        private string path;
        private string entityType;

        public JsonRepository()
        {
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            path += "\\Data\\Json\\";
            entityType = typeof(TEntity).Name;
        }

        public void Create(List<TEntity> entities)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var jsonName = entityType + "_" + dateTime + ".json";

            string savePath = path + jsonName;

            var json = JsonConvert.SerializeObject(entities, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(savePath))
            {
                sw.WriteLine(json);
            }
        }

        public List<TEntity> Read(string filename)
        {
            string readPath = path + filename;

            using (StreamReader sr = new StreamReader(readPath))
            {
                string json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TEntity>>(json);
            }
        }
    }
}
