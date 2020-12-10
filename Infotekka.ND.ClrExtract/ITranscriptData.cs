using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotekka.ND.ClrExtract
{
    public interface ITranscriptData
    {
        string SchoolName { get; }

        string Principal { get; }

        IAddressData SchoolAddress { get; }

        IIdentityData[] SchoolIds { get; }

        string SchoolPhone { get; }

        bool Graduated { get; }

        DateTime? GraduationDate { get; }

        DateTime? CivicsTest { get; }  //ND only

        IGpaClassRankData[] GPAs { get; }

        /// <summary>
        /// Standard college admission GPA
        /// </summary>
        decimal GpaWeightedCalc { get; }

        decimal? NdusGpa { get; } //ND only

        DateTime? SchoolEntryDate { get; }

        DateTime? SchoolExitDate { get; }

        string WithdrawalReason { get; }

        string FirstName { get; }

        string MiddleName { get; }

        string LastName { get; }

        string GradeLevel { get; }

        DateTime DateOfBirth { get; }

        /// <summary>
        /// Learner Sourced system primary key
        /// </summary>
        string SourcedId { get; }

        string StudentId { get; }

        IIdentityData[] StudentIds { get; }

        string StudentPhone { get; }

        IAddressData StudentAddress { get; }

        string DistrictName { get; }

        IIdentityData[] DistrictIds { get; }

        IAddressData DistrictAddress { get; }
    }
}
