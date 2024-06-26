﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotekka.ND.ClrExtract
{
    public interface ICourseData
    {
        bool CoreCourse { get; }

        string GradeLevel { get; }

        decimal CreditsReceived { get; }

        decimal CreditsAttempted { get; }

        string TermName { get; }

        DateTime DateAwarded { get; }

        string CourseTitle { get; }

        string StateDescriptiveCode { get; }

        string StateSubjectDesc { get; }

        string LocalCourseId { get; }

        string StateCourseId { get; }

        string NationalCourseId { get; }

        string LetterGrade { get; }

        bool DualCredit { get; }

        bool ApCourse { get; }

        bool CteCourse { get; }

        bool ExcludeFromGpa { get; }

        string CourseUri { get; }
    }
}
