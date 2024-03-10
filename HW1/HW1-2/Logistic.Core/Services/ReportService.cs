using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using Logistic.DAL;
using Logistic.Core.Interfaces;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Data.SqlTypes;

namespace Logistic.Core
{
    public class ReportService<TEntity> : IReportService<TEntity>
        where TEntity : class, IEntity
    {
        private JsonRepository<TEntity> jsonRepository;
        private XmlRepository<TEntity> xmlRepository;

        public ReportService(JsonRepository<TEntity> jsonRepository, XmlRepository<TEntity> xmlRepository)
        {
            this.jsonRepository = jsonRepository;
            this.xmlRepository = xmlRepository;
        }

        public string CreateReport(ReportType reportType, List<TEntity> entities)
        {
            if (reportType == ReportType.Json)
            {
                return jsonRepository.Create(entities);
            }
            else
            {
                return xmlRepository.Create(entities);
            }
        }

        public List<TEntity> LoadReport(string fileName)
        {
            if (fileName.EndsWith(".json"))
            {
                return jsonRepository.Read(fileName);
            }

            if (fileName.EndsWith(".xml"))
            {
                return xmlRepository.Read(fileName);
            }

            throw new Exception("invalid file format");
        }

        public byte[] LoadFile(string file)
        {
            string jsonPath, xmlPath;
            jsonPath = jsonRepository.path;
            xmlPath = xmlRepository.path;

            string readPath;
            if (file.EndsWith(".json"))
            {
                readPath = jsonPath + file;
            }
            else
            {
                readPath = xmlPath + file;
            }

            if (file.Contains('\\') || file.Contains('/'))
            {
                readPath = file;
            }

            using (StreamReader sr = new StreamReader(readPath))
            {
                string content = sr.ReadToEnd();
                return Encoding.UTF8.GetBytes(content);
            }
        }

        public string Serialize(List<TEntity> entitiesList, ReportType reportType)
        {
            if(reportType == ReportType.Json)
            {
                return jsonRepository.Serialize(entitiesList);
            }
            else
            {
                return xmlRepository.Serialize(entitiesList);
            }
        }

        public List<TEntity> Deserialize(string serializedData, ReportType reportType)
        {
            if (reportType == ReportType.Json)
            {
                return jsonRepository.Deserialize(serializedData);
            }
            else
            {
                return xmlRepository.Deserialize(serializedData);
            }
        }
    }
}
