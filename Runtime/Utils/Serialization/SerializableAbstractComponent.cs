using System;
using UnityEngine;

namespace ChickStar.CommonLibrary.Runtime.Utils.Serialization
{
    /// <summary>
    /// Interfaceや基底クラス T を継承するコンポーネントをシリアライズ可能にするためのクラス
    /// MonoBehaviourのフィールドでSerializableInterfaceを宣言することで、インスペクタ上でアサインできるようになる
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SerializableAbstractComponent<T> where T : Component
    {
        public GameObject? gameObject;
        private T? _body;
        private T[]? _bodies;

        public T? Body
        {
            get
            {
                if (gameObject == null)
                {
                    return null;
                }

                return _body ??= gameObject.GetComponent<T>();
            }
        }

        public T[]? Bodies
        {
            get
            {
                if (gameObject == null)
                {
                    return null;
                }

                return _bodies ??= gameObject.GetComponents<T>();
            }
        }
    }
}