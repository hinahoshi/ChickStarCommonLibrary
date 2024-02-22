using UnityEditor;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common
{
    public abstract class CustomInspectorBase<T> : UnityEditor.Editor where T : Object
    {
        protected T Target;

        private void OnEnable()
        {
            InitializeSerializable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            InitializeSerializable();
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                DrawGui();
                if (changeCheck.changed)
                {
                    EditorUtility.SetDirty(Target);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void DrawGui();

        protected virtual void InitializeSerializable()
        {
            if (Target == null)
            {
                Target = (T)target;
            }
        }
    }
}