using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private float m_MaxBatteryAmount;
        public float MaxBatteryAmount
        {
            get { return m_MaxBatteryAmount; }
            set { m_MaxBatteryAmount = value; }
        }
        public override void AddEnergy(float i_BatteryHoursToAdd)
        {
            float maxEnergeyToAdd = m_MaxBatteryAmount - m_energyLevel;
            if (m_energyLevel + i_BatteryHoursToAdd > m_MaxBatteryAmount)
            {
                throw new ValueOutOfRangeException(0 ,maxEnergeyToAdd);
            }

            m_energyLevel += i_BatteryHoursToAdd;
        }
        public class ElectricCar : ElectricVehicle
        {
            private eCarColors m_Color; //enum?? where to define?
            private int m_NumberOfDoors;
            public ElectricCar (eCarColors i_Color, int i_NumberOfDoors, float i_CurrentAirPressure)
            {
                m_Color = i_Color;
                m_NumberOfDoors = i_NumberOfDoors;
                m_MaxBatteryAmount = MAX_CAR_BATTERY_LEVEL;
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

        public class ElectricBike : ElectricVehicle
        {
            private eBikeLicencesTypes m_LicenceType; //enum?? where to define?
            private int m_EngineCapacity;
            public ElectricBike(eBikeLicencesTypes i_LicenceType, int i_EngineCapacity, float i_CurrentAirPressure)
            {
                m_LicenceType = i_LicenceType;
                m_EngineCapacity = i_EngineCapacity;
                m_MaxBatteryAmount = MAX_BIKE_BATTERY_LEVEL;
                m_Wheels = new List<Wheel>(BIKE_WHEELS_AMOUNT);
                for (int i = 0; i < CAR_WHEELS_AMOUNT; i++)
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
    }
}
