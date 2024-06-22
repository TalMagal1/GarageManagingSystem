using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;
        public float MinValue
        {
            get { return r_MinValue; }
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base($"Value is out of range. It should be between {i_MinValue} and {i_MaxValue}.")
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(string message, float i_MinValue, float i_MaxValue)
            : base(message)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(string message, float i_MinValue, float i_MaxValue, Exception innerException)
            : base(message, innerException)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}
