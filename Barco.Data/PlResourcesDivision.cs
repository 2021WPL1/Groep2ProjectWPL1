using System;
using System.Collections.Generic;

namespace Barco
{
    public partial class PlResourcesDivision
    {
        public int Id { get; set; }
        public int ResourcesId { get; set; }
        public string DivisionAfkorting { get; set; }

        public virtual PlResources Resources { get; set; }
    }
}
