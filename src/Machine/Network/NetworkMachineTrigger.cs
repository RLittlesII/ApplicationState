namespace ApplicationState.Machine.Network;

public enum NetworkMachineTrigger
{
    Initial,

    /// <summary>
    /// Connected to the network.
    /// </summary>
    Connected,

    /// <summary>
    /// Disconnected from the network.
    /// </summary>
    Disconnected,

    Unknown
}