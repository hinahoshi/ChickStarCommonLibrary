using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChickStar.CommonLibrary.UnityRuntime.Binder
{
    [RequireComponent(typeof(UIDocument))]
    public class RootView : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private void OnEnable()
        {
            /*_uiDocument = GetComponent<UIDocument>();
            var labelSlider = _uiDocument.rootVisualElement.Q<TemplateContainer>("LabelSlider");
            UnityEngine.Debug.Log(labelSlider?.GetType());

            var labelSliderView = new LabelSlider(labelSlider);*/
        }
    }

    public abstract class TemplateViewBase
    {
        private TemplateContainer _container;

        public TemplateViewBase(TemplateContainer templateContainer)
        {
            _container = templateContainer;
        }
    }

    public class LabelSlider : TemplateViewBase
    {
        private Label _label;
        private Slider _slider;

        public LabelSlider(TemplateContainer templateContainer) : base(templateContainer)
        {
            templateContainer.contentContainer.Q("LabelSlider");
            var a = templateContainer[0];
            UnityEngine.Debug.Log(a.GetType());
            /*_label = templateContainer.contentContainer.Q<Label>("Label");
            _slider = templateContainer.contentContainer.Q<Slider>("Slider");*/

            //_label.text = "うんこ";
        }
    }
}