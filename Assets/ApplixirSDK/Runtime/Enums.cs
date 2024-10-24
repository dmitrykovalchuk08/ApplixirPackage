namespace ApplixirSDK.Runtime
{
    public enum PlayVideoResult
    {
        None,

        /// <summary>
        /// an ad was presented and completed successfully
        /// </summary>
        ADWatched,

        /// <summary>
        /// no connectivity available or similar fatal network error
        /// </summary>
        NetworkError,

        /// <summary>
        /// an ad blocker was detected. Notify the user they must allow ads to receive rewards.
        /// </summary>
        ADBlocker,

        /// <summary>
        /// ad was ended abnormally and should not be rewarded
        /// </summary>
        ADInterrupted,

        /// <summary>
        /// no ads were returned to the player from the ad servers
        /// </summary>
        AdsUnavailable,

        /// <summary>
        /// A CORS error was returned (this should never happen unless you are not configured correctly)
        /// </summary>
        CorsError,

        /// <summary>
        /// There is no zoneId in the options
        /// </summary>
        NoZoneid,

        /// <summary>
        /// A video ad has been started (IMA only)
        /// </summary>
        ADStarted,

        /// <summary>
        /// The system has completed all processes and is closing
        /// </summary>
        SysClosing,

        /// <summary>
        /// (RMS only) The reward has been validated
        /// </summary>
        ADRewarded,

        /// <summary>
        /// An IMA video ad has loaded (no processing required, not used for GPT)
        /// </summary>
        ADReady,

        /// <summary>
        /// (RMS only) You returned a 418 error code from your RMS endpoint (due to invalid data detected)
        /// </summary>
        ADRejected,

        /// <summary>
        /// (RMS only) This user has already been granted the maximum rewards specified in your RMS settings
        /// </summary>
        ADMaximum,

        /// <summary>
        /// (RMS only) This user has maximum rate violations based on your RMS settings (they are likely scripting)
        /// </summary>
        ADViolation
    }

    public enum LogLevel
    {
        None = 0,
        Error = 1,
        Warn = 2,
        Info = 3,
        Debug = 4,
        Trace = 5
    }
}