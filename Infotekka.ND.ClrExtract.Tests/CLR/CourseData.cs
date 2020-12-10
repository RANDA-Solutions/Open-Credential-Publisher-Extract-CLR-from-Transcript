using System;
using System.Collections.Generic;
using System.Text;

namespace Infotekka.ND.ClrExtract.Tests.CLR
{
    class CourseData : ICourseData
    {
        public bool CoreCourse { get; set; }

        public string StateDescriptiveCode { get; set; }

        public string GradeLevel { get; set; }

        public decimal CreditsReceived { get; set; }

        public int CreditsAttempted { get; set; }

        public string TermName { get; set; }

        public DateTime DateAwarded { get; set; }

        public string CourseTitle { get; set; }

        public string StateSubjectDesc { get; set; }

        public string LetterGrade { get; set; }

        public bool DualCredit { get; set; }

        public bool ApCourse { get; set; }

        public bool CteCourse { get; set; }

        public bool ExcludeFromGpa { get; set; }

        public string StateCourseId { get; set; }
    }
}
