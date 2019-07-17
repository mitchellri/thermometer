namespace Thermometer
{
    /// <summary>
    /// States used in thermometer alert state machine.
    /// </summary>
    /// <remarks>
    /// <para>Off state indicates alert is ready to be triggered.</para>
    /// <para>On state indicates the alert will be triggered.</para>
    /// <para>Threshold state indicates the alert will not be triggered when it would normally.</para>
    /// </remarks>
    public enum AlertState
    {
        Off = 0,
        On = 1,
        Threshold = 2
    }
}