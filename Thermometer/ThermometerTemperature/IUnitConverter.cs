using System.Collections.Generic;

namespace Thermometer
{
    /// <summary>
    /// Interface for unit converters.
    /// </summary>
    public interface IUnitConverter
    {
        Temperature ToFahrenheit(Temperature temperature);
        Temperature ToCelsius(Temperature temperature);
        bool IsValidUnit(Unit unit);
    }
}