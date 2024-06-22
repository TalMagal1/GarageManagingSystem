using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_modelName;
        private string m_licenceNumber;
        protected float m_energyLevel;
        protected List<Wheel> m_Wheels;

        public string ModelName
        {
            get { return m_modelName; }
            set { m_modelName = value; }
        }
        public string LicenceNumber
        {
            get { return m_licenceNumber; }
            set { m_licenceNumber = value; }
        }
        public float EnergyLevel
        {
            get { return m_energyLevel; }
            set { m_energyLevel = value; }
        }
        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public abstract void AddEnergy(float i_EnergyToAdd);
        public void SetAllWheelsAirPressure(float i_AirPressureToSet)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Inflate(i_AirPressureToSet);
            }
        }
        public enum eCarColors
        {
            Yellow = 1,
            White,
            Red, 
            Black
        }
        public enum eBikeLicencesTypes
        {
            A = 1,
            A1,
            AA,
            B1
        }

        public const float MAX_CAR_FUEL_LEVEL = 45f;
        public const float MAX_BIKE_FUEL_LEVEL = 5.5f;
        public const float MAX_TRUCK_FUEL_LEVEL = 45f;
        public const float MAX_CAR_BATTERY_LEVEL = 3.5f;
        public const float MAX_BIKE_BATTERY_LEVEL = 2.5f;
        public const float MAX_CAR_AIR_PRESSURE = 31f;
        public const float MAX_BIKE_AIR_PRESSURE = 33f;
        public const float MAX_TRUCK_AIR_PRESSURE = 28f;
        public const int CAR_WHEELS_AMOUNT = 5;
        public const int BIKE_WHEELS_AMOUNT = 2;
        public const int TRUCK_WHEELS_AMOUNT = 12;

        public class Wheel
        {
            private string m_manufacturer;
            private float m_maxAirPressure;
            private float m_currentAirPressure;

            public Wheel(string i_Manufacturer, float i_MaxAirPressure)
            {
                m_manufacturer = i_Manufacturer;
                m_maxAirPressure = i_MaxAirPressure;
                m_currentAirPressure = 0;
            }
            public float MaxPressure
            {
                get { return m_maxAirPressure; }
                set { m_maxAirPressure = value; }
            }
            public float CurrentPressure
            {
                get { return m_currentAirPressure; }
                set { m_currentAirPressure = value; }
            }
            public void Inflate(float i_ToAdd)
            {
                float maxAirToAdd = m_maxAirPressure - m_currentAirPressure;
                if (m_currentAirPressure + i_ToAdd > m_maxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, maxAirToAdd);
                }

                m_currentAirPressure += i_ToAdd;
            }
            public void InflateToMax(float i_Max)
            {
                m_currentAirPressure = i_Max;
            }

        }

    }

}
