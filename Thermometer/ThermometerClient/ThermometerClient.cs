namespace Thermometer
{
    /// <summary>
    /// Base thermometer.
    /// </summary>
    class ThermometerClient
    {
        public Temperature Temperature { get; private set; }
        public ThermometerClient() {
            Temperature = null;
        }
        /// <summary>
        /// Sets temperature of thermometer.
        /// </summary>
        public virtual void UpdateTemperature(Temperature newTemperature)
        {
            if (Temperature == null && newTemperature != null) Temperature = new Temperature(newTemperature);
            else if (Temperature != null) Temperature.Copy(newTemperature);
        }
    }
}
