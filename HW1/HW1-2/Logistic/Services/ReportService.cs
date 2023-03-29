using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Models.Interfaces;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Repositories.Interfaces;
using Logistic.ConsoleClient.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace Logistic.ConsoleClient.Services
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

        public void CreateReport(ReportType reportType, List<TEntity> entities)
        {
            if (reportType == ReportType.Json)
            {
                jsonRepository.Create(entities);
            }
            else
            {
                xmlRepository.Create(entities);
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
    }
}
