using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents
{
    public class CscUI : MonoBehaviour
    {
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Dismiss()
        {
            gameObject.SetActive(false);
        }
    }
}