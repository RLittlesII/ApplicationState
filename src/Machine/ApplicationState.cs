namespace ApplicationState.Machine
{
    public enum ApplicationState
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