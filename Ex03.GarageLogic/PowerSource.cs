using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class PowerSource
    {
        public abstract void AddPowerSource(float i_AmountOfPowerSourceToAdd);

        public abstract void UpdateMaxEnergyAmount(float i_MaxPowerAmount);
    }
}