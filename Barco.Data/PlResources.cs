using System;
using System.Collections.Generic;
namespace Barco
{
    public partial class PlResources
    {
        public PlResources()
        {
            PlPlanningsKalender = new HashSet<PlPlanningsKalender>();
            PlResourcesDivision = new HashSet<PlResourcesDivision>();
        }
        public int Id { get; set; }
        public string Naam { get; set; }
        public string KleurRgb { get; set; }
        public string KleurHex { get; set; }
        public virtual ICollection<PlPlanningsKalender> PlPlanningsKalender { get; set; }
        public virtual ICollection<PlResourcesDivision> PlResourcesDivision { get; set; }
    }
}
