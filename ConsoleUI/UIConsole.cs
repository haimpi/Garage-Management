using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.VehicleCreator;

namespace Ex03.ConsoleUI
{
    public class UiConsole
    {
        public void WelcomeMessage()
        {
            Console.WriteLine("Garage Management System");
        }

        public void PrintMainMenu()
        {
            string mainMenu = @"
===========================================================================================
Welcome to our Garage!
1. Enter your car to our Garage.
2. Show all Driving License numbers currently at the Garage.
3. Change Vehicle status.
4. Add Vehicle wheel pressure to its maximum.
5. Fuel a gasoline Vehicle.
6. Charge an electric Vehicle.
7. Show Vehicle full details.
8. Exit Garage. 
===========================================================================================";

            Console.WriteLine(mainMenu);
        }

        public string GetLicenseNumber()
        {
            Console.Clear();
            string licenseNumberMessage = "Please enter a license number:";

            return GetValidStringInput(licenseNumberMessage);
        }

        public void PrintOutOfRangeErrorMessage(ValueOutOfRangeException i_Ex)
        {
            string message = $"{i_Ex.Message} {i_Ex.MinValue} to {i_Ex.MaxValue}";

            Console.WriteLine(message);
        }

        public void ShowMessage(string i_Message)
        {
            string msg = $@"_________________________________________
{i_Message}";

            Console.WriteLine(msg);
        }

        public void NotInGarageNotifier()
        {
            Console.WriteLine("This vehicle is not in our garage!");
        }

        public string GetOwnerPhone()
        {
            string phoneNumber = GetValidStringInput("Please enter your phone number (only digits):");

            while (!(phoneNumber.All(char.IsDigit)))
            {
                phoneNumber = GetValidStringInput("Invalid phone number, please enter again: (only digits)");
            }

            return phoneNumber;
        }

        public string GetClientName()
        {
            return GetValidStringInput("Please enter your name :");
        }

        public string GetModelName()
        {
            return GetValidStringInput("Please enter model name :");
        }

        public float GetPowerSourceAmount(bool i_IsElectric)
        {
            string msg = i_IsElectric ? "please enter the time to charge (hours):" : "Please enter amount you want to add (Liters):";

            return GetInputValue<float>(msg);
        }

        public string GetManufactureWheelsName()
        {
            return GetValidStringInput("Please enter the Manufacture wheel name:");
        }

        public float GetCurrentEnergy()
        {
            return GetInputValue<float>("Please enter the current energy amount (for fuel engine in L, for electric engine hours):");
        }

        public void GetWheelsInfo(out float io_CurrentAirPressure)
        {
            io_CurrentAirPressure = GetInputValue<float>("Please enter current air pressure in wheels");
        }

        public eVehicleType GetVehicleType()
        {
            PrintEnumMenuAndSet(
                "Please enter your vehicle", out eVehicleType vehicleType);

            return vehicleType;
        }

        public bool NeedToFilter()
        {
            Console.Clear();
            string msg = @"
===========================================================================================
Do you want to see all the licences or filter that ?
- 1 All licenses
- 2 Filter By status
===========================================================================================";
            int userChoice = GetInputValue<int>(msg);

            while (userChoice != 1 && userChoice != 2)
            {
                userChoice = GetInputValue<int>("Invalid choice , try again");
            }

            return userChoice == 2;
        }

        public void DisplayVehicleLicense(List<string> i_LicenseList, bool i_FilterNeeded, eVehicleStatus i_Status = default)
        {
            Console.Clear();
            Console.WriteLine(i_FilterNeeded ? $"Vehicles with {i_Status} status in the garage:" : "Vehicles in the garage:");

            foreach (string license in i_LicenseList)
            {
                Console.WriteLine(license);
            }
        }

        public eVehicleStatus GetSpecificVehiclesStatus()
        {
            PrintEnumMenuAndSet("Choose status:", out eVehicleStatus status);

            return status;
        }

        public int GetUserChoice()
        {
            return GetInputValue<int>("Enter your choice:");
        }

        public eFuelType GetFuelType()
        {
            PrintEnumMenuAndSet("Select fuel type to add", out eFuelType fuelType);

            return fuelType;
        }

        public string GetValidStringInput(string i_Message = "")
        {
            if (i_Message != "")
            {
                string msg = $@"_________________________________________
{i_Message}";
                Console.WriteLine(msg);
            }

            string input = Console.ReadLine();
            while (input == string.Empty)
            {
                Console.WriteLine("Invalid input, you must fill value");
                input = Console.ReadLine();
            }

            return input;
        }

        public T GetInputValue<T>(string i_PromptMessage) where T : struct  //get valid with generic
        {
            T inputValue;
            string inputStr = GetValidStringInput(i_PromptMessage);

            while (!tryParse(inputStr, out inputValue))
            {
                Console.WriteLine($"Invalid input. Please enter a valid {typeof(T).Name}:");
                inputStr = Console.ReadLine();
            }

            return inputValue;
        }

        private static bool tryParse<T>(string i_Value, out T io_Result) where T : struct
        {
            bool parsed = false;
            io_Result = default(T);

            if (typeof(T) == typeof(int))
            {
                parsed = int.TryParse(i_Value, out int intValue);
                io_Result = (T)(object)intValue;
            }
            else if (typeof(T) == typeof(float))
            {
                parsed = float.TryParse(i_Value, out float floatValue);
                io_Result = (T)(object)floatValue;
            }

            return parsed;
        }

        public bool PrintEnumMenuAndSet<TEnum>(string i_Message, out TEnum io_SelectedValue)
        {
            io_SelectedValue = default; // Set default value
            string msg = $@"
_________________________________________
{i_Message}";

            Console.WriteLine(i_Message);
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                Console.WriteLine($"- {Convert.ToInt32(value)}: {value}");
            }

            string userInput = Console.ReadLine();

            while (!IsValidEnumChoice<TEnum>(userInput))
            {
                Console.WriteLine("Invalid choice. Please try again.");
                userInput = Console.ReadLine();
            }

            io_SelectedValue = (TEnum)Enum.Parse(typeof(TEnum), userInput); // Parse enum value after validation

            return true;
        }

        public bool IsValidEnumChoice<TEnum>(string i_UserInput)
        {
            return int.TryParse(i_UserInput, out int choice) &&
                   Enum.GetValues(typeof(TEnum)).Cast<int>().Contains(choice);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}