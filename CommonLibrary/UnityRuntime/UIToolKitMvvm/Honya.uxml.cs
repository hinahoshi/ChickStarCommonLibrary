using UnityEngine.UIElements;

namespace ChickStar.CommonLibrary.UnityRuntime.UIToolKitMvvm
{
    public class Honya : ViewBase
    {
        private Label _label;

        public Honya()
        {
            _label = this.Query<Label>();
        }
    }
}