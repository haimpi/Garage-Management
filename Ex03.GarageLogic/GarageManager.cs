using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, GarageTicket> r_GarageClientDetails = null;//string = LicenseNumber
        private readonly UniquePropertyDetailsBuilder r_TypeBuilder;

        public GarageManager()
        {
            r_GarageClientDetails = new Dictionary<string, GarageTicket>();
            r_TypeBuilder = new UniquePropertyDetailsBuilder();
        }

        public string ShowVehicleData(string i_LicenseNumber)
        {
            string msg = string.Format($@"
____________________________________________
{r_GarageClientDetails[i_LicenseNumber]}
____________________________________________");

            return msg;
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return r_GarageClientDetails.ContainsKey(i_LicenseNumber);
        }

        public void AddVehicleToGarage(Vehicle i_Vehicle, string i_ClientName, string i_ClientPhoneNumber)
        {
            GarageTicket garageTicket = new GarageTicket(i_Vehicle, i_ClientName, i_ClientPhoneNumber);

            r_GarageClientDetails.Add(i_Vehicle.LicenseNumber, garageTicket);
        }

        public List<string> GetVehicleListByStatus(bool i_AllVehicles = false,
                                                   eVehicleStatus i_Status = eVehicleStatus.InProgress)
        {
            List<string> licensesList = new List<string>();
            eVehicleStatus vehicleStatus = i_Status;

            foreach (string licenseNumber in r_GarageClientDetails.Keys)
            {
                if (i_AllVehicles || r_GarageClientDetails[licenseNumber].VehicleStatus == vehicleStatus)
                {
                    licensesList.Add(licenseNumber);
                }
            }

            return licensesList;
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_Status)
        {
            r_GarageClientDetails[i_LicenseNumber].VehicleStatus = i_Status;
        }

        public void InflateWheelToMax(string i_LicenseNumber)
        {
            foreach (Wheel currentWheel in r_GarageClientDetails[i_LicenseNumber].Vehicle.Wheels)
            {
                currentWheel.InflateWheel(currentWheel.CalculateMissingAirPressureToMax());
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountOfFuel)
        {
            Fuel fuel = r_GarageClientDetails[i_LicenseNumber].Vehicle.PowerSource as Fuel;

            if (fuel != null)
            {
                fuel.AddPowerSource(i_AmountOfFuel, i_FuelType);
                r_GarageClientDetails[i_LicenseNumber].Vehicle.SetEnergyPercentageDetails();
            }
            else
            {
                throw new ArgumentException("The vehicle is not fuel type");
            }
        }

        public Type GetUniqueDetailsType(Vehicle i_Vehicle, string i_PropertyName)
        {
            PropertyInfo property = i_Vehicle.GetType().GetProperty(i_PropertyName);
            Type result = null;

            if (property != null && property.CanWrite)
            {
                result = property.PropertyType;
            }

            return result;
        }

        public void SetUniqueDetails(Vehicle i_Vehicle, string i_PropertyName, object i_PropertyValue)
        {
            PropertyInfo property = i_Vehicle.GetType().GetProperty(i_PropertyName);

            if (property != null && property.CanWrite)
            {
                property.SetValue(i_Vehicle, i_PropertyValue);
            }
        }

        public Dictionary<string, string> GetUniqueDetails(Vehicle i_Vehicle)
        {
            return r_TypeBuilder.GetPropertyMessage(i_Vehicle);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_BatteryHoursToAdd)
        {
            Electric electric = r_GarageClientDetails[i_LicenseNumber].Vehicle.PowerSource as Electric;

            if (electric != null)
            {
                electric.AddPowerSource(i_BatteryHoursToAdd);
                r_GarageClientDetails[i_LicenseNumber].Vehicle.SetEnergyPercentageDetails();
            }
            else
            {
                throw new ArgumentException("The vehicle is not electric type");
            }
        }
    }
}