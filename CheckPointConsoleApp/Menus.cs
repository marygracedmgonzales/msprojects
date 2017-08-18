using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckPointConsoleApp
{
    public class Menus
    {
        TextFiles textfile = new TextFiles();
        PhysicianRecords physicianRecords = new PhysicianRecords();
        UserInput userInput = new UserInput();

        public string[] MenuList = {"Add Physician records", "Delete Physician records", "Update Physician records",
                             "View all Physician records", "Find a Physician ID", "Clear Screen", "Exit"};
        public void Menu()
        {
            for (int i = 0; i < MenuList.Length; i++)
            {
                Console.WriteLine((i + 1) + " " + MenuList[i]);
            }
        }

        public void MenuGoBackHome()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            textfile.LabelInput("Press any key to go back to menu...");
            if (Console.ReadKey(true) != null)
            {
                this.MenuClearScreen();
            }
        }
        
        private void MenuAddPhysician(string selected)
        {
            Console.Clear();
            Console.WriteLine("**********" + selected + "**********");
            Console.WriteLine("");

            //Physician Basic Info
            Physician newRecord = new Physician();
            textfile.LabelInput("Enter first name : ");
            newRecord.FirstName = userInput.InputStringValidation();            
            textfile.LabelInput("Enter middle name : ");
            newRecord.MiddleName = userInput.InputStringValidation();
            textfile.LabelInput("Enter last name : ");
            newRecord.LastName = userInput.InputStringValidation();
            textfile.LabelInput("Enter birthdate  (MM/DD/YYYY) : ");
            newRecord.BirthDate = userInput.InputDateTimeValidation();
            newRecord.Gender = userInput.InputGenderValidation();
            textfile.LabelInput("Enter weight : ");
            newRecord.Weight = userInput.InputDecimalValidation();
            textfile.LabelInput("Enter height : ");
            newRecord.Height = userInput.InputDecimalValidation();

            //Contact Info
            Console.WriteLine("");
            Console.WriteLine("**********Contact Information**********");
            Console.WriteLine("");
            try
            {
                ContactInfo contactInfo = new ContactInfo();
                textfile.LabelInput("Enter home address : ");
                var homeAddress = userInput.InputStringAddressValidation();
                contactInfo.HomeAddress = (homeAddress != null) ? homeAddress : "NA";
                textfile.LabelInput("Enter home phone number : ");
                var phoneNumber = userInput.InputPhoneNumberValidation();
                contactInfo.HomePhone = (phoneNumber != null) ? phoneNumber : "NA";
                textfile.LabelInput("Enter office address : ");
                var officeAddress = userInput.InputStringAddressValidation();
                contactInfo.OfficeAddress = (officeAddress != null) ? officeAddress : "NA";
                textfile.LabelInput("Enter office phone number : ");
                var officePhoneNumber = userInput.InputPhoneNumberValidation();
                contactInfo.OfficePhone = (officePhoneNumber != null) ? officePhoneNumber : "NA";
                textfile.LabelInput("Enter email address : ");
                var emailAddress = userInput.InputEmailValidation();
                contactInfo.EmailAddress = (emailAddress != null) ? emailAddress : "NA";
                textfile.LabelInput("Enter cellphone number : ");
                var cellphoneNumber = userInput.InputPhoneNumberValidation();
                contactInfo.CellPhoneNumber = (cellphoneNumber != null) ? cellphoneNumber : "NA";

                newRecord.PhysicianContactInfo = contactInfo;
            }
            catch (NullReferenceException ex)
            {
                ArgumentException argEx = new ArgumentException("Index is NullReferenceException", "index", ex); throw argEx;
            }

            //Specialization
            Console.WriteLine("");
            Console.WriteLine("**********Specialization**********");
            Console.WriteLine("");
            bool addSpecialization = true;
            List<Specialization> specializationList = new List<Specialization>();

            try
            {
                while (addSpecialization)
                {
                    Specialization specialization = new Specialization();
                    textfile.LabelInput("Enter specialization : ");
                    specialization.Name = userInput.InputStringValidation();
                    textfile.LabelInput("Enter description : ");
                    specialization.Description = userInput.InputStringValidation();
                    specializationList.Add(specialization);
                    bool isAddSpec = true;
                    textfile.LabelInput("Do you want to add new specialization record?(Y/N) ");
                    var ansSp = Console.ReadLine();
                    do
                    {
                        if (ansSp.Equals("Y", StringComparison.InvariantCultureIgnoreCase) || ansSp.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            isAddSpec = false;
                        }
                        if (isAddSpec)
                        {
                            textfile.LabelInput("Please enter Y/N only... ");
                            ansSp = Console.ReadLine();
                        }
                    } while (isAddSpec);
                    {
                        
                    }
                    if ("N".Equals(ansSp.ToUpper()))
                    {
                        addSpecialization = false;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                ArgumentException argEx = new
                ArgumentException("Index is NullReferenceException", "index", ex); throw argEx;
            }

            newRecord.Specialization = specializationList;

            Console.WriteLine("");
            textfile.LabelInput("Do you want to save new record?(Y/N) ");
            var ans = Console.ReadLine();
            if ("Y".Equals(ans.ToUpper()))
            {
                int result = physicianRecords.AddRecord(newRecord);
                if (result == 1)
                {
                    Console.WriteLine("");
                    physicianRecords.ViewAllRecords();
                }
            }
            else
            {
                Console.Clear();
            }
        }

        private void MenuDeletePhysician(string selected)
        {
            Console.Clear();
            Console.WriteLine("**********" + selected + "**********");
            Console.WriteLine("");

            if (physicianRecords.CurrentRecordList().Count > 0)
            {
                textfile.LabelInput("Enter valid physician ID : ");
                int input = userInput.InputIntegerValidation(); //get user input integer                                               
                int i = 0;
                bool resFound = false;
                foreach (Physician record in physicianRecords.CurrentRecordList())
                {
                    if (record.Id == input)
                    {
                        resFound = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (resFound)
                {
                    physicianRecords.FindPhysicianByID(input);
                    Console.WriteLine("");
                    Console.WriteLine("Press 1 to delete contact info only.");
                    Console.WriteLine("Press 2 to delete specializations only.");
                    Console.WriteLine("Press 3 to delete All physician information.");
                    Console.WriteLine("Press 4 to cancel and go back to main menu.");
                    bool delOpt = true;
                    Console.WriteLine("");
                    while (delOpt)
                    {
                        int deleteSelection = userInput.InputIntegerValidation(); //get user input integer
                        if (deleteSelection == 1 || deleteSelection == 2 || deleteSelection == 3)
                        {
                            delOpt = false;
                            physicianRecords.DeleteRecord(i, deleteSelection);
                        }
                        else if (deleteSelection == 4)
                        {
                            break;
                        }
                        else
                        {
                            delOpt = true;
                            textfile.LabelInput("Invalid delete option!");
                        }
                    }
                }
                else
                {
                    textfile.LabelInput("Invalid physician ID!");
                }
            }
            else //record list == 0
            {
                textfile.LabelInput("No Physicians record yet!");
            }
        }

        private void MenuUpdatePhysician(string selected)
        {
            Console.Clear();
            Console.WriteLine("**********" + selected + "**********");
            Console.WriteLine("");

            if (physicianRecords.CurrentRecordList().Count > 0)
            {
                textfile.LabelInput("Enter ID number : ");
                int physicianID = userInput.InputIntegerValidation(); //get user input integer  
                int i = 0;
                bool resFound = false;
                int index = 0;

                foreach (Physician record in physicianRecords.CurrentRecordList())
                {
                    if (record.Id == physicianID)
                    {
                        resFound = true;
                        index = i;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (resFound)
                {
                    physicianRecords.FindPhysicianByID(physicianID);
                    Console.WriteLine("");
                    Console.WriteLine("Press 1 to update basic information");
                    Console.WriteLine("Press 2 to update contact information");
                    Console.WriteLine("Press 3 to update specialization information");
                    Console.WriteLine("Press 4 to cancel and go back to main menu.");
                    bool updateOpt = true;
                    Console.WriteLine("");
                    Physician physician = physicianRecords.CurrentRecordList()[index];
                    while (updateOpt)
                    {                   
                        int updateSelection = userInput.InputIntegerValidation(); //get user input integer  
                        if (updateSelection == 1)
                        {
                            updateOpt = false;
                            Physician updatedRecord = new Physician();
                            updatedRecord.Height = Convert.ToDecimal(0);
                            updatedRecord.Weight = Convert.ToDecimal(0);

                            Console.Clear();
                            Console.WriteLine("***Update Basic Information***");
                            Console.WriteLine("------------------------------------------------");
                            Console.WriteLine("1) First name: " + physician.FirstName);
                            Console.WriteLine("2) Middle name: " + physician.MiddleName);
                            Console.WriteLine("3) Last name: " + physician.LastName);
                            Console.WriteLine("4) Birthdate: " + physician.BirthDate.ToShortDateString());
                            Console.WriteLine("5) Gender: " + physician.Gender);
                            Console.WriteLine("6) Height: " + physician.Height);
                            Console.WriteLine("7) Weight: " + physician.Weight);
                            Console.WriteLine("------------------------------------------------");

                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            Console.WriteLine("Please enter the number of field to update. Press other number to cancel : ");
                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            int fieldNum = userInput.InputIntegerValidation(); //get user input integer
                            bool updateOpen = (fieldNum == 1 || fieldNum == 2 || fieldNum == 3 || fieldNum == 4 || fieldNum == 5 || fieldNum == 6 || fieldNum == 7) ? true : false;

                            while (updateOpen)
                            {
                                switch (fieldNum)
                                {
                                    case 1: //FirstName
                                        textfile.LabelInput("Enter new first name : ");
                                        updatedRecord.FirstName = userInput.InputStringValidation();
                                        break;
                                    case 2: //MiddleName
                                        textfile.LabelInput("Enter new middle name : ");
                                        updatedRecord.MiddleName = userInput.InputStringValidation();
                                        break;
                                    case 3: //LastName
                                        textfile.LabelInput("Enter new last name : ");
                                        updatedRecord.LastName = userInput.InputStringValidation();
                                        break;
                                    case 4: //Birthdate
                                        textfile.LabelInput("Enter new birthdate : ");
                                        updatedRecord.BirthDate = userInput.InputDateTimeValidation();
                                        break;
                                    case 5: //Gender
                                        updatedRecord.Gender = userInput.InputGenderValidation();
                                        break;
                                    case 6: //Height
                                        textfile.LabelInput("Enter new height record : ");
                                        updatedRecord.Height = userInput.InputDecimalValidation();
                                        break;
                                    case 7: //Weight
                                        textfile.LabelInput("Enter new weight record : ");
                                        updatedRecord.Weight = userInput.InputDecimalValidation();
                                        break;
                                    default:
                                        userInput.Alertmsg("Invalid input!", "error");
                                        break;
                                }

                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                Console.WriteLine("Do you want to update another field?");
                                Console.WriteLine("Please enter the number of field to update. Press other number to save changes : ");
                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                fieldNum = userInput.InputIntegerValidation(); //get user input integer

                                updateOpen = (fieldNum == 1 || fieldNum == 2 || fieldNum == 3 || fieldNum == 4 || fieldNum == 5 || fieldNum == 6 || fieldNum == 7) ? true : false;
                                if (!updateOpen)
                                {
                                    int result = physicianRecords.UpdateRecord(updatedRecord, index);
                                    Console.WriteLine("");
                                    physicianRecords.FindPhysicianByID(physicianID);
                                    break;
                                }
                            }

                        }
                        else if (updateSelection == 2)
                        {
                            updateOpt = false;
                            ContactInfo updatedRecord = new ContactInfo();

                            Console.Clear();
                            Console.WriteLine("***Update Contact Information***");
                            Console.WriteLine("------------------------------------------------");
                            Console.WriteLine("1) Home address: " + physician.PhysicianContactInfo.HomeAddress);
                            Console.WriteLine("2) Home phone: " + physician.PhysicianContactInfo);
                            Console.WriteLine("3) Office address: " + physician.PhysicianContactInfo);
                            Console.WriteLine("4) Office phone: " + physician.BirthDate.ToShortDateString());
                            Console.WriteLine("5) Email address: " + physician.Gender);
                            Console.WriteLine("6) Cellphone number: " + physician.Height);
                            Console.WriteLine("------------------------------------------------");

                            Console.WriteLine("Please enter the number of field to update. Press other number to cancel : ");
                            int fieldNum = userInput.InputIntegerValidation(); //get user input integer

                            bool updateOpen = (fieldNum == 1 || fieldNum == 2 || fieldNum == 3 || fieldNum == 4 || fieldNum == 5 || fieldNum == 6) ? true : false;
                            while (updateOpen)
                            {
                                switch (fieldNum)
                                {
                                    case 1: //HomeAddress
                                        textfile.LabelInput("Enter new home address : ");
                                        updatedRecord.HomeAddress = userInput.InputStringAddressValidation();
                                        break;
                                    case 2: //HomePhone
                                        textfile.LabelInput("Enter new home phone number : ");
                                        updatedRecord.HomePhone = userInput.InputPhoneNumberValidation();
                                        break;
                                    case 3: //OfficeAddress
                                        textfile.LabelInput("Enter new office address : ");
                                        updatedRecord.OfficeAddress = userInput.InputStringAddressValidation();
                                        break;
                                    case 4: //OfficePhone
                                        textfile.LabelInput("Enter new office phone number : ");
                                        updatedRecord.OfficePhone = userInput.InputPhoneNumberValidation();
                                        break;
                                    case 5: //EmailAddress
                                        textfile.LabelInput("Enter new email address : ");
                                        updatedRecord.EmailAddress = userInput.InputEmailValidation();
                                        break;
                                    case 6: //CellPhoneNumber
                                        textfile.LabelInput("Enter new cellphone number : ");
                                        updatedRecord.CellPhoneNumber = userInput.InputPhoneNumberValidation();
                                        break;
                                    default:
                                        userInput.Alertmsg("Invalid input!", "error");
                                        break;
                                }
                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                Console.WriteLine("Do you want to update another field?");
                                Console.WriteLine("Please enter the number of field to update. Press other number to save changes : ");
                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                fieldNum = userInput.InputIntegerValidation(); //get user input integer
                                updateOpen = (fieldNum == 1 || fieldNum == 2 || fieldNum == 3 || fieldNum == 4 || fieldNum == 5 || fieldNum == 6) ? true : false;

                                if (!updateOpen)
                                {
                                    int result = physicianRecords.UpdateContactInfo(updatedRecord, index);
                                    Console.WriteLine("");
                                    physicianRecords.FindPhysicianByID(physicianID);
                                    break;
                                }
                            }

                        }
                        else if (updateSelection == 3)
                        {
                            updateOpt = false;
                            List<Specialization> specializationUpdateList = new List<Specialization>();
                            Specialization specializationUpdate = new Specialization();

                            Console.Clear();
                            List<int> specIds = new List<int>();
                            if (physician.Specialization.Count != 0)
                            {
                                Console.WriteLine("***Specialization(s)***");
                                foreach (Specialization specialization in physician.Specialization)
                                {
                                    Console.WriteLine("ID : " + specialization.Id);
                                    Console.WriteLine("Name : " + specialization.Name);
                                    Console.WriteLine("Description : " + specialization.Description);
                                    specIds.Add(specialization.Id);
                                }
                            }
                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            Console.WriteLine("Please enter the ID number of specialization. Press other number to cancel : ");
                            Console.WriteLine("-------------------------------------------------------------------------------------");
                            int idNum = userInput.InputIntegerValidation(); //get user input integer
                            bool updateOpen = (specIds.Contains(idNum)) ? true : false;

                            while (updateOpen)
                            {
                                //For Specialization name
                                textfile.LabelInput("Do you want to update the specialization name record?(Y/N)");
                                string nameUpdate = Console.ReadLine();
                                bool isNameUpdate = true;
                                while (isNameUpdate)
                                {
                                    if (nameUpdate.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        textfile.LabelInput("Enter updated record name : ");
                                        specializationUpdate.Name = userInput.InputStringValidation();
                                        isNameUpdate = false;
                                    }
                                    else if (nameUpdate.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        isNameUpdate = false;
                                    }
                                    else
                                    {
                                        textfile.LabelInput("Invalid input, please enter Y/N...");
                                    }
                                }

                                //For Description
                                textfile.LabelInput("Do you want to update the description record?(Y/N)");
                                string descUpdate = Console.ReadLine();
                                bool isDescUpdate = true;

                                while (isDescUpdate)
                                {
                                    if (descUpdate.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        textfile.LabelInput("Enter updated description record : ");
                                        specializationUpdate.Description = userInput.InputStringValidation();
                                        isDescUpdate = false;
                                    }
                                    else if (descUpdate.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        isDescUpdate = false;
                                    }
                                    else
                                    {
                                        textfile.LabelInput("Invalid input, please enter Y/N...");
                                    }
                                }

                                int specIndex = specIds.IndexOf(idNum);
                                int result = physicianRecords.UpdateSpecializationInfo(specializationUpdate, specIndex);
                                Console.WriteLine("");
                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                Console.WriteLine("Do you want to update another specialization?");
                                Console.WriteLine("Please enter the ID number of specialization. Press other number to cancel : ");
                                Console.WriteLine("-------------------------------------------------------------------------------------");
                                idNum = userInput.InputIntegerValidation(); //get user input integer
                                updateOpen = (specIds.Contains(idNum)) ? true : false;
                            }
                        }
                        else if (updateSelection == 4)
                        {
                            break;
                        }
                        else
                        {
                            updateOpt = true;
                            textfile.LabelInput("Invalid update option!");
                        }
                    }
                }
                else
                {
                    textfile.LabelInput("ID doesn't exists!");
                }
            }
            else //record list == 0
            {
                textfile.LabelInput("No Physicians record yet!");
            }
        }

        private void MenuViewAllPhysicians(string selected)
        {
            Console.Clear();
            Console.WriteLine("**********" + selected + "**********");
            Console.WriteLine("");

            physicianRecords.ViewAllRecords();
        }

        private void MenuFindPhysicianById(string selected)
        {
            Console.Clear();
            Console.WriteLine("**********" + selected + "**********");
            Console.WriteLine("");

            textfile.LabelInput("Enter ID number : ");
            int findPhysicianID = userInput.InputIntegerValidation(); //get user input integer
            bool isExist = false;
            if(physicianRecords.TotalRecords() != 0)
            {
                foreach (Physician record in physicianRecords.CurrentRecordList())
                {
                    if (record.Id == findPhysicianID)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    physicianRecords.FindPhysicianByID(findPhysicianID);
                }
                else
                {
                    userInput.Alertmsg("Record not found!", "error");
                }
            }
            else
            {
                userInput.Alertmsg("Record not found!", "error");
            }
            
        }

        public void MenuClearScreen()
        {
            Console.WriteLine("");

            //Display the Menu label
            textfile.CenterLine();
            Console.WriteLine(textfile.Menu);
            textfile.CenterLine();

            //Display the menus
            this.Menu();
        }

        public void MenuOption(int selectedOpt)
        {
            UserInput userInput = new UserInput();
            try
            {
                string selected = MenuList[selectedOpt];
                switch (selected)
                {
                    case "Add Physician records":
                        this.MenuAddPhysician(selected);
                        this.MenuGoBackHome();
                        userInput.GetUserInput();
                        break;
                    case "Delete Physician records":
                        this.MenuDeletePhysician(selected);
                        this.MenuGoBackHome();
                        userInput.GetUserInput();
                        break;
                    case "Update Physician records":
                        this.MenuUpdatePhysician(selected);
                        this.MenuGoBackHome();
                        userInput.GetUserInput();
                        break;
                    case "View all Physician records":
                        this.MenuViewAllPhysicians(selected);
                        this.MenuGoBackHome();
                        userInput.GetUserInput();
                        break;
                    case "Find a Physician ID":
                        this.MenuFindPhysicianById(selected);
                        this.MenuGoBackHome();
                        userInput.GetUserInput();
                        break;
                    case "Clear Screen":
                        Console.Clear();
                        this.MenuClearScreen();
                        userInput.GetUserInput();
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                string InvalidOpt = "Invalid Option!";
                userInput.Alertmsg(InvalidOpt, "error");
                userInput.GetUserInput();
            }
        }
    }
}
