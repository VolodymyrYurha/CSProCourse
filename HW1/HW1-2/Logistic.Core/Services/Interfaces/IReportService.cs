using Logistic.Models.Enums;
using Logistic.Models.Interfaces;

namespace Logistic.Core.Interfaces
{
    public interface IReportService<TEntity>
        where TEntity : class, IEntity
    {
        void CreateReport(ReportType reportType, List<TEntity> entities);

        List<TEntity> LoadReport(string fileName);
    }
}
