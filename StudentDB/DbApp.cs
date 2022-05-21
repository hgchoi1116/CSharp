// Jason Choi
// TINFO-200B
// L6-OOP DbApp.cs
// DbApp imports a databse (.txt) and provide user options to manipulate List<student> database.
// Create: creates a new student object into the database
// Delete: deletes an existing record on the database
// Find: finds an existing record using email (primary key)
// Print: prints all exising data on the console
// Modify: modify an existing record
//      **cannot modify email and enrollment date. Email is a primary key to the student object. Enrollment Date is generated
//      at the time of the object creation. To change email, delete the current record and create a new record with updated email.
// Save: saves and creates an output (.txt) file
// Exit: exits the program
//////////////////////////////////////////////////////////////
// Change History
// Date     Developer       Description
// 02092022 hyunc16         creation of a new project and created menu selection
// 02142022 hyunc16         added print and save functions and created input and output files
// 02212022 hyunc16         added find function
// 02232022 hyunc16         added create function
// 02282022 hyunc16         added delete function
// 03022022 hyunc16         added modify function and inserted header

using System;
using System.Collections.Generic;
using System.IO;

namespace StudentDB
{
    internal class DbApp
    {
        // use a constant identifier for the file name
        // two files for testing purpose to compare input .txt and output .txt
        public const string STUDENTDB_DATAFILE = "STUDENTDB_DATAFILE.txt";
        public const string STUDENTDB_DATAFILE_OUTPUT = "STUDENTDB_DATAFILE_OUTPUT.txt";
        // constant variable for two data types of child classes
        public const string UNDERGRAD = "StudentDB.Undergrad";
        public const string GRADSTUDENT = "StudentDB.GradStudent";

        // main database storgae for runtime when the DB is executing
        private List<Student> students = new List<Student>();

        public DbApp()
        {
            // seed the databsae if currently testing without an input file
            // SeedDatabaseList();

            // continue testing using the input file
            ReadDataFromInputFile();
        }

        private void ReadDataFromInputFile()
        {
            // 1- construct the objects needed to read in from a file on the disk
            StreamReader inFile = new StreamReader(STUDENTDB_DATAFILE);

            // 2 - use the reader object to continually read data
            string studentType; ;
            while ((studentType = inFile.ReadLine()) != null)
            {
                // for each student in the file
                // reading in a single student record line by line
                string firstName = inFile.ReadLine();
                string lastName = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());
                string email = inFile.ReadLine();
                DateTime date = DateTime.Parse(inFile.ReadLine());



                if (studentType == UNDERGRAD)
                {
                    // read the undergrad additional props
                    YearRank year = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();

                    // make a undergrad student object - 2 step procedure
                    Undergrad undergrad = new Undergrad(firstName, lastName, gpa, email, date, year, major);
                    students.Add(undergrad);
                }
                else if (studentType == GRADSTUDENT)
                {
                    decimal credit = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();

                    // create a grad student object
                    students.Add(new GradStudent(firstName, lastName, gpa, email, date, credit, advisor));
                }
            }

            // 3- release the resources used
            inFile.Close();
        }

        // main database engine application loop
        internal void Run()
        {
            while (true) //always run until Exit is selected
            {
                // display a menu of choices to the user
                DisplayMainMenu();

                // get user selection and execute that module 
                char selection = GetUserSelection();

                switch (selection)
                {
                    case 'C': // [C]reate
                    case 'c':
                        CreateStudentRecord();
                        break;
                    case 'D': // [D]elete
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'F': // [F]ind
                    case 'f':
                        string email = string.Empty;
                        FindStudentRecord(out email);
                        break;
                    case 'P': // [P]rint
                    case 'p':
                        PrintAllStudentRecords();
                        break;
                    case 'M': // [M]odify
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'S': // [S]ave
                    case 's':
                        SaveAllStudentDataToOutputFle();
                        break;
                    case 'E': // [E]xit
                    case 'e':
                        // SaveAllStudentDataToOutputFle();
                        System.Environment.Exit(0); // get out of the application
                        break;
                    default:
                        Console.WriteLine("Error: Sorry that was not a choice. Select Again: ");
                        break;
                }
            }
        }

        private void ModifyStudentRecord()
        {
            // search student record to modify
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu != null) // record found
            {
                bool condition = true;
                while (condition)
                {
                    // modify instruction (email and enrollment date are not edittable)
                    Console.WriteLine("\n****Email and Enrollment Date cannot be modified. To update Email or " +
                        "Enrollment Date,\n****delete the record and create a new record with correct Email.");
                    // modify selection choices 
                    Console.Write("[1] First Name\n[2] Last Name\n[3] GPA\n[4] YearRank for Undergrad" +
                    "(ex. Freshman, Sophmore, Junior, Senior)\n[5] Major for Undergrad\n[6] Tuition Credit for " +
                    "Grad\n[7] Faculty Advisor for Grad\n[8] Exit to Main Menu\n---When done modifying, select \"8\" " +
                    "to exit the modify menu.---\nEnter your selection (number) from the list: ");
                    char selection = GetUserSelection();
                    Console.WriteLine("");
                    // Format on modify menu
                    // 1. the program provides current value
                    // 2. Then, asks for new value.
                    // 3. Lastly, confirms the change by giving feedback to the user.
                    switch (selection)
                    {
                        case '1': // First name
                            Console.WriteLine($"Current first name: {stu.Info.FirstName}"); //current value
                            Console.Write("New First Name: "); // asks for new value
                            string firstName = Console.ReadLine();
                            stu.Info.FirstName = firstName;
                            Console.WriteLine($"**Updated to First Name: {stu.Info.FirstName}**"); //confirm feedback
                            break;
                        case '2': // Last Name
                            Console.WriteLine($"Current Last Name: {stu.Info.LastName}");
                            Console.Write("New Last Name: ");
                            string lastName = Console.ReadLine();
                            stu.Info.LastName = lastName;
                            Console.WriteLine($"**Updated to Last Name: {stu.Info.LastName}**");
                            break;
                        case '3': // GPA
                            Console.WriteLine($"Current GPA: {stu.GradePtAvg}");
                            Console.Write("New GPA: ");
                            double gradePtAvg = double.Parse(Console.ReadLine());
                            stu.GradePtAvg = gradePtAvg;
                            Console.WriteLine($"**Updated to GPA: {stu.GradePtAvg}**");
                            break;
                        case '4': // YearRank for Undergrad
                            if (stu.GetType().ToString() == UNDERGRAD) // verifies student type
                            {
                                // cast stu to Undergrad to access Undergrad variables (Rank)
                                Console.WriteLine($"Current YearRank: {((Undergrad)(stu)).Rank}");
                                Console.Write("[1]Freshman [2]Sophmore [3]Junior [4]Senior\nSelect New YearRank (Enter number): ");
                                ((Undergrad)(stu)).Rank = (YearRank)int.Parse(Console.ReadLine());
                                Console.WriteLine($"**Updated to YearRank: {((Undergrad)(stu)).Rank}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Selection. The record is not an Undergrad Student. Please try again.\n");
                            }
                            break;
                        case '5': // Major for Undergrad
                            if (stu.GetType().ToString() == UNDERGRAD) // verifies student type
                            {
                                // cast stu to Undergrad to access Undergrad variables (DegreeMajor)
                                Console.WriteLine($"Current Major: {((Undergrad)(stu)).DegreeMajor}");
                                Console.Write("New Major: ");
                                ((Undergrad)(stu)).DegreeMajor = Console.ReadLine();
                                Console.WriteLine($"**Updated to Major: {((Undergrad)(stu)).DegreeMajor}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Selection. The record is not an Undergrad Student. Please try again.\n");
                            }
                            break;
                        case '6': // Tution Credit for Grad
                            if (stu.GetType().ToString() == GRADSTUDENT) // verifies student type
                            {
                                Console.WriteLine($"Current Tuition Credit: {((GradStudent)(stu)).TuitionCredit}");
                                Console.Write("New Tuition Credit: ");
                                ((GradStudent)(stu)).TuitionCredit = decimal.Parse(Console.ReadLine());
                                Console.WriteLine($"**Updated to Tution Credit: {((GradStudent)(stu)).TuitionCredit}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Selection. The record is not a GradStudent. Please try again.\n");
                            }
                            break;
                        case '7': // Faculty Advisor for Grad
                            if (stu.GetType().ToString() == GRADSTUDENT) // verifies student type
                            {
                                Console.WriteLine($"Current Faculty Advisor: {((GradStudent)(stu)).FacultyAdvisor}");
                                Console.Write("New Faculty Advisor: ");
                                ((GradStudent)(stu)).FacultyAdvisor = Console.ReadLine();
                                Console.WriteLine($"**Updated to Faculty Advisor: {((GradStudent)(stu)).FacultyAdvisor}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid Selection. The record is not a GradStudent. Please try again.\n");
                            }
                            break;
                        case '8': //exit
                            condition = false;
                            break;
                        default:
                            Console.Write("Error: Sorry that was not a choice.");
                            break;
                    }
                }
            }
            else
            {
                // record was not in the database
                Console.WriteLine($"*****Record Not Found.\n Cannot modify record for user: {email}");
            }
        }

        // delete the record by asking the user the email address
        private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            if (stu != null)
            {
                // record was found => remove the record
                students.Remove(stu);
                Console.WriteLine($"*****Record Deleted.");
            }
            else
            {
                // record was not in the database
                Console.WriteLine($"*****Record Not Found.\n Cannot delete record for user: {email}");
            }
        }

        private void CreateStudentRecord()
        {
            // check for the presence of a desired email address
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            if (stu == null) // record not found
            {
                // create a new undergrad or grad
                // gathering info
                Console.Write("Enter the first name: ");
                string first = Console.ReadLine();
                Console.Write("Enter the last name: ");
                string last = Console.ReadLine();
                Console.Write("Enter the grade pt. average: ");
                double gpa = double.Parse(Console.ReadLine());
                // email is already received above
                // enrollment date is generated by the system
                Console.Write("[U]ndergrad or [G]rad Student? ");
                string studentType = Console.ReadLine().ToUpper();

                if (studentType == "U")
                {
                    Console.WriteLine("[1]Freshman [2]Sophmore [3]Junior [4]Senior");
                    Console.Write("Enter the year/rank in school: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());

                    Console.Write("Enter the degree major: ");
                    string major = Console.ReadLine();

                    //make a new student of type undergrad and add to the list<>
                    Undergrad undergrad = new Undergrad(first, last, gpa, email, DateTime.Now, rank, major);
                    students.Add(undergrad);

                    Console.WriteLine($"Adding new UNDERGRAD to database: \n{undergrad}");

                }
                else if (studentType == "G")
                {
                    // tuition
                    Console.Write("Enter tuition reimbursement amount: ");
                    decimal tuition = decimal.Parse(Console.ReadLine());
                    // advisor
                    Console.Write("Enter the faculty advisor: ");
                    string advisor = Console.ReadLine();

                    //make a new student of type grad student and add to the list<>
                    GradStudent grad = new GradStudent(first, last, gpa, email, DateTime.Now, tuition, advisor);
                    students.Add(grad);

                    Console.WriteLine($"Adding new GRAD to database: \n{grad}");
                }
                else
                {
                    Console.WriteLine($"ERROR: no student type matches {studentType}");
                }
            }
            else
            {
                // email was FOUND - rainy day for creating a new student
                Console.WriteLine($"{email} RECORD FOUND! Cannot add a student with email {email}:");
            }
        }

        // searches the current list<> for a student record
        // inputs - email (primary key)
        // output - returns the student if FOUND and null if NOT FOUND
        private Student FindStudentRecord(out string email)
        {
            // get the search string from the user
            Console.Write("\nENTER email address (primary key) to search:");
            email = Console.ReadLine();

            // look through a student at a time to find the matching email
            foreach (var stu in students)
            {
                if (email == stu.Info.EmailAddress)
                {
                    // FOUND email in the list <>
                    Console.WriteLine($"FOUND email address: {stu.Info.EmailAddress}");
                    return stu;
                }
            }

            // NOT FOUND in the list<>
            Console.WriteLine($"{email} NOT FOUND.");
            return null;
        }

        private void SaveAllStudentDataToOutputFle()
        {
            // 1- create the file object and attach it to the actual file on disk
            StreamWriter outFile = new StreamWriter(STUDENTDB_DATAFILE_OUTPUT);

            // 2- use the file - the same way you would use any stream writer
            Console.WriteLine("\n********** Printing All Records to the OUTPUT File *************");
            foreach (var stu in students)
            {
                outFile.WriteLine(stu.ToStringForOutputFile());

                // TODO: when testing is complete, you can remove this echo to "Console"
                Console.WriteLine(stu.ToStringForOutputFile());
            }
            Console.WriteLine("******** Done Printing All Records to the OUTPUT File ***********");

            // 3- release the resource - close the file
            outFile.Close();
        }
        private void PrintAllStudentRecords()
        {
            Console.WriteLine("\n********** Printing All Records *************");
            foreach (var stu in students)
            {

                Console.WriteLine(stu);
            }
            Console.WriteLine("******** Done Printing All Records ***********");
        }

        // wanted to change the echanism of getting a char form the user so that the user
        // only has to type the actual selection and not worry about "Enter" afterwards.
        // want to change HOW this method does that - NOT WHAT the method does
        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }
        private void DisplayMainMenu()
        {
            Console.Write(@"
********************************************
*********** Student DB Main Menu ***********
********************************************
[C]reate a student record
[D]elete a student record
[F]ind a student record
[P]rint all records
[M]odify a student record
[S]ave all records without exit
[E]xit the database (without save)
Make selection by entering the first letter.
******** User Selection: ");
        }

        //public void SeedDatabaseList()
        //{
        //    // the 2 step method to add a student to the list
        //    Student stu1 = new Student("Alice", "Andersion", 3.9, "aanderson@uw.edu", DateTime.Now);
        //    students.Add(stu1);

        //    // the 1 step method, call the add method and hand in a constructor call for a student
        //    students.Add(new Student("Bob", "Bradshaw", 3.7, "bbradshaw@uw.edu", DateTime.Now));
        //    students.Add(new Student("Carlos", "Castaneda", 3.5, "ccastaneda@uw.edu", DateTime.Now));
        //    students.Add(new Student("David", "Davis", 3.0, "ddavis@uw.edu", DateTime.Now));

        //}
    }
}