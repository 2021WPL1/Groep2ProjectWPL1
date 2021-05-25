using System;
namespace Barco.ModelViews.Settings
{
  
    public class ConfigurationQueryResult<TQueryResult>
    {
        public Exception Error { get; set; }
        public TQueryResult QueryResult { get; set; }
        public QueryStatus Status { get; set; }
    }
}