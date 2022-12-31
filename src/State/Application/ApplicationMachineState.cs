namespace State.Application
{
    public enum ApplicationMachineState
    {
        /// <summary>
        /// The initial state of the application before any other state can be determined.
        /// </summary>
        Initial,

        /// <summary>
        /// Application is in Background
        /// </summary>
        Background,

        /// <summary>
        /// Application is in Foreground
        /// </summary>
        Foreground,
    }
}