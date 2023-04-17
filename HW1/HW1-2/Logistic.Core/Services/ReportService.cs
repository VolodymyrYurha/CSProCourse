using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using Logistic.DAL;
using Logistic.DAL.Interfaces;
using Logistic.Core.Interfaces;
using Newtonsoft.Json.Linq;

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
