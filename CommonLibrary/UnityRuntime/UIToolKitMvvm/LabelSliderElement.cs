using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChickStar.CommonLibrary.UnityRuntime.UIToolKitMvvm
{
    public class LabelSliderElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<LabelSliderElement, UxmlTraits>
        {
        }
    }


    public abstract class ViewBase : VisualElement
    {
    }

    public interface IView
    {
    }

    /*public abstract partial class ViewBase<TCreateType> : VisualElement, IView
        where TCreateType : ViewBase<TCreateType>, new()
    {
    }*/
}