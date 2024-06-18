using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumOfDoors;
        private const float k_MaxAirPressure = 30f;
        private const float k_MaxFuelLiter = 58f;
        private const float k_MaxBatteryLife = 4.8f;
        private const int k_NumberOfWheels = 4;

        public Car(string i_LicenseNumber, string i_ModelName, PowerSource i_Engine)
            : base(i_LicenseNumber, k_NumberOfWheels, i_ModelName, i_Engine) 
        {
            InstallWheels(k_MaxAirPressure);
            SetPowerSourceDetails();
        }

        public override void SetEnergyPercentageDetails()
        {
            UpdateCurrentEnergyPercent(k_MaxFuelLiter, k_MaxBatteryLife);
        }

        public override void SetPowerSourceDetails()
        {
            if (PowerSource is Fuel fuelVehicle)
            {
                fuelVehicle.FuelType = eFuelType.Octan95;
                PowerSource.UpdateMaxEnergyAmount(k_MaxFuelLiter);
            }
            else
            {
                PowerSource.UpdateMaxEnergyAmount(k_MaxBatteryLife);
            }
        }

        public override string ToString()
        {
            string carDataAsString = $@"{base.ToString()}
Car color - {m_CarColor}.
Car number of doors- {m_NumOfDoors}.";

            return carDataAsString;
        }

        public eCarColor CarColor
        {
            get => m_CarColor;
            set 
            {
                if(Enum.IsDefined(typeof(eCarColor), value))
                {
                    m_CarColor = value;
                }
                else
                {
                    throw new ArgumentException("Invalid chosen car color. Try again.");
                }
            }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get => m_NumOfDoors;
            set
            {
                if (Enum.IsDefined(typeof(eNumberOfDoors), value))
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    throw new FormatException("Invalid chosen number of doors. Try again.");
                }
            }
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public enum eCarColor
        {
            Blue = 1,
            White,
            Red,
            Yellow
        }
    }
}