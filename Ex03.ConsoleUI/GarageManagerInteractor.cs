using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Reflection;
using static Ex03.GarageLogic.VehicleCreator;

namespace Ex03.ConsoleUI
{
    public class GarageManagerInteractor
    {
        private readonly GarageManager r_Garage = new GarageManager();
        private readonly UiConsole r_UiInteractor = new UiConsole();

        public enum eSystemOptions
        {
            InsertToGarage = 1,
            DisplayLicenseNumbers,
            ChangeVehicleStatus,
            InflateTheWheels,
            FuelVehicle,
            ChargeElectricVehicle,
            DisplayVehicleData,
            Exit
        }

        public void RunGarage()
        {
            r_UiInteractor.WelcomeMessage();
            r_UiInteractor.PrintMainMenu();
            int userChoice = r_UiInteractor.GetUserChoice();

            while(true)
            {
                switch((eSystemOptions)userChoice)
                {
                    case eSystemOptions.InsertToGarage:
                        insertToGarage();
                        break;
                    case eSystemOptions.DisplayLicenseNumbers:
                        displayLicenseNumbers();
                        break;
                    case eSystemOptions.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eSystemOptions.InflateTheWheels:
                        inflateTheWheelsToMax();
                        break;
                    case eSystemOptions.FuelVehicle:
                        fuelVehicle();
                        break;
                    case eSystemOptions.ChargeElectricVehicle:
                        chargeElectricVehicle();
                        break;
                    case eSystemOptions.DisplayVehicleData:
                        displayVehicleDataByLicenseNumber();
                        break;
                    case eSystemOptions.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        r_UiInteractor.ShowMessage("Invalid input");
                        break;
                }

                r_UiInteractor.PrintMainMenu();
                userChoice = r_UiInteractor.GetUserChoice();
            }
        }

        private void chargeElectricVehicle()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();
            float timeToCharge = r_UiInteractor.GetPowerSourceAmount(true);

            try
            {
                if(r_Garage.IsVehicleInGarage(licenseNumber))
                {
                    r_Garage.ChargeVehicle(licenseNumber, timeToCharge);
                    r_UiInteractor.ShowMessage("The charging was completed successfully.");
                }
                else
                {
                    r_UiInteractor.NotInGarageNotifier();
                }
            }
            catch(ValueOutOfRangeException ex)
            {
                r_UiInteractor.PrintOutOfRangeErrorMessage(ex);
            }
            catch(ArgumentException ex)
            {
                r_UiInteractor.ShowMessage(ex.Message);
            }
        }

        private void fuelVehicle()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();
            float amountToFill = r_UiInteractor.GetPowerSourceAmount(false);

            try
            {
                if (r_Garage.IsVehicleInGarage(licenseNumber))
                {
                    r_Garage.RefuelVehicle(licenseNumber, r_UiInteractor.GetFuelType(), amountToFill);
                    r_UiInteractor.ShowMessage("The refueling was completed successfully.");
                }
                else
                {
                    r_UiInteractor.NotInGarageNotifier();
                }
            }
            catch (ValueOutOfRangeException ex)
            {
                r_UiInteractor.PrintOutOfRangeErrorMessage(ex);
            }
            catch (ArgumentException ex)
            {
                r_UiInteractor.ShowMessage(ex.Message);
            }
        }

        private void displayVehicleDataByLicenseNumber()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();

            if (r_Garage.IsVehicleInGarage(licenseNumber))
            {
                r_UiInteractor.ShowMessage(r_Garage.ShowVehicleData(licenseNumber));
            }
            else
            {
                r_UiInteractor.NotInGarageNotifier();
            }
        }

        private void inflateTheWheelsToMax()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();

            try
            {
                if (r_Garage.IsVehicleInGarage(licenseNumber))
                {
                    r_Garage.InflateWheelToMax(licenseNumber);
                    r_UiInteractor.ShowMessage("The vehicle wheels are now at the maximum air pressure");
                }
                else
                {
                    r_UiInteractor.NotInGarageNotifier();

                }
            }
            catch (ValueOutOfRangeException)
            {
                r_UiInteractor.ShowMessage("The air pressure is already at the maximum");
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();
            eVehicleStatus status = r_UiInteractor.GetSpecificVehiclesStatus();

            if (r_Garage.IsVehicleInGarage(licenseNumber))
            {
                r_Garage.ChangeVehicleStatus(licenseNumber, status);
                r_UiInteractor.ShowMessage("The vehicle status changed");
            }
            else
            {
                r_UiInteractor.NotInGarageNotifier();
            }
        }

        private void displayLicenseNumbers()
        {
            bool isFilterNeeded = r_UiInteractor.NeedToFilter();
            List<string> licensesList;

            if (!isFilterNeeded)
            {
                licensesList = r_Garage.GetVehicleListByStatus(true);
                r_UiInteractor.DisplayVehicleLicense(licensesList, false);
            }
            else
            {
                eVehicleStatus status = r_UiInteractor.GetSpecificVehiclesStatus();
                licensesList = r_Garage.GetVehicleListByStatus(false, status);
                r_UiInteractor.DisplayVehicleLicense(licensesList, isFilterNeeded, status);
            }
        }

        private void insertToGarage()
        {
            string licenseNumber = r_UiInteractor.GetLicenseNumber();

            if (r_Garage.IsVehicleInGarage(licenseNumber))
            {
                r_Garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InProgress);
                r_UiInteractor.ShowMessage("The vehicle is already in our garage (in progress)");
            }
            else
            {
                eVehicleType vehicleType = GetVehicleType();
                getClientDetails(out string clientName, out string clientPhone, out string modelName);
                Vehicle newVehicle = CreateVehicle(licenseNumber, modelName, vehicleType);
                collectBasicVehicleInformation(newVehicle);
                getUniqueDetails(newVehicle);
                r_Garage.AddVehicleToGarage(newVehicle, clientName, clientPhone);
                r_UiInteractor.ClearScreen();
                r_UiInteractor.ShowMessage("Your Vehicle successfully entered to the garage!");
            }
        }

        private void getUniqueDetails(Vehicle i_Vehicle)
        {
            Dictionary<string, string> propertyNameMessageDictionary = r_Garage.GetUniqueDetails(i_Vehicle);

            foreach (var keyValuePair in propertyNameMessageDictionary)
            {
                string propertyName = keyValuePair.Key;
                string inputMessage = keyValuePair.Value;

                r_UiInteractor.ShowMessage(inputMessage);
                Type propertyType = r_Garage.GetUniqueDetailsType(i_Vehicle, propertyName);
                while (true)
                {
                    try
                    {
                        string userInput = r_UiInteractor.GetValidStringInput();
                        object userChoice = null;
                        userChoice = propertyType.IsEnum ? Enum.Parse(propertyType, userInput) : Convert.ChangeType(userInput, propertyType);

                        if (userChoice != null)
                        {
                            r_Garage.SetUniqueDetails(i_Vehicle, propertyName, userChoice);
                            break;
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        r_UiInteractor.ShowMessage(ex.InnerException.Message);
                    }
                    catch (FormatException)
                    {
                        r_UiInteractor.ShowMessage("Invalid input, please try again.");
                    }
                }
            }
        }

        private void collectBasicVehicleInformation(Vehicle i_NewVehicle)
        {
            bool validAirPressure = false, validEnergyAmount = false;

            while (!validEnergyAmount)
            {
                try
                {
                    float floatCurrentEnergy = r_UiInteractor.GetCurrentEnergy();
                    i_NewVehicle.AddPowerSource(floatCurrentEnergy);
                    validEnergyAmount = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    r_UiInteractor.PrintOutOfRangeErrorMessage(ex);
                }
            }

            string manufacturerName = r_UiInteractor.GetManufactureWheelsName();

            while (!validAirPressure)
            {
                try
                {
                    r_UiInteractor.GetWheelsInfo(out float currentAirPressure);
                    i_NewVehicle.SetWheels(manufacturerName, currentAirPressure);
                    validAirPressure = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    r_UiInteractor.PrintOutOfRangeErrorMessage(ex);
                }
            }
        }

        private void getClientDetails(out string io_Name, out string io_PhoneNumber, out string io_ModelName)
        {
            io_Name = r_UiInteractor.GetClientName();
            io_PhoneNumber = r_UiInteractor.GetOwnerPhone();
            io_ModelName = r_UiInteractor.GetModelName();
        }

        public eVehicleType GetVehicleType()
        {
            return r_UiInteractor.GetVehicleType();
        }
    }
}