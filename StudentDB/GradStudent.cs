// Jason Choi
// TINFO-200B
// L6-OOP GradStudent.cs
// GradStudent.cs is a child class of Student.cs
// It offers new variables (TuitionCredit and FacultyAdvisor)
//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 02212022 hyunc16         creation of a child class
// 03022022 hyunc16         added header and comments

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDB
{
    internal class GradStudent : Student
    {
        // properties
        public decimal TuitionCredit { get; set; }
        public string FacultyAdvisor { get; set; }

        public GradStudent(string first, string last, double gpa, string email, DateTime enrolled,
                        decimal credit, string advisor)
            : base(new ContactInfo(first, last, email), gpa, enrolled)  // original 5 args are passed up to the student
        {
            // 2 new specific props for a grad, will be stored here in the derived class
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // override with an expression-bodied method - lambda expression
        public override string ToString() => base.ToString() + $"    Credit: {TuitionCredit:C}\n   Advisor: {FacultyAdvisor}\n";

        public override string ToStringForOutputFile()
        {
            // 1 - create a buffer to hold the built up info for printing
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();

            // 2 - build up the buffer with the object(s) data
            str += $"{TuitionCredit}\n";
            str += $"{FacultyAdvisor}";

            // 3 - return the buffer as a string
            return str;
        }
    }
}
