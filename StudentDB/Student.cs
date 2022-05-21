// Jason Choi
// TINFO-200B
// L6-OOP Student.cs
// Student.cs is a parent class of GradStudent.cs and Undergrad.cs
// It has general info about student such as ContactInfo (separate class), GPA (gradePtAvg), and EnrollmentDate
// EnrollmentDate is automatically generated at the time of student object creation.
//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 02092022 hyunc16         creation of a class
// 02232022 hyunc16         updated first name, last name, and email to separate class ContactInfo
// 03022022 hyunc16         added header and comments
using System;

namespace StudentDB
{

    // a descriptive comment here over each method
    // explaining the purpose or function within the overall system
    internal class Student
    {
        // auto-implemented properties to define student object

        public ContactInfo Info { get; set; }
        private double gradePtAvg;
        public DateTime EnrollmentDate { get; set; }

        //fully specified constructor for making students in the database
        public Student(ContactInfo info, double grades, DateTime enrolled)
        {
            // just assign across the parameters being passed in as args
            Info = info;
            GradePtAvg = grades;
            EnrollmentDate = enrolled;
        }

        // default constructor no args does nothing
        public Student()
        {

        }

        public double GradePtAvg
        {
            get
            {
                return gradePtAvg;
            }
            set
            {
                // range of GPA value 0-4
                if (0 <= value && value <= 4)
                {
                    gradePtAvg = value;
                }
                else
                {
                    // default the value to what is reportable to the registrar's office
                    gradePtAvg = 0.7;
                }
            }
        }




        // ToString method for formatting only data to the output file
        public virtual string ToStringForOutputFile()
        {
            // 1 - create some sort of buffer - to hold the string we are going to build up from the data in the class
            string str = string.Empty;

            // 2 - the hard part - determine which properties/data from the class that you want to include and how you want
            // to format the output
            str += $"{Info.FirstName}\n";
            str += $"{Info.LastName}\n";
            str += $"{GradePtAvg}\n";
            str += $"{Info.EmailAddress}\n";
            str += $"{EnrollmentDate}\n";

            // 3 - return the string/buffer
            return str;
        }

        // override of ToString for formatting in the User Interface
        // includes data from the object and the labels describing the attributes
        public override string ToString()
        {
            // 1 - create some sort of buffer - to hold the string we are going to build up from the data in the class
            string str = string.Empty;

            str += "************** Student Record *************\n";
            // 2 - the hard part - determine which properties/data from the class that you want to include and how you want
            // to format the output
            str += $"First name: {Info.FirstName}\n";
            str += $" Last name: {Info.LastName}\n";
            str += $" Grade Avg: {GradePtAvg:F1}\n"; // one decimal place format
            str += $"     Email: {Info.EmailAddress}\n";
            str += $"  Enrolled: {EnrollmentDate}\n";

            // 3 - return the string/buffer
            return str;


        }
    }
}