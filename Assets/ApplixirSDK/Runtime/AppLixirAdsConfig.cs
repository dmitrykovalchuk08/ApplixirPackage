using UnityEngine;

namespace ApplixirSDK.Runtime
{
    [CreateAssetMenu(fileName = "AppLixirAdsConfig", menuName = "AppLixir/AdsConfig")]
    public class AppLixirAdsConfig : ScriptableObject
    {
        public int accountId;
        public int siteId;
        public int zoneId;
        public LogLevel logLevel = LogLevel.None;
    }
}