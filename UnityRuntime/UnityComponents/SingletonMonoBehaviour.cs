using System;
using ChickStar.CommonLibrary.Runtime.Logger;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        protected abstract bool DontDestroy { get; }

        private string GameObjectName => $"Singleton_{typeof(T).Name}";

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var singletonGameObject = new GameObject();
                var singletonComponent = singletonGameObject.AddComponent<T>();
                singletonGameObject.name = singletonComponent.GameObjectName;
                singletonComponent.Initialize();
                singletonComponent.SetDontDestroy();

                _instance = singletonComponent;

                return _instance;
            }
        }

        protected abstract void Initialize();

        protected virtual void Awake()
        {
            SetInstanceInAwake();
        }

        private void SetInstanceInAwake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                SetDontDestroy();
            }
            else
            {
                CscEasyLogger.LogWarning($"{GameObjectName} has already been instantiated");
                DestroyImmediate(gameObject);
            }

            _instance.Initialize();
        }

        private void SetDontDestroy()
        {
            if (DontDestroy)
            {
                DontDestroyOnLoad(_instance);
            }
        }
    }
}