using System;
using System.Collections.Generic;
using System.Threading;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Garage;
namespace Ex03.ConsoleUI
{
    internal static class GarageUI
    {
        
        public static void NewCustomerFlow(Garage i_Garage)
        {
            Dictionary<string, string> newCustomerForm = FillNewForm(i_Garage);
            Garage.Customer currentCust = i_Garage.AddNewCustomer(newCustomerForm);
            PrintCustomerInfo(currentCust);
        }
        public static void PrintCustomerInfo(Garage.Customer i_currentCust)
        {
            Console.WriteLine("General Information:\n");
            string genralInfo = string.Format("Name: {0}\nPhone Number: {1}\nLicence Number: {2}\nVehicle Type: {3}\nCurrent Energy Level: {4}\nCurrent Tire Air Pressure: {5}",
                i_currentCust.Name, i_currentCust.Phone, i_currentCust.Vehicle.LicenceNumber, i_currentCust.Vehicle.ModelName, i_currentCust.Vehicle.EnergyLevel, i_currentCust.Vehicle.Wheels[0].CurrentPressure);
            Console.WriteLine(genralInfo);

            string vehicleType = i_currentCust.Vehicle.ModelName;
            string typeInfo;
            switch (vehicleType)
            {
                case "Fuel Car":
                    Console.WriteLine("\nCar Specific Information:");
                    FuelVehicle.FuelCar fuelCar = i_currentCust.Vehicle as FuelVehicle.FuelCar;
                    typeInfo = string.Format("Vehicle Color: {0}\nNumber Of Doors: {1}\nMax Air Pressure: {2}\nMax Fuel Capacity: {3}",
                        fuelCar.Color, fuelCar.NumberOfDoors, Vehicle.MAX_CAR_AIR_PRESSURE, fuelCar.MaxFuelAmount);
                    Console.WriteLine(typeInfo);
                    break;
                case "E Car":
                    Console.WriteLine("\nCar Specific Information:");
                    ElectricVehicle.ElectricCar eCar = i_currentCust.Vehicle as ElectricVehicle.ElectricCar;
                    typeInfo = string.Format("Vehicle Color: {0}\nNumber Of Doors: {1}\nMax Air Pressure: {2}\nMax Battery Capacity: {3}",
                        eCar.Color, eCar.NumberOfDoors, Vehicle.MAX_CAR_AIR_PRESSURE, eCar.MaxBatteryAmount);
                    Console.WriteLine(typeInfo);
                    break;
                case "Fuel Bike":
                    Console.WriteLine("\nBike Specific Information:");
                    FuelVehicle.FuelBike fuelBike = i_currentCust.Vehicle as FuelVehicle.FuelBike;
                    typeInfo = string.Format("Bike liecene: {0}\nEngine Capacity: {1}\nMax Air Pressure: {2}\nMax Fuel Capacity: {3}",
                        fuelBike.LicenceType, fuelBike.EngineCapacity, Vehicle.MAX_BIKE_AIR_PRESSURE, fuelBike.MaxFuelAmount);
                    Console.WriteLine(typeInfo);
                    break;
                case "E Bike":
                    Console.WriteLine("\nBike Specific Information:");
                    ElectricVehicle.ElectricBike eBike = i_currentCust.Vehicle as ElectricVehicle.ElectricBike;
                    typeInfo = string.Format("Bike liecene: {0}\nEngine Capacity: {1}\nMax Air Pressure: {2}\nMax Battery Capacity: {3}",
                        eBike.LicenceType, eBike.EngineCapacity, Vehicle.MAX_BIKE_AIR_PRESSURE, eBike.MaxBatteryAmount);
                    Console.WriteLine(typeInfo);
                    break;
                case "Truck":
                    Console.WriteLine("Truck Specific Information:");
                    FuelVehicle.FuelTruck truck = i_currentCust.Vehicle as FuelVehicle.FuelTruck;
                    typeInfo = string.Format("Is moving toxic: {0}\nLoad vol: {1}\nMax Air Pressure: {2}\nMax Fuel Capacity: {3}",
                        truck.IsMovingToxic, truck.LoadVol, Vehicle.MAX_TRUCK_AIR_PRESSURE, truck.MaxFuelAmount);
                    Console.WriteLine(typeInfo);
                    break;
                default:
                    Console.WriteLine("\nUnknown Vehicle Type");
                    break;
            }
        }
        private static Dictionary<string, string> FillNewForm(Garage i_Garage)
        {
            Dictionary<string, string> newCustomerForm = new Dictionary<string, string>()
            {
                { "Licence Number", null },
                { "Vehicle Type", null },
                { "Current Energy Level", null },
                { "Current Tire Air Pressure", null },
                { "Vehicle Color (Cars)", null },
                { "Number Of Doors (Cars)", null },
                { "Engine Capacity (Bikes)", null },
                { "Licence Type (Bikes)", null },
                { "Load Vol (Truck)", null },
                { "Is Toxic (Truck)", null },
                { "Name", null },
                { "Phone Number", null },
            };

            Console.WriteLine("Enter Car Licence ID: ");
            string carLicence = Console.ReadLine();
            if (!i_Garage.IsVehicleInGarage(carLicence))
            {
                newCustomerForm["Licence Number"] = carLicence;
                newCustomerForm["Name"] = GetNameInput();
                newCustomerForm["Phone Number"] = GetPhoneNumberInput();
                newCustomerForm["Vehicle Type"] = GetCarTypeInput();
                newCustomerForm["Current Energy Level"] = GetCurrentEnergyLevelInput(newCustomerForm["Vehicle Type"]);
                newCustomerForm["Current Tire Air Pressure"] = GetCurrentAirPressureInput(newCustomerForm["Vehicle Type"]);
                if (newCustomerForm["Vehicle Type"] == Garage.FUEL_CAR || newCustomerForm["Vehicle Type"] == Garage.E_CAR)
                {
                    newCustomerForm["Vehicle Color (Cars)"] = GetCarColor();
                    newCustomerForm["Number Of Doors (Cars)"] = GetDoorsNum();
                }
                if (newCustomerForm["Vehicle Type"] == Garage.E_BIKE || newCustomerForm["Vehicle Type"] == Garage.FUEL_BIKE)
                {
                    newCustomerForm["Licence Type (Bikes)"] = GetLicenseLevel();
                    newCustomerForm["Engine Capacity (Bikes)"] = GetBikeEngineCapacity();
                }
                if (newCustomerForm["Vehicle Type"] == Garage.TRUCK)
                {
                    newCustomerForm["Is Toxic (Truck)"] = IsToxicTruck();
                    newCustomerForm["Load Vol (Truck)"] = GetTruckLoadVolume();
                }
            }
            return newCustomerForm;
        }
        public static string Menu(Garage i_Garage)
        {
            Console.Clear();
            string menuOptions =
                "Hey! Welcome to our garage, please choose...:\n1 - Add new vehicle to the garage\n2 - Show current car id's in inventory\n3 - change your vehicle status\n4 - inflate your tires to the max\n5 - fill up fuel\n6 - charge up battery\n7 - show my vehicle details\n8 - Exit";
            Console.WriteLine(menuOptions);
            string menuSelction = Console.ReadLine();
            switch (menuSelction)
            {
                case "1":
                    NewCustomerFlow(i_Garage);
                    break;
                case "2":
                    while (true)
                    {
                        try
                        {
                            PrintInventoryCarsByStatus(i_Garage);
                            Console.WriteLine("\nTo Return to Menu Press Enter...");
                            Console.ReadLine();
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                case "3":
                    while (true)
                    {
                        try
                        {
                            GetAndChangeRepairStatus(i_Garage);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                case "4":
                    while (true)
                    {
                        try
                        {
                            GetAndInflateAllWheelsToMaxByID(i_Garage);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                case "5":
                    while (true)
                    {
                        try
                        {
                            GetAndFillFuelByID(i_Garage);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                case "6":
                    while (true)
                    {
                        try
                        {
                            GetAndFillBatteryByID(i_Garage);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                case "7":
                    while (true)
                    {
                        try
                        {
                            GetAndPrintCustomerInfoByID(i_Garage);
                            Console.WriteLine("\nTo Return to Menu Press Enter...");
                            Console.ReadLine();
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message + "Try Again..");
                        }
                    }
                    break;
                default:

                    break;
            }
            Console.WriteLine("Operation performed successfully");
            Thread.Sleep(1000);
            return menuSelction;
        }
        public static void PrintInventoryCarsByStatus(Garage i_Garage)
        {
            Console.WriteLine("Please Choose how to filter the Inventory:\n1 - On Progress\n2 - Fixed\n3 - Paid\nOther - All");
            int status = int.Parse(Console.ReadLine());
            List<Garage.Customer> filtered = i_Garage.GetInventoryCarsByStatus(status);
            foreach (Garage.Customer customer in filtered)
            {
                Console.WriteLine(customer.Vehicle.LicenceNumber);
            }
        }
        public static void GetAndChangeRepairStatus(Garage i_Garage)
        {

            Console.WriteLine("Enter The Licence Number of the Vehicle you want to update");
            string id = Console.ReadLine();
            Console.WriteLine("Please Choose the new status:\n1 - On Progress\n2 - Fixed\n3 - Paid");
            int status = int.Parse(Console.ReadLine());
            i_Garage.ChangeRepairStatus(status, id);
        }
        public static void GetAndInflateAllWheelsToMaxByID(Garage i_Garage)
        {
            Console.WriteLine("Enter The Licence Number of the Vehicle you want to update");
            string id = Console.ReadLine();
            i_Garage.InflateAllWheelsToMaxByID(id);
        }
        public static void GetAndFillFuelByID(Garage i_Garage)
        {
            Console.WriteLine("Enter The Licence Number of the Vehicle you want to update");
            string id = Console.ReadLine();
            Console.WriteLine("Which Fuel Do you want to add?\n1 - Octan98\n2 - Octan96\n3 - Octan95\n4 - Soler");
            int fuelType = int.Parse(Console.ReadLine());
            Console.WriteLine("How much to add?");
            float toAdd = float.Parse(Console.ReadLine());
            i_Garage.FillFuelByID(toAdd, fuelType, id);
        }
        public static void GetAndFillBatteryByID(Garage i_Garage)
        {
            Console.WriteLine("Enter The Licence Number of the Vehicle you want to update");
            string id = Console.ReadLine();    
            Console.WriteLine("How much to add? (in minutes) ");
            float toAdd = float.Parse(Console.ReadLine());
            i_Garage.FillBatteryByID(toAdd, id);
        }
        public static void GetAndPrintCustomerInfoByID(Garage i_Garage)
        {
            Console.WriteLine("Enter Your Vehicle Licence Number");
            string id = Console.ReadLine();
            Customer requestedCustomer = i_Garage.GetCustomerByID(id);
            PrintCustomerInfo(requestedCustomer);
        }

        private static string GetTruckLoadVolume()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter Truck load volume");                    
                    string truckLoad = Console.ReadLine();
                    if(int.Parse(truckLoad) <= 0)
                    {
                        throw new ValueOutOfRangeException("Truck load volume cannot be a negative number",1,10000);
                    }
                    return truckLoad;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }   
        }
        private static string GetBikeEngineCapacity()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter Engine Capacity");                    
                    string engineCapacity = Console.ReadLine();
                    if(int.Parse(engineCapacity) <= 0 || int.Parse(engineCapacity) > 1200)
                    {
                        throw new ValueOutOfRangeException("Engine Capacity Cannot be a negative number",1,1200);
                    }
                    return engineCapacity;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }   
        }
        private static string GetDoorsNum()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("How many doors in your car?"); 
                    string DoorsNum = Console.ReadLine();
                    if (Convert.ToInt32(DoorsNum) < 2 || Convert.ToInt32(DoorsNum) > 5)
                    {
                        throw new ValueOutOfRangeException(2, 5);
                    }
                    return DoorsNum;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }
        }
        private static string IsToxicTruck()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Does the truck can cool the products?: \n" + 
                                                "1 - Yes\n" +
                                                "2 - No");                    
                    string isToxic = Console.ReadLine();
                    if(isToxic != "1" && isToxic != "2")
                    {
                        throw new ValueOutOfRangeException(1,2);
                    }

                    return isToxic == "1" ? "true" : "false";
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }   
        }
        private static string GetLicenseLevel()
        {
            while (true)
            { 
                try
                {
                    Console.WriteLine("Enter The Bike Level: \n" + 
                                                "1 - A\n" +
                                                "2 - A1\n" +
                                                "3 - AA\n" + 
                                                "4 - B1");                    
                    string LicenceLevel = Console.ReadLine();
                    ValidateCarColorOrLicenceLevel(LicenceLevel);
                    return LicenceLevel;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }   
        }
        private static string GetCarColor()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter The Car Color: \n" + 
                                                "1 - Yellow\n" +
                                                "2 - White\n" +
                                                "3 - Red\n" + 
                                                "4 - Black");                    
                    string carColor = Console.ReadLine();
                    ValidateCarColorOrLicenceLevel(carColor);
                    return carColor;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }   
        }
        private static void ValidateCarColorOrLicenceLevel(string i_carColorInput)
        {
            int input = int.Parse(i_carColorInput);
            if (input > 4 || input < 0)
            {
                throw new ValueOutOfRangeException(0, 4);
            }
        }
        private static string GetNameInput()
        {
            Console.WriteLine("Enter Your Name: ");
            return (Console.ReadLine());
        }
        private static string GetPhoneNumberInput()
        {
            Console.WriteLine("Enter Your Phone Number: ");
            return (Console.ReadLine());
        }
        private static string GetCurrentEnergyLevelInput(string i_VehicleType)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Your Current Energy Level (Fuel/Liters | Battery/Hours) : ");
                    string EnergyLevel = Console.ReadLine();
                    ValidateEnergyLevelByType(EnergyLevel, i_VehicleType);
                    return EnergyLevel;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }
        }
        private static void ValidateEnergyLevelByType(string i_EnergyLevel, string i_VehicleType)
        {
            float EnergyLevel = float.Parse(i_EnergyLevel);

            switch (i_VehicleType)
            {
                case Garage.FUEL_CAR:
                    if (EnergyLevel > Vehicle.MAX_CAR_FUEL_LEVEL || EnergyLevel < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_CAR_FUEL_LEVEL);
                    }
                    break;

                case Garage.FUEL_BIKE:
                    if (EnergyLevel > Vehicle.MAX_BIKE_FUEL_LEVEL || EnergyLevel < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_BIKE_FUEL_LEVEL);
                    }
                    break;

                case Garage.E_BIKE:
                    if (EnergyLevel > Vehicle.MAX_BIKE_BATTERY_LEVEL || EnergyLevel < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_BIKE_BATTERY_LEVEL);
                    }
                    break;

                case Garage.E_CAR:
                    if (EnergyLevel > Vehicle.MAX_CAR_BATTERY_LEVEL || EnergyLevel < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_CAR_BATTERY_LEVEL);
                    }
                    break;

                case Garage.TRUCK:
                    if (EnergyLevel > Vehicle.MAX_TRUCK_FUEL_LEVEL || EnergyLevel < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_TRUCK_FUEL_LEVEL);
                    }
                    break;
            }
        }
        private static string GetCurrentAirPressureInput(string i_VehicleType)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Your Current Air Pressure: ");
                    string AirPressure = Console.ReadLine();
                    ValidateAirPressureByType(AirPressure, i_VehicleType);
                    return AirPressure;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }
        }
        private static void ValidateAirPressureByType(string i_AirPressure, string i_VehicleType)
        {
            float AirPressure = float.Parse(i_AirPressure);

            switch (i_VehicleType)
            {
                case Garage.FUEL_CAR:
                    if (AirPressure > Vehicle.MAX_CAR_AIR_PRESSURE || AirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_CAR_AIR_PRESSURE);
                    }
                    break;

                case Garage.FUEL_BIKE:
                    if (AirPressure > Vehicle.MAX_BIKE_AIR_PRESSURE || AirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_BIKE_AIR_PRESSURE);
                    }
                    break;

                case Garage.E_BIKE:
                    if (AirPressure > Vehicle.MAX_BIKE_AIR_PRESSURE || AirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_BIKE_AIR_PRESSURE);
                    }
                    break;

                case Garage.E_CAR:
                    if (AirPressure > Vehicle.MAX_CAR_AIR_PRESSURE || AirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_CAR_AIR_PRESSURE);
                    }
                    break;

                case Garage.TRUCK:
                    if (AirPressure > Vehicle.MAX_TRUCK_AIR_PRESSURE || AirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, Vehicle.MAX_TRUCK_AIR_PRESSURE);
                    }
                    break;
            }
        }
        private static string GetCarTypeInput()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter The Car Type:\n" +
                                      "1 - Fuel Car\n" +
                                      "2 - Electric Car\n" +
                                      "3 - Fuel Bike\n" +
                                      "4 - Electric Bike\n" +
                                      "5 - Truck");

                    string carType = Console.ReadLine();
                    ValidateCarType(carType);
                    return carType;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try Again..");
                }
            }
        }
        private static void ValidateCarType(string i_VehicleType)
        {
            int input = int.Parse(i_VehicleType);
            if (input > 5 || input < 0)
            {
                throw new ValueOutOfRangeException(0, 5);
            }
        }

    }
}
