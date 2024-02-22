using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Common.DrawUtils.Drawer
{
    [Serializable]
    public class SearchFieldDrawer
    {
        [SerializeField] private string searchText;
        private SearchField _searchField;

        public string Draw()
        {
            // SearchFieldはGetPermanentControlIDを行うので、シリアライズ時の初期化ができないので、
            // Drawの中でnullチェックをかけ続ける必要がある
            _searchField ??= new SearchField();
            searchText = _searchField.OnToolbarGUI(searchText);
            return searchText;
        }
    }
}