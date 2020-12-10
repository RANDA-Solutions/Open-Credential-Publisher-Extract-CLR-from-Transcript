using System;
using System.Collections.Generic;
using System.Text;

namespace Infotekka.ND.ClrExtract.Tests.CLR
{
    class GpaClassRankData : IGpaClassRankData
    {
        public string Description { get; set; }

        public decimal? GPA { get; set; }

        public int? ClassRank { get; set; }

        public int? ClassSize { get; set; }
    }
}
