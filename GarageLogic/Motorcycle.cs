using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineCapacity;
        private eMotorcycleLicenseType m_LicenseType;
        private const int k_NumOfWheels = 2;
        private const float k_MaxFuelLiter = 5.8f;
        private const float k_MaxAirPressure = 29f;
        private const float k_MaxBatteryLife = 2.8f;

        public Motorcycle(string i_LicenseNumber, string i_ModelName, PowerSource i_Engine)
            : base(i_LicenseNumber, k_NumOfWheels, i_ModelName, i_Engine)
        {
            InstallWheels(k_MaxAirPressure);
            SetPowerSourceDetails();
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
            set
            {
                if (value > 0)
                {
                    m_EngineCapacity = value;
                }
                else
                {
                    throw new FormatException("Invalid engine capacity. Try again.");
                }
            }
        }

        public eMotorcycleLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                if (Enum.IsDefined(typeof(eMotorcycleLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    throw new FormatException("The selected number of license does not exist in the system. Try again.");
                }
            }
        }

        public override void SetEnergyPercentageDetails()
        {
            UpdateCurrentEnergyPercent(k_MaxFuelLiter, k_MaxBatteryLife);
        }

        public override void SetPowerSourceDetails()
        {
            if (PowerSource is Fuel fuelVehicle)
            {
                fuelVehicle.FuelType = eFuelType.Ocatn98;
                PowerSource.UpdateMaxEnergyAmount(k_MaxFuelLiter);
            }
            else
            {
                PowerSource.UpdateMaxEnergyAmount(k_MaxBatteryLife);
            }
        }

        public override string ToString()
        {
            string motorcycleDataAsString = $@"{base.ToString()}
Motorcycle Engine capacity : {m_EngineCapacity}.
Motorcycle license type : {m_LicenseType.ToString()}.";

            return motorcycleDataAsString;
        }

        public enum eMotorcycleLicenseType
        {
            A1 = 1,
            A2,
            AB,
            B2
        }
    }
}