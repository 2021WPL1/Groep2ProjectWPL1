using System;
namespace Barco.ModelViews.Settings
{
  //bianca-generic class for the conversion of the result of the TQueryResult
    public class ConfigurationQueryResult<TQueryResult>
    {
        public Exception Error { get; set; }
        public TQueryResult QueryResult { get; set; }
        public QueryStatus Status { get; set; }
    }
}