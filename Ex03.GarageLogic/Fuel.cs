using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Fuel : PowerSource
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuel;
        private float m_MaxFuel;
        private const int k_MinFuel = 0;

        public float CurrentFuel
        {
            get { return m_CurrentFuel; }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override void AddPowerSource(float i_AddFuelToTank)
        {
            if (i_AddFuelToTank > 0 && m_CurrentFuel + i_AddFuelToTank <= m_MaxFuel)
            {
                m_CurrentFuel += i_AddFuelToTank;
            }
            else
            {
                string message = $"Invalid Fuel amount (you can add maximum {m_MaxFuel - m_CurrentFuel}L), tank capacity is between";
                throw new ValueOutOfRangeException(message, k_MinFuel, m_MaxFuel);
            }
        }

        public void AddPowerSource(float i_AddFuelToTank, eFuelType i_FuelType)
        {
            if (i_FuelType == FuelType)
            {
                AddPowerSource(i_AddFuelToTank);
            }
            else
            {
                string message = "Invalid fuel type.";
                throw new ArgumentException(message);
            }
        }

        public override void UpdateMaxEnergyAmount(float i_MaxPowerAmount)
        {
            m_MaxFuel = i_MaxPowerAmount;
        }
    }
}