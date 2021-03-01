using System;
using System.Collections.Generic;

namespace Barco.Data
{
    public partial class RqRequestDetail
    {
        public RqRequestDetail()
        {
            Eut = new HashSet<Eut>();
        }

        public int IdRqDetail { get; set; }
        public string Testdivisie { get; set; }
        public string Pvgresp { get; set; }
        public int IdRequest { get; set; }

        public virtual RqRequest IdRequestNavigation { get; set; }
        public virtual RqTestDevision TestdivisieNavigation { get; set; }
        public virtual ICollection<Eut> Eut { get; set; }
    }
}
