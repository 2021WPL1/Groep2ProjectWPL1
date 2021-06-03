using System;
using System.Collections.Generic;
using System.Text;
namespace Barco.Data
{
   public class ComboObject
   {
       public RqRequestDetail  RqRequestDetail { get; set; }
       public RqOptionel RqOptionel { get; set; }
       public RqRequest Request { get; set; }
       public Eut Eut { get; set; }
       public string PvgResp { get; set; }
       public string TestDivisie { get; set; }
        public string EutNr { get; set; }
   }


}
