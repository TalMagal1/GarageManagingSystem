using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.ElectricVehicle;
using static Ex03.GarageLogic.FuelVehicle;
using static Ex03.GarageLogic.Garage.Customer;
using static Ex03.GarageLogic.Vehicle;

namespace Ex03.GarageLogic
{
    public class Garage //public??
    {
        public const string FUEL_CAR = "1";
        public const string E_CAR = "2";
        public const string FUEL_BIKE = "3";
        public const string E_BIKE = "4";
        public const string TRUCK = "5";

        public List<Customer> m_Inventory = new List<Customer>();
        public Customer AddNewCustomer(Dictionary<string, string> i_Form)
        {
            Customer newCustomer = new Customer();
            newCustomer.Name = i_Form["Name"];
            newCustomer.Phone = i_Form["Phone Number"];
            newCustomer.RepairStatus = Customer.eRepairStatus.OnProgress;
            
            Vehicle newVehicle = createNewVehicle(i_Form);
            newCustomer.Vehicle = newVehicle;
            
            m_Inventory.Add(newCustomer);
            Console.WriteLine("Welcome new customer! Inventory now have {0} customers", m_Inventory.Count);
            return newCustomer;
        }
        private Vehicle createNewVehicle(Dictionary<string, string> i_Form)
        {
            string carType = i_Form["Vehicle Type"];
            Vehicle resultVehicle;
            switch (carType)
            {
                case FUEL_CAR:
                    resultVehicle =  new FuelCar((eCarColors)(int.Parse(i_Form["Vehicle Color (Cars)"])),
                                                    int.Parse(i_Form["Number Of Doors (Cars)"]),
                                                    int.Parse(i_Form["Current Tire Air Pressure"]));
                    resultVehicle.ModelName = "Fuel Car";
                    break;
                case FUEL_BIKE:
                    resultVehicle = new FuelBike((eBikeLicencesTypes)(int.Parse(i_Form["Licence Type (Bikes)"])),
                                                    int.Parse(i_Form["Engine Capacity (Bikes)"]),
                                                    int.Parse(i_Form["Current Tire Air Pressure"]));
                    resultVehicle.ModelName = "Fuel Bike";
                    break;
                case E_BIKE:
                    resultVehicle = new ElectricBike((eBikeLicencesTypes)(int.Parse(i_Form["Licence Type (Bikes)"])),
                                                    int.Parse(i_Form["Engine Capacity (Bikes)"]),
                                                    int.Parse(i_Form["Current Tire Air Pressure"]));
                    resultVehicle.ModelName = "E Bike";
                    break;
                case E_CAR:
                    resultVehicle = new ElectricCar((eCarColors)(int.Parse(i_Form["Vehicle Color (Cars)"])),
                                                    int.Parse(i_Form["Number Of Doors (Cars)"]),
                                                    int.Parse(i_Form["Current Tire Air Pressure"]));
                    resultVehicle.ModelName = "E Car";
                    break;
                case TRUCK:
                    resultVehicle = new FuelTruck(bool.Parse(i_Form["Is Toxic (Truck)"]),
                                                    int.Parse(i_Form["Load Vol (Truck)"]),
                                                    int.Parse(i_Form["Current Tire Air Pressure"]));
                    resultVehicle.ModelName = "Truck";
                    break;
                default:
                    throw new ArgumentException("Vehicle type is invalid");
            }

            resultVehicle.EnergyLevel = float.Parse(i_Form["Current Energy Level"]);
            resultVehicle.LicenceNumber = i_Form["Licence Number"];
            return resultVehicle;
        }
        public bool IsVehicleInGarage(string carLicence)
        {
            bool res = false;
            foreach (var customer in m_Inventory)
            {
                if (customer.Vehicle.LicenceNumber == carLicence)
                {
                    customer.RepairStatus = Customer.eRepairStatus.OnProgress;
                    Console.WriteLine("We already have your vehicle - we changed the status to \"On Progress\"");
                    res = true;
                }
            }
            return res;
        }
        public List<Customer> GetInventoryCarsByStatus(int status)
        {
            List<Customer> customersByStatus = new List<Customer>(0);
            if (!Enum.IsDefined(typeof(eRepairStatus), status))
            {
                customersByStatus = m_Inventory;
            }
            else
            {
                eRepairStatus eStatus = (eRepairStatus)status;
                foreach (var customer in m_Inventory) 
                {
                    if (customer.RepairStatus == eStatus)
                    {
                        customersByStatus.Add(customer);    
                    }

                }
            }
            return customersByStatus;
        }
        public void ChangeRepairStatus(int i_Status, string i_LicenceID)
        {
            Customer foundCust = m_Inventory.Find(cust => cust.Vehicle.LicenceNumber == i_LicenceID);
            if (foundCust == null)
            {
                throw new ArgumentException("Didnt found a vehicle with this ID");
            }
            if (Enum.IsDefined(typeof(eRepairStatus), i_Status))
            {
                foundCust.RepairStatus = (eRepairStatus)i_Status;
            }
            else
            {
                throw new ArgumentException("Invalid Status");
            }
        }
        public void InflateAllWheelsToMaxByID(string i_ID)
        {
            Customer foundCust = m_Inventory.Find(cust => cust.Vehicle.LicenceNumber == i_ID);
            if (foundCust == null)
            {
                throw new ArgumentException("Didnt found a vehicle with this ID");
            }
            else
            {
                foreach (var wheel in foundCust.Vehicle.Wheels) 
                {
                    wheel.InflateToMax(wheel.MaxPressure);
                }
            }
        }
        public void FillFuelByID(float i_ToAdd, int i_FuelType, string i_ID)
        {
            Customer foundCust = m_Inventory.Find(cust => cust.Vehicle.LicenceNumber == i_ID);
            if (foundCust == null)
            {
                throw new ArgumentException("Didnt found a vehicle with this ID");
            }
            if (!Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                throw new ArgumentException("Invalid input for Fuel Type");
            }
            eFuelType eFuelType = (eFuelType)i_FuelType;
            if (((FuelVehicle)(foundCust.Vehicle)).FuelType == eFuelType)
            {
                ((FuelVehicle)(foundCust.Vehicle)).AddEnergy(i_ToAdd);
            }
            else
            {
                throw new ArgumentException("Fuel type is not matching");
            }
        }
        public void FillBatteryByID(float i_ToAdd, string i_ID)
        {
            Customer foundCust = m_Inventory.Find(cust => cust.Vehicle.LicenceNumber == i_ID);
            if (foundCust == null)
            {
                throw new ArgumentException("Didnt found a vehicle with this ID");
            }
            ((ElectricVehicle)(foundCust.Vehicle)).AddEnergy(i_ToAdd / 60);
            //foundCust.Vehicle.AddEnergy(i_ToAdd/60);
        }
        public Customer GetCustomerByID(string i_ID)
        {
            Customer foundCust = m_Inventory.Find(cust => cust.Vehicle.LicenceNumber == i_ID);
            if (foundCust == null)
            {
                throw new ArgumentException("Didnt found a vehicle with this ID");
            }
            return foundCust;
        }
        public class Customer
        {
            public enum eRepairStatus
            {
                OnProgress = 1,
                Fixed,
                Paid,
            }

            private string m_Name;
            private string m_Phone;
            private eRepairStatus m_RepairStatus;
            private Vehicle m_Vehicle;

            public string Name
            {
                get { return m_Name; }
                set { m_Name = value; }
            }
            public string Phone
            {
                get { return m_Phone; }
                set { m_Phone = value; }
            }
            public eRepairStatus RepairStatus
            {
                get { return m_RepairStatus; }
                set { m_RepairStatus = value; }
            }
            public Vehicle Vehicle
            {
                get { return m_Vehicle; }
                set { m_Vehicle = value; }
            }
        }
    }

}
