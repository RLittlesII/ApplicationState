namespace State.Network
{
    public enum NetworkMachineState
    {
        /// <summary>
        /// The initial network access.
        /// </summary>
        Initial,

        /// <summary>
        /// Application has network access
        /// </summary>
        Online,

        /// <summary>
        /// Application had no network access
        /// </summary>
        Offline
    }
}