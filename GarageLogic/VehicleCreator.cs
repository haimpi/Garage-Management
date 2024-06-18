using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eVehicleType
        {
            Car =1,
            ElectricCar,
            Motorcycle,
            ElectricMotorcycle,
            Truck
        }

        public static Vehicle CreateVehicle(string i_LicenseNumber,string i_ModelName, eVehicleType i_VehicleType)
        {
            Vehicle vehicleToCreate = null;
             
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicleToCreate = new Car(i_LicenseNumber, i_ModelName, new Fuel());
                    break;
                case eVehicleType.ElectricCar:
                    vehicleToCreate = new Car(i_LicenseNumber, i_ModelName, new Electric());
                    break;
                case eVehicleType.Motorcycle:
                    vehicleToCreate = new Motorcycle(i_LicenseNumber, i_ModelName, new Fuel());
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicleToCreate = new Motorcycle(i_LicenseNumber, i_ModelName, new Electric());
                    break;
                case eVehicleType.Truck:
                    vehicleToCreate = new Truck(i_LicenseNumber, i_ModelName, new Fuel());
                    break;
            }

            return vehicleToCreate;
        }
    }
}