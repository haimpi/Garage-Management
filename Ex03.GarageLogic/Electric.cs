using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Electric : PowerSource
    {
        private float m_BatteryTimeLeft;
        private float m_BatteryMaxTime;
        private const int k_MinEnergy = 0;

        public float BatteryTimeLeft
        {
            get { return m_BatteryTimeLeft; }
        }

        public override void UpdateMaxEnergyAmount(float i_MaxPowerAmount)
        {
            m_BatteryMaxTime = i_MaxPowerAmount;
        }

        public override void AddPowerSource(float i_AddHoursToBattery)
        {
            if(i_AddHoursToBattery > 0 && m_BatteryTimeLeft + i_AddHoursToBattery <= m_BatteryMaxTime)
            {
                m_BatteryTimeLeft += i_AddHoursToBattery;
            }
            else
            {
                string message = "Invalid input. The battery lifetime need to be between";
                throw new ValueOutOfRangeException(message, k_MinEnergy, m_BatteryMaxTime);
            }
        }
    }
}