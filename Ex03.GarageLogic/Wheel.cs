using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentWheelAirPressure;
        private readonly float r_MaxWheelAirPressure;
        private const float k_MinAirPressure = 0;

        public Wheel(float i_MaxWheelAirPressure)
        {
            r_MaxWheelAirPressure = i_MaxWheelAirPressure;
        }

        public void InflateWheel(float i_ChosenAirToInflate)
        {
            if(i_ChosenAirToInflate > 0 && m_CurrentWheelAirPressure + i_ChosenAirToInflate <= r_MaxWheelAirPressure)
            {
                m_CurrentWheelAirPressure += i_ChosenAirToInflate;
            }
            else
            {
                string message = "Invalid input. The air pressure need to be between";
                throw new ValueOutOfRangeException(message, k_MinAirPressure, r_MaxWheelAirPressure);
            }
        }
        
        public float CalculateMissingAirPressureToMax()
        {
            return r_MaxWheelAirPressure - m_CurrentWheelAirPressure;
        }

        public string ManufacturerName
        {
            get 
            {
                return m_ManufacturerName; 
            }
            set 
            {
                m_ManufacturerName = value; 
            }
        }

        public override string ToString()
        {
            return
                $"Wheel Manufacturer: {m_ManufacturerName}, Air Pressure: {m_CurrentWheelAirPressure}, Max Wheel Pressure:{r_MaxWheelAirPressure}";
        }
    }
}