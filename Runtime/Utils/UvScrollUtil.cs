using UnityEngine;

namespace ChickStar.CommonLibrary.Runtime.Utils
{
    public static class UvScrollUtil
    {
        public delegate void UvScrollDelegate(
            Material material,
            string propertyName,
            float speedX,
            float speedY,
            float repeatTiming
        );

        public static UvScrollDelegate CommonUvScroll()
        {
            return (material, propertyName, speedX, speedY, repeatTiming) =>
            {
                var x = Mathf.Repeat(Time.time * speedX, repeatTiming);
                var y = Mathf.Repeat(Time.time * speedY, repeatTiming);
                var offset = new Vector2(x, y);
                material.SetTextureOffset(propertyName, offset);
            };
        }

        public interface IUvScroller
        {
            public void Scroll(
                Material material,
                string propertyName,
                float speedX,
                float speedY,
                float repeatTiming
            );
        }
    }
}