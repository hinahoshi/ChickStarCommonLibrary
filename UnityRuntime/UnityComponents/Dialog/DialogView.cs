using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChickStar.CommonLibrary.UnityRuntime.UnityComponents.Dialog
{
    public class DialogView : CscUI
    {
        [SerializeField] private Text title;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform contentsRoot;
        [SerializeField] private VerticalLayoutGroup contentsLayoutGroup;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button backdrop;

        public ScrollRect ScrollRect => scrollRect;
        public RectTransform ContentsRoot => contentsRoot;

        public void SetTitle(string titleText, Font font)
        {
            title.font = font;
            SetTitle(titleText);
        }

        public void SetTitle(string titleText)
        {
            title.text = titleText;
        }

        public void SetContentsAlignment(TextAnchor alignment)
        {
            contentsLayoutGroup.childAlignment = alignment;
        }

        public T AddContentFromPrefab<T>(T prefab) where T : UnityEngine.Object
        {
            return Instantiate(prefab, parent: contentsRoot);
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(Dismiss);
            backdrop.onClick.AddListener(Dismiss);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(Dismiss);
            backdrop.onClick.RemoveListener(Dismiss);
        }

        public override void Dismiss()
        {
            base.Dismiss();
            var childCount = contentsRoot.childCount;
            for (int i = 0; i < childCount; i++)
            {
                var child = contentsRoot.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }
    }
}