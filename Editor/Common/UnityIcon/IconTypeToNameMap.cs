using System.Collections.Generic;

namespace ChickStar.CommonLibrary.Editor.Common.UnityIcon
{
    /// <summary>
    /// todo IconTypeとこのマップを自動生成してあげられると便利
    /// </summary>
    public static partial class UnityIconDrawer
    {
        private static readonly Dictionary<IconType, string> Map = new()
        {
            { IconType.Loading, "Loading" },
            { IconType.Refresh, "Refresh" },
            { IconType.CrossIcon, "CrossIcon" },
            { IconType.CreateAddNew, "CreateAddNew" },
        };
    }
}