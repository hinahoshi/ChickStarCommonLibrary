using System;
using ChickStar.CommonLibrary.Runtime.Logger;
using UnityEditor;

namespace ChickStar.CommonLibrary.Editor.Utils
{
    public static class AssetDatabaseUtil
    {
        /// <summary>
        /// UnityEngine.Objectをアセットとしてつくり、SaveAssets/Refreshをかける
        /// </summary>
        /// <param name="assetObject">アセットとして生成するオブジェクト</param>
        /// <param name="assetPath">保存先のパス</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>生成したアセットをLoadAssetAtPathして返却する</returns>
        public static T CreateAssetWithSave<T>(T assetObject, string assetPath) where T : UnityEngine.Object
        {
            try
            {
                AssetDatabase.CreateAsset(assetObject, assetPath);
                SaveAndRefresh();
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            catch (Exception e)
            {
                CscEasyLogger.LogException(e);
                return null;
            }
        }

        public static void AddObjectToAssetWithSave(UnityEngine.Object childAsset, string parentAssetPath)
        {
            AssetDatabase.AddObjectToAsset(childAsset, parentAssetPath);
            SaveAndRefresh();
        }

        public static void AddObjectToAssetWithSave(UnityEngine.Object childAsset, UnityEngine.Object parentAsset)
        {
            AssetDatabase.AddObjectToAsset(childAsset, parentAsset);
            SaveAndRefresh();
        }

        public static void SaveAndRefresh()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void SetDirtyAndSaveAsset(UnityEngine.Object assetObject)
        {
            EditorUtility.SetDirty(assetObject);
            AssetDatabase.SaveAssetIfDirty(assetObject);
            SaveAndRefresh();
        }
    }
}