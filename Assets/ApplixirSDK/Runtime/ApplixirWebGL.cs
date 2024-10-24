using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AOT;
using UnityEngine;

namespace ApplixirSDK.Runtime
{
    internal delegate void SimpleCallback(int val);

    public static class ApplixirWebGL
    {
        #region Public methods

        /// <summary>
        /// Initialise the SDK by loading the AppLixirAdsConfig.
        /// Be sure to create o=config in your resources and
        /// fill thre appropriate values. 
        /// </summary>
        public static void Initialise()
        {
            var config = Resources.Load<AppLixirAdsConfig>("AppLixirAdsConfig");

            if (config != null)
            {
                Debug.LogError("Please create AppLixirAdsConfig");
                _isInitialized = false;
                return;
            }

            _accountId = config.accountId;
            _siteId = config.siteId;
            _zoneId = config.zoneId;
            _logLevel = config.logLevel;
            _userId = GenerateUniqueId();
        }

        /// <summary>
        /// Calls out to the applixir service to show a video ad.
        /// Result is returned via the resultCallback.
        /// </summary>
        public static void PlayVideo(Action<PlayVideoResult> resultCallback)
        {
            if (_isInitialized == false)
            {
                LogError("Applixir SDK is not initialized. Use Initialise() before calling PlayVideo");
                resultCallback?.Invoke(PlayVideoResult.AdsUnavailable);
                return;
            }

            _videoResultCallback = resultCallback;
            ShowVideo(_accountId, _siteId, _zoneId, _userId, ApplixirEventHandler, (int)_logLevel);
        }

        #endregion

        #region Private

        private const string Tag = "ApplixirWebGL";

        [DllImport("__Internal")]
        private static extern void ShowVideo(
            int accountId,
            int siteId,
            int zoneId,
            int userId,
            SimpleCallback onCompleted,
            int verbosity);

        private static int _accountId;
        private static int _siteId;
        private static int _zoneId;
        private static int _userId;
        private static LogLevel _logLevel;
        private static bool _isInitialized;
        private static Action<PlayVideoResult> _videoResultCallback;


        [MonoPInvokeCallback(typeof(SimpleCallback))]
        private static void ApplixirEventHandler(int result)
        {
            Log("Got Video result: " + result);
            PlayVideoResult pvr = result switch
            {
                1 => PlayVideoResult.ADWatched,
                2 => PlayVideoResult.NetworkError,
                3 => PlayVideoResult.ADBlocker,
                4 => PlayVideoResult.ADInterrupted,
                5 => PlayVideoResult.AdsUnavailable,
                6 => PlayVideoResult.CorsError,
                7 => PlayVideoResult.NoZoneid,
                8 => PlayVideoResult.ADStarted,
                9 => PlayVideoResult.SysClosing,
                10 => PlayVideoResult.ADRewarded,
                11 => PlayVideoResult.ADReady,
                12 => PlayVideoResult.ADRejected,
                13 => PlayVideoResult.ADMaximum,
                14 => PlayVideoResult.ADViolation,
                _ => PlayVideoResult.AdsUnavailable
            };

            _videoResultCallback?.Invoke(pvr);
        }

        static int GenerateUniqueId()
        {
            MD5 md5 = MD5.Create();
            try
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(SystemInfo.deviceUniqueIdentifier));
                int hashCode = BitConverter.ToInt32(hashBytes, 0);

                return hashCode;
            }
            finally
            {
                md5.Dispose();
            }
        }

        private static void Log(string message)
        {
            if (_logLevel >= LogLevel.Info)
            {
                Debug.Log($"[{Tag}] {message}");
            }
        }

        private static void LogError(string message)
        {
            if (_logLevel >= LogLevel.Error)
            {
                Debug.LogError($"[{Tag}] {message}");
            }
        }

        #endregion
    }
}