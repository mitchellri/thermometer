using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermometer
{
    /// <summary>
    /// Converts temperatures from celsius.
    /// </summary>
    class CelsiusConverter : IUnitConverter
    {
        public Temperature ToCelsius(Temperature temperature)
        {
            if (temperature == null) return temperature;
            if (!IsValidUnit(temperature.Unit)) throw new NotSupportedException();
            return temperature;
        }
        public Temperature ToFahrenheit(Temperature temperature)
        {
            if (temperature == null) return temperature;
            if (!IsValidUnit(temperature.Unit)) throw new NotSupportedException();
            return new Temperature(temperature.Value * (9 / 5) + 32, Unit.Fahrenheit);
        }
        /// <summary>
        /// Unit is valid to be converted with this object.
        /// </summary>
        public bool IsValidUnit(Unit unit)
        {
            return unit == Unit.Celsius;
        }
    }
}
