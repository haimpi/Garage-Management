using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_VehicleModel;
        private readonly string r_LicenseNumber;
        private float m_EnergyPercentageLeft;
        private readonly List<Wheel> r_Wheels;
        private readonly PowerSource r_PowerSource;

        protected Vehicle(string i_LicenseNumber, int i_NumberOfWheels, string i_ModelName, PowerSource i_SourceEnergy)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>(i_NumberOfWheels);
            r_VehicleModel = i_ModelName;
            r_PowerSource = i_SourceEnergy;
        }

        public abstract void SetPowerSourceDetails();

        public abstract void SetEnergyPercentageDetails();

        public void InstallWheels(float i_MaxAirPressure)
        {
            for (int i = 0; i < Wheels.Capacity; i++)
            {
                Wheel currentWheel = new Wheel(i_MaxAirPressure);
                Wheels.Add(currentWheel);
            }
        }

        public void SetWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.InflateWheel(i_CurrentAirPressure);
                wheel.ManufacturerName = i_ManufacturerName;
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                return r_PowerSource;
            } 
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        } 

        public string LicenseNumber => r_LicenseNumber;

        private string isElectricOrFuel()
        {
            string msg = null;

            if (this.PowerSource.GetType() == typeof(Fuel))
            {
                if (this.PowerSource is Fuel fuelVehicle)
                {
                    msg = $"The fuel type is: {fuelVehicle.FuelType}";
                }
            }
            else
            {
                msg = "This is an Electric vehicle";
            }

            return msg;
        }

        public void AddPowerSource(float i_PowerEnergy)
        {
            r_PowerSource.AddPowerSource(i_PowerEnergy);
            SetEnergyPercentageDetails();
        }

        public void UpdateCurrentEnergyPercent(float i_MaxFuelAmount, float i_MaxBatteryTime)
        {
            if (r_PowerSource is Fuel fuelVehicle)
            {
                m_EnergyPercentageLeft = (fuelVehicle.CurrentFuel / i_MaxFuelAmount) * 100;
            }
            else if (r_PowerSource is Electric electricVehicle)
            {
                m_EnergyPercentageLeft = (electricVehicle.BatteryTimeLeft / i_MaxBatteryTime) * 100;
            }
        }

        public override string ToString()
        {
            string vehicleDataAsString = string.Format(
                $@"License number : {r_LicenseNumber}
Model name : {r_VehicleModel}
Energy percentage left : {m_EnergyPercentageLeft}%
{isElectricOrFuel()}
Num of Wheels {Wheels.Count}:
{Wheels[0]}");

            return vehicleDataAsString;
        }
    }
}