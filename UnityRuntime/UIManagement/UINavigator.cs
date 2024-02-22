using System.Collections.Generic;
using ChickStar.CommonLibrary.Runtime.Utils.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.UnityRuntime.UIManagement
{
    public class UINavigator : MonoBehaviour
    {
        public List<UINavigation<Button>> buttonNavigations;
        public List<UINavigation<InputField>> inputFieldNavigations;

        private void Awake()
        {
            RegisterNavigations(buttonNavigations);
            RegisterNavigations(inputFieldNavigations);
        }

        private void OnDestroy()
        {
            UnRegisterNavigations(buttonNavigations);
        }

        private void RegisterNavigations<T>(List<UINavigation<T>> navigations) where T : Selectable
        {
            foreach (var navigation in navigations)
            {
                navigation.Register();
            }
        }
        
        private void UnRegisterNavigations<T>(List<UINavigation<T>> navigations) where T : Selectable
        {
            foreach (var navigation in navigations)
            {
                navigation.UnRegister();
            }
        }
    }
}