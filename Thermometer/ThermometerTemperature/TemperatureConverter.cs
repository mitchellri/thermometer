using System;
using System.Linq;

namespace Thermometer
{
    /// <summary>
    /// Management object of temperature converters used for converting temperatures.
    /// </summary>
    class TemperatureConverter
    {
        public IUnitConverter[] Converters { get; set; }
        public TemperatureConverter()
        {
            Converters = new IUnitConverter[2];
            Converters[0] = new CelsiusConverter();
            Converters[1] = new FarenheitConverter();
        }
        public TemperatureConverter(params IUnitConverter[] converters)
        {
            Converters = converters;
        }
        /// <summary>
        /// Returns new temperature object converted to the parameter unit if the unit converter exists.
        /// </summary>
        /// <exception cref="System.NotSupportedException">
        /// Thrown when requested unit is not in converter collection, or is not supported.
        /// </exception>
        public Temperature ToUnit(Temperature temperature, Unit unit)
        {
            if (temperature == null) return temperature;
            if (temperature.Unit == unit) return temperature;
            Temperature convertedTemperature;
            IUnitConverter converter = GetConverter(temperature.Unit);
            if (converter == null) throw new NotSupportedException();
            else if (unit == Unit.Celsius) convertedTemperature = converter.ToCelsius(temperature);
            else if (unit == Unit.Fahrenheit) convertedTemperature = converter.ToFahrenheit(temperature);
            else throw new NotSupportedException();
            return convertedTemperature;
        }
        /// <summary>
        /// Returns new temperature object converted to the parameter unit if the unit converter exists.
        /// </summary>
        private IUnitConverter GetConverter(Unit unit)
        {
            return Converters.FirstOrDefault(converter => converter.IsValidUnit(unit));
        }
    }
}
