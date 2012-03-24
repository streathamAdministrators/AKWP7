using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.OptionService
{
    public class OptionDto
    {
        public bool IsPassword { get; set; }
        public bool IsSynchro { get; set; }
        public bool IsPrimaryTile { get; set; }
        public bool Isreport { get; set; }
        public bool IsComparator { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
