﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Logistic.ConsoleClient.Repositories.Interfaces;

namespace Logistic.ConsoleClient.Repositories
{
    public class XmlRepository<TEntity> : IRepositorySerialize<TEntity>
        where TEntity : class
    {
        private string path;
        private string entityType;

        public XmlRepository()
        {
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path += "\\Data\\Xml\\";

            entityType = typeof(TEntity).Name;
        }

        public void Create(List<TEntity> entities)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var xmlName = entityType + "_" + dateTime + ".xml";

            string savePath = path + xmlName;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TEntity>));

            // using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
            // {
            //    xmlSerializer.Serialize(fs, entities);
            //    Console.WriteLine("Object has been serialized");
            // }
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
            };
            using var writer = XmlWriter.Create(savePath, settings);
            xmlSerializer.Serialize(writer, entities);
        }

        public List<TEntity> Read(string filename)
        {
            var serializer = new XmlSerializer(typeof(List<TEntity>));

            // read the XML from a file
            using var stream = new FileStream(path + filename, FileMode.Open);
            return (List<TEntity>)serializer.Deserialize(stream);
            // return returnList;
        }
    }
}
