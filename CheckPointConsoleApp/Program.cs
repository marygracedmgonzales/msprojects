using CheckPointConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        TextFiles textfile = new TextFiles();
        Menus menus = new Menus();
        UserInput userInput = new UserInput();
        PhysicianRecords physicianRecords = new PhysicianRecords();

        //Display the banner text
        textfile.CenterLine();
        Console.WriteLine(textfile.BannerText);
        textfile.CenterLine();

        //Display the Menu label
        textfile.CenterLine();
        Console.WriteLine(textfile.Menu);
        textfile.CenterLine();

        //Display the menus
        menus.Menu();

        // get user input.
        userInput.GetUserInput();
    }
}

public class UserInput
{
    TextFiles textfile = new TextFiles();

    public void GetUserInput()
    {
        Menus menus = new Menus();
        // get user input
        Console.WriteLine("");
        textfile.LabelInput("Type a number and hit <enter> : ");
        
        try
        {
            int input = Convert.ToInt32(Console.ReadLine());
            menus.MenuOption(input - 1);
        }
        catch (IndexOutOfRangeException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Index is out of range", "index", ex); throw argEx;
        }
        catch (FormatException ex)
        {
            string InvalidOpt = "Please enter number only!";
            this.Alertmsg(InvalidOpt, "error");
            this.GetUserInput();
        }
    }
    
    public Decimal InputDecimalValidation()
    {
        bool isValidOpt = false;
        string input = null;
        do
        {
            input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[1-9]\d*(\.\d+)?$"))
            {
                isValidOpt = true;
            }
            else
            {
                this.Alertmsg("Please enter numbers only", "error");
                isValidOpt = false;
            }
        } while (!isValidOpt);

        return Convert.ToDecimal(input);
    }

    public string InputGenderValidation()
    {
        string input = null;
        bool isGender = true;
        while (isGender)
        {
            textfile.LabelInput("Enter gender (M/F) : ");
            string gender = Console.ReadLine();

            if (gender.Equals("M", StringComparison.InvariantCultureIgnoreCase))
            {
                input = gender;
                isGender = false;
            }
            else if (gender.Equals("F", StringComparison.InvariantCultureIgnoreCase))
            {
                input = gender;
                isGender = false;
            }
            else
            {
                textfile.LabelInput("Invalid input, please enter M for Male or F for Female...");
                Console.WriteLine("");
            }
        }
        return input;
    }

    public DateTime InputDateTimeValidation()
    {
        DateTime dDate;
        bool isValid = false;
        do
        {
            string now = Console.ReadLine();
            if (DateTime.TryParse(now, out dDate))
            {
                String.Format("{0:d/MM/yyyy}", dDate);
                isValid = true;
            }
            else
            {
                textfile.LabelInput("Invalid date format");
                Console.WriteLine("");
            }
            
        } while (!isValid);
        return dDate;
    }

    public string InputStringAddressValidation()
    {
        String input = "";
        bool isValidOpt = false;
        do
        {
            input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$"))
            {
                isValidOpt = true;
            }
            else
            {
                this.Alertmsg("Please enter text only", "error");
                Console.WriteLine("");
                isValidOpt = false;
            }
        } while (!isValidOpt);
        return input;
    }

    public string InputEmailValidation()
    {
        String input = "";
        bool isValidOpt = false;
        do
        {
            input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z0-9@.]+$"))
            {
                isValidOpt = true;
            }
            else
            {
                this.Alertmsg("Please enter text only", "error");
                Console.WriteLine("");
                isValidOpt = false;
            }
        } while (!isValidOpt);
        return input;
    }

    public string InputStringValidation()
    {
        String input = "";
        bool isValidOpt = false;
        do
        {
            input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^[a-zA-Z ]+$"))
            {
                isValidOpt = true;
            }
            else
            {
                this.Alertmsg("Please enter text only", "error");
                Console.WriteLine("");
                isValidOpt = false;
            }
        } while (!isValidOpt);
        return input;
    }
    
    public String InputPhoneNumberValidation()
    {
        string input = null;
        try
        {
            bool isValidOpt = false;
            do
            {
                input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^[1-9]+$"))
                {
                    isValidOpt = true;
                }
                else
                {
                    this.Alertmsg("Please enter valid number", "error");
                }
            } while (!isValidOpt);
        }
        catch (OverflowException ex)
        {
            this.Alertmsg("Please enter valid telephone number", "error");
            ArgumentException argEx = new
            ArgumentException("OverflowException", "index", ex); throw argEx;
        }

        return input;
    }

    public int InputIntegerValidation()
    {
        string input = null;
        try
        {
            bool isValidOpt = false;
            do
            {
                input = Console.ReadLine();

                if (Regex.IsMatch(input, @"^[1-9]+$"))
                {
                    isValidOpt = true;
                }
                else
                {
                    this.Alertmsg("Please enter valid number", "error");
                }
            } while (!isValidOpt);
        }
        catch(OverflowException ex)
        {
            this.Alertmsg("Please enter valid number", "error");
            ArgumentException argEx = new
            ArgumentException("OverflowException", "index", ex); throw argEx;
        }
        
        return Convert.ToInt32(input);
    }

    public void Alertmsg(String msg, String msgType)
    {
        if (msgType == "success"){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        if (msgType == "error"){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}

public class TextFiles
{
    public string Lines = "========================================================";
    public string LinesLabel = "________________________________________________________";
    public string Menu = "                           MENU";
    public string BannerText = " Welcome to PW Hospital Information Management System!";
    public void CenterLine()
    {
        Console.WriteLine(Lines);
    }
    public void CenterLineLabel()
    {
        Console.WriteLine(LinesLabel);
    }
    public void LabelInput(string text)
    {
        Console.Write(text);
    }
}

public class PhysicianRecords
{
    public static List<Physician> RecordList = new List<Physician>();
    
    public List<Physician> CurrentRecordList()
    {
        return RecordList;
    }

    public int TotalRecords()
    {
        return RecordList.Count();
    }

    TextFiles textfile = new TextFiles();
    UserInput userInput = new UserInput();

    public int AddRecord(Physician newRecord)
    {
        int prevCount = TotalRecords();
        int recordId = prevCount + 1;
        int specializationId = 1;
        newRecord.Id = recordId;
        newRecord.PhysicianContactInfo.PhysicianId = recordId;
        if(newRecord.Specialization.Count != 0){
            foreach (Specialization specialization in newRecord.Specialization)
            {
                specialization.PhysicianId = recordId;
                specialization.Id = specializationId;
                specializationId++;
            }
        }
        RecordList.Add(newRecord);
        int newCount = TotalRecords();

        return (newCount > prevCount) ? 1 : 0;
    }

    public int UpdateRecord(Physician updatedRecord, int i)
    {        
        int result = 0;
        try
        {
            if(!string.IsNullOrWhiteSpace(updatedRecord.FirstName))
            {
                RecordList[i].FirstName = updatedRecord.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.MiddleName))
            {
                RecordList[i].MiddleName = updatedRecord.MiddleName;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.LastName))
            {
                RecordList[i].LastName = updatedRecord.LastName;
            }
            if (!updatedRecord.BirthDate.Equals(DateTime.MinValue))
            {
                RecordList[i].BirthDate = updatedRecord.BirthDate;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.Gender))
            {
                RecordList[i].Gender = updatedRecord.Gender;
            }
            if (!updatedRecord.Height.Equals(Decimal.MinValue))
            {
                RecordList[i].Height = updatedRecord.Height;
            }
            if (!updatedRecord.Weight.Equals(Decimal.MinValue))
            {
                RecordList[i].Weight = updatedRecord.Weight;
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Index is out of range", "index", ex); throw argEx;
        }
        userInput.Alertmsg("Successfully updated!", "success");
        return result;
    }

    public int UpdateContactInfo(ContactInfo updatedRecord, int i)
    {
        int result = 0;
        try
        {
            if (!string.IsNullOrWhiteSpace(updatedRecord.HomeAddress))
            {
                RecordList[i].PhysicianContactInfo.HomeAddress = updatedRecord.HomeAddress;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.HomePhone))
            {
                RecordList[i].PhysicianContactInfo.HomePhone = updatedRecord.HomePhone;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.OfficeAddress))
            {
                RecordList[i].PhysicianContactInfo.OfficeAddress = updatedRecord.OfficeAddress;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.OfficePhone))
            {
                RecordList[i].PhysicianContactInfo.OfficePhone = updatedRecord.OfficePhone;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.EmailAddress))
            {
                RecordList[i].PhysicianContactInfo.EmailAddress = updatedRecord.EmailAddress;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.CellPhoneNumber))
            {
                RecordList[i].PhysicianContactInfo.CellPhoneNumber = updatedRecord.CellPhoneNumber;
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Index is out of range", "index", ex); throw argEx;
        }
        userInput.Alertmsg("Successfully updated!", "success");
        return result;
    }

    public int UpdateSpecializationInfo(Specialization updatedRecord, int i)
    {
        int result = 0;
        try
        {
            if (!string.IsNullOrWhiteSpace(updatedRecord.Name))
            {
                RecordList[i].Specialization[i].Name = updatedRecord.Name;
            }
            if (!string.IsNullOrWhiteSpace(updatedRecord.Description))
            {
                RecordList[i].Specialization[i].Description = updatedRecord.Description;
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Index is out of range", "index", ex); throw argEx;
        }
        userInput.Alertmsg("Successfully updated!", "success");
        return result;
    }

    public void ViewAllRecords()
    {
        if (RecordList.Count != 0)
        {  
           foreach (Physician record in RecordList)
            {
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("Physician ID : " + record.Id);
                Console.WriteLine("Firstname : " + record.FirstName);
                Console.WriteLine("Middlename : " + record.MiddleName);
                Console.WriteLine("Lastname : " + record.LastName);
                Console.WriteLine("Gender : " + record.Gender);
                Console.WriteLine("Birthdate : " + record.BirthDate.ToShortDateString());
                Console.WriteLine("Height : " + record.Height);
                Console.WriteLine("Weight : " + record.Weight);
                Console.WriteLine("Home Phone : " + record.PhysicianContactInfo.HomePhone);
                Console.WriteLine("Home Address : " + record.PhysicianContactInfo.HomeAddress);
                Console.WriteLine("Office Phone : " + record.PhysicianContactInfo.OfficePhone);
                Console.WriteLine("Office Address : " + record.PhysicianContactInfo.OfficeAddress);
                Console.WriteLine("Email Address : " + record.PhysicianContactInfo.EmailAddress);
                Console.WriteLine("Cellphone Number : " + record.PhysicianContactInfo.CellPhoneNumber);
                Console.WriteLine("--------------------------------------------------------------------");
            }
        }
        else
        {
            textfile.LabelInput("No Physicians record yet!");
        }
    }

    public void FindPhysicianByID(int physicianID)
    {
        int recordFound = 0;
        try
        {
            foreach (Physician record in RecordList)
            {
                if (record.Id == physicianID)
                {
                    Console.WriteLine("--------------------------------------------------------------------");
                    Console.WriteLine("Physician ID : " + record.Id);
                    Console.WriteLine("Firstname : " + record.FirstName);
                    Console.WriteLine("Middlename : " + record.MiddleName);
                    Console.WriteLine("Lastname : " + record.LastName);
                    Console.WriteLine("Gender : " + record.Gender);
                    Console.WriteLine("Birthdate : " + record.BirthDate.ToShortDateString());
                    Console.WriteLine("Height : " + record.Height);
                    Console.WriteLine("Weight : " + record.Weight);
                    Console.WriteLine("Home Phone : " + record.PhysicianContactInfo.HomePhone);
                    Console.WriteLine("Home Address : " + record.PhysicianContactInfo.HomeAddress);
                    Console.WriteLine("Office Phone : " + record.PhysicianContactInfo.OfficePhone);
                    Console.WriteLine("Office Address : " + record.PhysicianContactInfo.OfficeAddress);
                    Console.WriteLine("Email Address : " + record.PhysicianContactInfo.EmailAddress);
                    Console.WriteLine("Cellphone Number : " + record.PhysicianContactInfo.CellPhoneNumber);
                    if(record.Specialization.Count != 0)
                    {
                        Console.WriteLine("***Specialization(s)***");
                        foreach (Specialization specialization in record.Specialization)
                        {
                            Console.WriteLine("ID : " + specialization.Id);
                            Console.WriteLine("Name : " + specialization.Name);
                            Console.WriteLine("Description : " + specialization.Description);
                        }
                    }
                    Console.WriteLine("--------------------------------------------------------------------");
                    recordFound = 1;
                }
            }
        }
        catch (FormatException ex) {
            ArgumentException argEx = new
            ArgumentException("RecordList is null.", "index", ex); throw argEx;
        }
        if(recordFound == 0)
        {
            textfile.LabelInput("No record found!");
        }        
    }

    public int DeleteRecord(int i, int deleteOption)
    {
        int result = 0;
        try
        {
            int previousCount = RecordList.Count();
            if(deleteOption == 1)
            {
                RecordList[i].PhysicianContactInfo = new ContactInfo();
                userInput.Alertmsg("Successfully deleted!", "success");
            }
            else if(deleteOption == 2)
            {
                RecordList[i].Specialization = new List<Specialization>();
                userInput.Alertmsg("Successfully deleted!", "success");
            }
            else
            {
                RecordList.RemoveAt(0);
            }
            
            int newCount = RecordList.Count();

            result = previousCount.CompareTo(newCount);
        }
        catch (IndexOutOfRangeException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Index is out of range", "index", ex); throw argEx;
        }
        catch (ArgumentNullException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Record list is null", "index", ex); throw argEx;
        }
        catch(InvalidOperationException ex)
        {
            ArgumentException argEx = new
            ArgumentException("Invalid Operation", "index", ex); throw argEx;
        }
        return result;
    }
}



