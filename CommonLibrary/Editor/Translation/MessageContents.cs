using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChickStar.CommonLibrary.Editor.Translation
{
    public class MessageContents : ScriptableObject
    {
        [Serializable]
        public class Content
        {
            public string key;
            public string body;
        }

        public Messages.Language language;
        public List<Content> contents = new();

        public bool HasDuplicatedKey(out List<string> duplicates)
        {
            duplicates = contents
                .GroupBy(x => x.key)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList();

            return duplicates.Count > 0;
        }

        public bool HasDuplicatedBody(out List<string> duplicates)
        {
            duplicates = contents
                .GroupBy(x => x.body)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList();

            return duplicates.Count > 0;
        }
    }
}