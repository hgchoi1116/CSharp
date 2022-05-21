// Jason Choi
// TINFO-200B
// L6-OOP Undergrad.cs
// Undergrad.cs is a child class of Student.cs
// It offers new variables (Rank and DegreeMajor) and enum YearRank
//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 02212022 hyunc16         creation of a child class
// 03022022 hyunc16         added header and comments
using System;

namespace StudentDB
{
    // enumerated type for c# represents year/class in school
    public enum YearRank
    {
        Freshman = 1,       // specify integer value for the variable
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }
    // inheritance 
    internal class Undergrad : Student
    {
        // properties
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }

        // full-spec constructor
        public Undergrad(string first, string last, double gpa, string email, DateTime enrolled,
                        YearRank year, string major)
            : base(new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = year;
            DegreeMajor = major;
        }


        // using an expression-bodied method for the override to string - undergrad
        public override string ToString() => base.ToString() + $"      Year: {Rank}\n     Major: {DegreeMajor}\n";

        // print out the RTTI info - grab all data from the base class
        // finally, print the  new props
        public override string ToStringForOutputFile()
        {
            // 1 - create a buffer to hold the built up info for printing
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();

            // 2 - build up the buffer with the object(s) data
            str += $"{Rank}\n";
            str += $"{DegreeMajor}";

            // 3 - return the buffer as a string
            return str;
        }



    }
}
