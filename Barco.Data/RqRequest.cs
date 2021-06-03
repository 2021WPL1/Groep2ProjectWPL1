using System;
using System.Collections.Generic;
namespace Barco
{
    public partial class RqRequest
    {
        public RqRequest()
        {
            PlPlanning = new HashSet<PlPlanning>();
            PlPlanningsKalender = new HashSet<PlPlanningsKalender>();
            RqOptionel = new HashSet<RqOptionel>();
            RqRequestDetail = new HashSet<RqRequestDetail>();
        }
        public int IdRequest { get; set; }
        public string JrNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public string JrStatus { get; set; }
        public string Requester { get; set; }
        public string BarcoDivision { get; set; }
        public string JobNature { get; set; }
        public string EutProjectname { get; set; }
        public string EutPartnumbers { get; set; }
        public string HydraProjectNr { get; set; }
        public DateTime ExpectedEnddate { get; set; }
        public bool? InternRequest { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public bool Battery { get; set; }
        public virtual ICollection<PlPlanning> PlPlanning { get; set; }
        public virtual ICollection<PlPlanningsKalender> PlPlanningsKalender { get; set; }
        public virtual ICollection<RqOptionel> RqOptionel { get; set; }
        public virtual ICollection<RqRequestDetail> RqRequestDetail { get; set; }
    }
}
