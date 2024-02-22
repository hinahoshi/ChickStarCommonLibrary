using System;
using ChickStar.CommonLibrary.Runtime.Logger;
using ChickStar.CommonLibrary.Runtime.Utils;
using UnityEngine;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents
{
    /// <summary>
    /// RendererにアタッチされたMaterialをUVスクロールするコンポーネント
    /// </summary>
    public class RendererBackGroundScrollerWithUv : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private int materialIndex;
        [SerializeField] private string texturePropertyName = "_MainTex";
        [SerializeField] private float speedX = 0.1f;
        [SerializeField] private float speedY = 0.1f;
        [SerializeField] private float repeatTiming = 1.0f;
        [SerializeField] private GameObject uvScrollerGameObject;

        private Material _material;
        private UvScrollUtil.UvScrollDelegate _uvScrollDelegate;


        private void Awake()
        {
            try
            {
                _material = renderer.materials[materialIndex];
            }
            catch (Exception e)
            {
                CscEasyLogger.LogException(e);
                enabled = false;
                return;
            }

            var uvScroller = uvScrollerGameObject.GetComponent<UvScrollUtil.IUvScroller>();
            _uvScrollDelegate = uvScroller != null ? uvScroller.Scroll : UvScrollUtil.CommonUvScroll();
        }

        private void Update()
        {
            if (_material == null)
            {
                return;
            }

            _uvScrollDelegate?.Invoke(
                material: _material,
                propertyName: texturePropertyName,
                speedX: speedX,
                speedY: speedY,
                repeatTiming: repeatTiming
            );
        }
    }
}