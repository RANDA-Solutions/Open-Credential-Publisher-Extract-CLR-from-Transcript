using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotekka.ND.ClrExtract
{
    public interface IGpaClassRankData
    {
        string Description { get; }

        decimal? GPA { get; }

        int? ClassRank { get; }

        int? ClassSize { get; }
    }
}
