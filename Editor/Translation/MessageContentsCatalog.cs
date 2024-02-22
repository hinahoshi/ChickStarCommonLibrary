using System;
using System.Collections.Generic;
using ChickStar.CommonLibrary.Runtime.Utils;
using ChickStar.CommonLibrary.UnityRuntime.Utils;
using UnityEditor;

namespace ChickStar.CommonLibrary.Editor.Translation
{
    public class MessageContentsCatalog : SingletonScriptableObject<MessageContentsCatalog>
    {
        public MessageContents[] contentsArray = { };

        private void ResetContentsSize()
        {
            var newArray = new MessageContents[EnumUtil.SizeOf<Messages.Language>()];
            for (int index = 0; index < contentsArray.Length; index++)
            {
                newArray[index] = contentsArray[index];
            }

            contentsArray = newArray;
        }

        public void AddContents(Messages.Language language, MessageContents contents)
        {
            ResetContentsSize();
            var index = (int)language;
            contentsArray[index] = contents;
        }
    }
}