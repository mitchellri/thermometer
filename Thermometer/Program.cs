using System;
using System.Linq;
using System.Globalization;

namespace Thermometer
{
    /// <summary>
    /// Use with console by default. Parameters may be passed for different implemented input methods. Created using .NET 4.5 with Visual Studio 2017.
    /// </summary>
    /// <remarks>
    /// 'console' parameter supported for console input.
    /// Use exit code 'exit' to exit program.
    /// </remarks>
    /// <param name="inputMethod">Method to input values to thermometer</param>
    class Program
    {
        private readonly static Temperature freezingTemperature = new Temperature(0, Unit.Celsius);
        private readonly static Temperature freezingThreshold = new Temperature(.5m, Unit.Celsius);
        private readonly static Temperature boilingTemperature = new Temperature(100, Unit.Celsius);
        private readonly static Temperature boilingThreshold = new Temperature(.5m, Unit.Celsius);
        private readonly static Action<ThermometerClient> defaultMethod = RunConsole;
        private const string ExitCode = "EXIT";
        private static void RunConsole(ThermometerClient client)
        {
            String input;
            string[] inputTokens;
            bool exit = false;
            Temperature indexTemperature = new Temperature(0m, Unit.Celsius);
            CultureInfo[] cultures = { new CultureInfo("en-US"), new CultureInfo("fr-FR") };
            while (!exit)
            {
                input = Console.ReadLine().ToUpper().Trim();
                if (input == ExitCode) exit = true;
                else
                {
                    inputTokens = input.Split(' ').Where(val => !string.IsNullOrEmpty(val)).ToArray();
                    if (inputTokens.Length > 1) for (int i = 0; i + 1 < inputTokens.Length; i += 2)
                        {
                            if (inputTokens[i] == ExitCode || inputTokens[i + 1] == ExitCode) return;
                            else if (Decimal.TryParse(inputTokens[i], out Decimal num) && inputTokens[i + 1].Length == 1 && Enum.IsDefined(typeof(Unit), (Unit)inputTokens[i + 1][0]))
                            {
                                indexTemperature.Value = Convert.ToDecimal(inputTokens[i]);
                                indexTemperature.Unit = (Unit)Convert.ToChar(inputTokens[i + 1][0]);
                                client.UpdateTemperature(indexTemperature);
                            }
                        }
                    if (inputTokens[inputTokens.Length - 1] == ExitCode) break;
                }
            }
        }
        /// <summary>
        /// Use with console by default. Parameters may be passed for different implemented input methods.
        /// </summary>
        /// <remarks>
        /// 'console' parameter supported for console input.
        /// Use exit code 'exit' to exit program
        /// </remarks>
        /// /// <param name="inputMethod">Method to input values to thermometer</param>
        static void Main(string[] args)
        {
            ThermometerClient client = new AlertThermometerClient(
                    new ThermometerAlert("Freezing alert", delegate (Temperature oldTemperature, Temperature newTemperature, AlertState alertState)
                    {
                        Temperature greaterThresholdFreezing = freezingTemperature.Add(freezingThreshold);
                        bool isFreezing = newTemperature == null ? false : newTemperature.LessThanOrEqual(freezingTemperature);
                        bool isThreshold = newTemperature == null ? false : newTemperature.LessThanOrEqual(greaterThresholdFreezing);
                        bool wasFreezing = oldTemperature == null ? false : oldTemperature.LessThanOrEqual(freezingTemperature);
                        bool wasThreshold = oldTemperature == null ? false : oldTemperature.LessThanOrEqual(greaterThresholdFreezing);
                        AlertState newAlertState;
                        if (!wasFreezing && isFreezing && alertState == AlertState.Off) newAlertState = AlertState.On;
                        else if (wasThreshold && isThreshold) newAlertState = AlertState.Threshold;
                        else newAlertState = AlertState.Off;
                        return newAlertState;
                    }),
                    new ThermometerAlert("Boiling alert", delegate (Temperature oldTemperature, Temperature newTemperature, AlertState alertState)
                    {
                        Temperature lesserThresholdBoiling = boilingTemperature.Subtract(boilingThreshold);
                        bool isBoiling = newTemperature == null ? false : newTemperature.GreaterThanOrEqual(boilingTemperature);
                        bool isThreshold = newTemperature == null ? false : newTemperature.GreaterThanOrEqual(lesserThresholdBoiling);
                        bool wasBoiling = oldTemperature == null ? false : oldTemperature.GreaterThanOrEqual(boilingTemperature);
                        bool wasThreshold = oldTemperature == null ? false : oldTemperature.GreaterThanOrEqual(lesserThresholdBoiling);
                        AlertState newAlertState;
                        if (!wasBoiling && isBoiling && alertState == AlertState.Off) newAlertState = AlertState.On;
                        else if (wasThreshold && isThreshold) newAlertState = AlertState.Threshold;
                        else newAlertState = AlertState.Off;
                        return newAlertState;
                    })
                );
            // Multithreading can be implemented for simultanious inputs
            // Input can be made to objects for multiple identical input types
            if (args.Length > 0) switch (args[0])
                {
                    case "console":
                        RunConsole(client);
                        break;
                    default:
                        defaultMethod(client);
                        break;
                }
            else defaultMethod(client);
        }
    }
}
