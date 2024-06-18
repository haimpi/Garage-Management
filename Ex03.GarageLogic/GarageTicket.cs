using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageTicket
    {
        private readonly string r_VehicleOwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private readonly Vehicle r_Vehicle;

        public GarageTicket(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerNumber)
        {
            r_Vehicle = i_Vehicle;
            r_VehicleOwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerNumber;
            m_VehicleStatus = eVehicleStatus.InProgress;
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus;}
            set { m_VehicleStatus = value; }
        }

        public override string ToString()
        {
            string vehicleDataAsString = string.Format(
                $@"Vehicle details:

Owner Name: {r_VehicleOwnerName}
Owner Phone Number: {r_OwnerPhoneNumber}
Status in garage: {m_VehicleStatus}
{r_Vehicle}"
            );

            return vehicleDataAsString;
        }
    }
}