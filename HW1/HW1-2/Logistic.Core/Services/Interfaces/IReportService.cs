﻿using Logistic.Models.Enums;
using Logistic.Models.Interfaces;

namespace Logistic.Core.Interfaces
{
    public interface IReportService<TEntity>
        where TEntity : class, IEntity
    {
        string CreateReport(ReportType reportType, List<TEntity> entities);

        List<TEntity> LoadReport(string file);

        byte[] LoadFile(string file);

        string Serialize(List<TEntity> entitiesList, ReportType reportType);

        List<TEntity> Deserialize(string serializedData, ReportType reportType);
    }
}
