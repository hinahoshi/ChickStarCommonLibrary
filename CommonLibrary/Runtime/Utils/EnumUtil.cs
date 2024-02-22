using System;

namespace ChickStar.CommonLibrary.Runtime.Utils
{
    public static class EnumUtil
    {
        public static int SizeOf<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}