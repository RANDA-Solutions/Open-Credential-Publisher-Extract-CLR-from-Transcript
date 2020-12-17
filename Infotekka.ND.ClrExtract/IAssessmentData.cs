using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotekka.ND.ClrExtract
{
    public interface IAssessmentData
    {
        string Name { get; }

        string AssessmentType { get; }

        string Description { get; }

        DateTime DateTaken { get; }

        Dictionary<string, decimal> CategoryScores { get; }
    }
}
