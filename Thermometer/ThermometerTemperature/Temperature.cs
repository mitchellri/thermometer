using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermometer
{
    /// <summary>
    /// Temperature object holds value and unit.
    /// </summary>
    public class Temperature
    {
        public decimal Value { get; set; }
        public Unit Unit { get; set; }
        private static readonly TemperatureConverter _converter = new TemperatureConverter();
        public Temperature(decimal value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }
        public Temperature(Temperature temperature)
        {
            if (temperature != null)
            {
                Value = temperature.Value;
                Unit = temperature.Unit;
            }
        }
        /// <summary>
        /// Sets temperature's value and unit to the parameter temperature's value and unit.
        /// </summary>
        public void Copy(Temperature temperature)
        {
            if (temperature != null)
            {
                Value = temperature.Value;
                Unit = temperature.Unit;
            }
        }
        /// <summary>
        /// Returns new temperature object converted to the parameter unit.
        /// </summary>
        public Temperature ConvertTo(Unit unit)
        {
            return _converter.ToUnit(this,unit);
        }
        public bool GreaterThan(Temperature second)
        {
            if (second == null) return false;
            return Value > second.ConvertTo(Unit).Value;
        }
        public bool GreaterThanOrEqual(Temperature second)
        {
            if (second == null) return false;
            return Value >= second.ConvertTo(Unit).Value;
        }
        public bool LessThan(Temperature second)
        {
            if (second == null) return false;
            return Value < second.ConvertTo(Unit).Value;
        }
        public bool LessThanOrEqual(Temperature second)
        {
            if (second == null) return false;
            return Value <= second.ConvertTo(Unit).Value;
        }
        public bool Equal(Temperature second)
        {
            if (second == null) return false;
            return Value == second.ConvertTo(Unit).Value;
        }
        public Temperature Subtract(Temperature second)
        {
            if (second == null) return this;
            return new Temperature(Value-second.ConvertTo(Unit).Value,Unit);
        }
        public Temperature Add(Temperature second)
        {
            if (second == null) return this;
            return new Temperature(Value+second.ConvertTo(Unit).Value, Unit);
        }
    }
}
