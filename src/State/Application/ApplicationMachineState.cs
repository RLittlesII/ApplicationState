namespace ApplicationState.Application
{
    public enum ApplicationMachineState
    {
        /// <summary>
        /// The initial state of the application before any other state can be determined.
        /// </summary>
        Initial,

        /// <summary>
        /// Application in Background
        /// </summary>
        Background,

        /// <summary>
        /// Application in Foreground
        /// </summary>
        Foreground,
    }
}