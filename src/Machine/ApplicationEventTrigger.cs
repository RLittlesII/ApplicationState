namespace ApplicationState.Machine
{
    public enum ApplicationEventTrigger
    {
        /// <summary>
        /// Start the application.
        /// </summary>
        Start,

        /// <summary>
        /// Stop the application.
        /// </summary>
        Stop,

        /// <summary>
        /// Deeplink application start.
        /// </summary>
        Deeplink,

        /// <summary>
        /// Background notification launch.
        /// </summary>
        Notification,

        /// <summary>
        /// Connected to the network.
        /// </summary>
        Connected,

        /// <summary>
        /// Disconnected from the network.
        /// </summary>
        Disconnected,
    }
}