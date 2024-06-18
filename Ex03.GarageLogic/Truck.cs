using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsHazardousMaterials;
        private float m_CargoVolume;
        private const float k_MaxAirPressure = 28f;
        private const int k_NumOfWheels = 12;
        private const float k_MaxFuelLiter = 110f;

        public Truck(string i_LicenseNumber, string i_ModelName, PowerSource i_Engine)
            : base(i_LicenseNumber, k_NumOfWheels, i_ModelName, i_Engine)
        {
            InstallWheels(k_MaxAirPressure);
            SetPowerSourceDetails();
        }

        public override void SetEnergyPercentageDetails()
        {
            UpdateCurrentEnergyPercent(k_MaxFuelLiter, 0);
        }

        public override void SetPowerSourceDetails()
        {
            if (PowerSource is Fuel fuelVehicle)
            {
                fuelVehicle.FuelType = eFuelType.Soler;
                PowerSource.UpdateMaxEnergyAmount(k_MaxFuelLiter);
            }
        }

        public override string ToString()
        {
            string truckDataAsString = $@"{base.ToString()}
Truck can take hazardous materials : {m_IsHazardousMaterials}.
Truck cargo volume : {m_CargoVolume}.";

            return truckDataAsString;
        }

        public bool IsHazardousMaterials
        {
            get
            {
                return m_IsHazardousMaterials;
            }
            set 
            {
                if (value != true && value != false)
                {
                    throw new FormatException("Invalid value. Only 'true' or 'false' allowed.");
                }
                m_IsHazardousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (value > 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new FormatException("Invalid positive float number for Cargo Volume.");
                }
            }
        }
    }
}