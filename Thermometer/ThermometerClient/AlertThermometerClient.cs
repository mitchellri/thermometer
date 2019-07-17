namespace Thermometer
{
    /// <summary>
    /// Base thermometer with user defined alarm state management methods.
    /// </summary>
    class AlertThermometerClient : ThermometerClient
    {
        public ThermometerAlert[] ThermometerAlerts { get; set; }
        public AlertThermometerClient(params ThermometerAlert[] thermometerAlerts) : base() {
            ThermometerAlerts = thermometerAlerts;
        }
        /// <summary>
        /// Updates alerts and sets temperature of thermometer.
        /// </summary>
        public override void UpdateTemperature(Temperature newTemperature)
        {
            foreach (ThermometerAlert thermometerAlert in ThermometerAlerts) thermometerAlert.Update(Temperature, newTemperature);
            base.UpdateTemperature(newTemperature);
        }
    }
}