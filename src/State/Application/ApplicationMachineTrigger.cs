namespace State.Application
{
    public enum ApplicationMachineTrigger
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
        Notification
    }
}