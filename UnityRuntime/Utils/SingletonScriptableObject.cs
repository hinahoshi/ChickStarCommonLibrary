using System;
using ChickStar.CommonLibrary.Runtime.Logger;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.Utils
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject, IDisposable
        where T : SingletonScriptableObject<T>
    {
        #region Editor Only

#if UNITY_EDITOR

        public string AssetFileName => $"{ResourceName}.asset";
#endif

        #endregion

        private static string ResourceName => $"{typeof(T).Name}";

        private static T _instance;

        public static T Instance
        {
            get
            {
                var loadResult = Resources.LoadAll<T>(ResourceName);
                if (loadResult == null || loadResult.Length == 0)
                {
                    return null;
                }

                if (loadResult.Length > 1)
                {
                    CscEasyLogger.LogError(
                        $"ScriptableObject: {ResourceName} is duplicate. " +
                        "Index:0 is returned."
                    );

                    for (var i = 1; i < loadResult.Length; i++)
                    {
                        Resources.UnloadAsset(loadResult[i]);
                    }
                }

                loadResult[0].Initialize();
                return loadResult[0];
            }
        }

        public void Dispose()
        {
            Resources.UnloadAsset(_instance);
            _instance = null;
        }

        protected virtual void Initialize()
        {
        }
    }
}