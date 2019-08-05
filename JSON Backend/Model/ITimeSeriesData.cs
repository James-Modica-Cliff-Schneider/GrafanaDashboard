using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Model
{
    public interface ITimeSeriesData
    {
        Task<List<DeviceCounts>> GetDataTimeSeriesData(DateTime to, DateTime from, string tablename);
        Task<List<DeviceCounts>> GetDataSeriesData(DateTime to, DateTime from, string tablename);
        Task<List<EcoIQTimingCount>> GetDataEcoIQTimeSeriesData(DateTime to, DateTime from, string tablename);
        Task<List<MostCommonProblem>> GetMostCommonRecentProblemsData(string tablename);
    }
}