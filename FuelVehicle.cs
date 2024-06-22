using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class FuelVehicle : Vehicle
    {
        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler
        }

        private eFuelType m_FuelType;
        private float m_MaxFuelAmount;
        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }
        public float MaxFuelAmount
        {
            get { return m_MaxFuelAmount; }
            set { m_MaxFuelAmount = value; }
        }
        public override void AddEnergy(float i_FuelToAdd)
        {
            float maxEnergeyToAdd = m_MaxFuelAmount - m_energyLevel;
            if (m_energyLevel + i_FuelToAdd > m_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, maxEnergeyToAdd);
            }
            m_energyLevel += i_FuelToAdd;
        }

        public class FuelCar : FuelVehicle
        {
            private eCarColors m_Color; //enum?? where to define?
            private int m_NumberOfDoors;
            public FuelCar(eCarColors i_Color, int i_NumberOfDoors, float i_CurrentAirPressure)
            {
                m_Color = i_Color;
                m_NumberOfDoors = i_NumberOfDoors;
                m_MaxFuelAmount = MAX_CAR_FUEL_LEVEL;
                m_FuelType = eFuelType.Octan95;
                m_Wheels = new List<Wheel>(CAR_WHEELS_AMOUNT);
                for (int i = 0; i < CAR_WHEELS_AMOUNT; i++)
                {
                    m_Wheels.Add(new Wheel("GoodYear", MAX_CAR_AIR_PRESSURE));
                }
                SetAllWheelsAirPressure(i_CurrentAirPressure);
            }
            public eCarColors Color
            {
                get { return m_Color; }
                set { m_Color = value; }
            }
            public int NumberOfDoors
            {
                get { return m_NumberOfDoors; }
                set { m_NumberOfDoors = value; }
            }

        }

        public class FuelBike : FuelVehicle
        {
            private eBikeLicencesTypes m_LicenceType; //enum?? where to define?
            private int m_EngineCapacity;
            public FuelBike(eBikeLicencesTypes i_LicenceType, int i_EngineCapacity, float i_CurrentAirPressure)
            {
                m_LicenceType = i_LicenceType;
                m_EngineCapacity = i_EngineCapacity;
                m_MaxFuelAmount = MAX_BIKE_FUEL_LEVEL;
                m_FuelType = eFuelType.Octan98;
                m_Wheels = new List<Wheel>(BIKE_WHEELS_AMOUNT);
                for (int i = 0; i < BIKE_WHEELS_AMOUNT; i++)
                {
                    m_Wheels.Add(new Wheel("GoodYear", MAX_BIKE_AIR_PRESSURE));
                }
                SetAllWheelsAirPressure(i_CurrentAirPressure);
            }
            public eBikeLicencesTypes LicenceType
            {
                get { return m_LicenceType; }
                set { m_LicenceType = value; }
            }
            public int EngineCapacity
            {
                get { return m_EngineCapacity; }
                set { m_EngineCapacity = value; }
            }
        }

        public class FuelTruck : FuelVehicle
        {
            private readonly eFuelType r_FuelType = eFuelType.Soler;
            private bool m_IsMovingToxic;
            private int m_loadVol;
            public FuelTruck(bool i_IsMovingToxic, int i_loadVol, float i_CurrentAirPressure)
            {
                m_IsMovingToxic = i_IsMovingToxic;
                m_MaxFuelAmount = MAX_TRUCK_FUEL_LEVEL;
                m_loadVol = i_loadVol;
                m_FuelType = eFuelType.Soler;
                m_Wheels = new List<Wheel>(TRUCK_WHEELS_AMOUNT);
                for (int i = 0; i < TRUCK_WHEELS_AMOUNT; i++)
                {
                    m_Wheels.Add(new Wheel("GoodYear", MAX_TRUCK_AIR_PRESSURE));
                }
                SetAllWheelsAirPressure(i_CurrentAirPressure);
            }
            public bool IsMovingToxic
            {
                get { return m_IsMovingToxic; }
                set { m_IsMovingToxic = value; }
            }
            public int LoadVol
            {
                get { return m_loadVol; }
                set { m_loadVol = value; }
            }
        }

    }
}
