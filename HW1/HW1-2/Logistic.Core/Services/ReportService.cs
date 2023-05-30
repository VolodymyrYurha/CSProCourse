using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using Logistic.DAL;
using Logistic.Core.Interfaces;

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
    }
}
