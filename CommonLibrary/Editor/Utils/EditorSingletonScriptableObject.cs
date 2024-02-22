using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Utils
{
    public class EditorSingletonScriptableObject<T> : ScriptableObject where T : EditorSingletonScriptableObject<T>
    {
        /*private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance == null ? EditorGUIUtility.Load<T>(typeof(T).Name) : Instance;
            }

        }*/
    }
}