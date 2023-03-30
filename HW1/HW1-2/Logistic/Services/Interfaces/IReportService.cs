using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Models.Interfaces;

namespace Logistic.ConsoleClient.Services.Interfaces
{
    public interface IReportService<TEntity>
        where TEntity : class, IEntity
    {
        void CreateReport(ReportType reportType, List<TEntity> entities);

        List<TEntity> LoadReport(string fileName);
    }
}
