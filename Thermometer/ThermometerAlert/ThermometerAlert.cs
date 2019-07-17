using System;

namespace Thermometer
{
    delegate AlertState AlertCondition(Temperature oldTemperature, Temperature newTemperature, AlertState alertState);
    /// <summary>
    /// Alert with user defined alert management condition is based off old and new temperatures.
    /// </summary>
    class ThermometerAlert
    {
        public string Message { get; set; }
        private AlertCondition _alertCondition { get; set; }
        private AlertState _alertState { get; set; }
        public ThermometerAlert(string message, AlertCondition alertCondition)
        {
            Message = message;
            _alertCondition = alertCondition;
            _alertState = AlertState.Off;
        }
        /// <summary>
        /// Updates alert state. Alert is triggered when state is on.
        /// </summary>
        public void Update(Temperature oldTemperature, Temperature newTemperature)
        {
            _alertState = _alertCondition(oldTemperature, newTemperature, _alertState);
            if (_alertState == AlertState.On) SendAlert();
        }
        private void SendAlert()
        {
            Console.WriteLine(Message);
        }
    }
}