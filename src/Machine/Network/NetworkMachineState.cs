namespace ApplicationState.Machine.Network;

public enum NetworkMachineState
{
    Initial,

    /// <summary>
    /// Application has network access
    /// </summary>
    Online,

    /// <summary>
    /// Application had no network access
    /// </summary>
    Offline,

    Unknown
}