// Jason Choi
// TINFO-200B
// L6-OOP ContactInfo.cs
// ContactInfo.cs holds general contact info (FirstName, LastName, and emailAddress)
// Email address is a primary key to the student record. It cannot be editted.
//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 02232022 hyunc16         creation of a class
// 03022022 hyunc16         added header and comments

namespace StudentDB
{
    public class ContactInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string emailAddress;

        // fully specified all aprams will get values
        public ContactInfo(string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            emailAddress = email;
        }


        public string EmailAddress
        {
            get
            {
                // not changing the get from the auto-implemented version
                return emailAddress;
            }
            set
            {
                // email address must contain @ and longer than 3 characters
                if (value.Contains("@") && value.Length > 3)
                {
                    emailAddress = value;
                }
                else
                {
                    emailAddress = "Error: Invliad email.";
                }
            }
        }
        // lamda expression-bodied method for utility printing out a contact info
        public override string ToString() => $"{FirstName} {LastName} \n{EmailAddress}\n";

        public string ToStringLegal() => $"{LastName}, {FirstName}, {EmailAddress}\n";

    }
}