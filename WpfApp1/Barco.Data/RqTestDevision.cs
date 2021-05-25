using System;
using System.Collections.Generic;

namespace Barco.Barco.Data
{
    public partial class RqTestDevision
    {
        public RqTestDevision()
        {
            RqRequestDetail = new HashSet<RqRequestDetail>();
        }

        public string Afkorting { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<RqRequestDetail> RqRequestDetail { get; set; }
    }
}
