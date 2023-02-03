using System;
using UnityEngine;

namespace Localization
{
    [Serializable]
    public class LocalizedString
    {
        [SerializeField] [TextArea(3, 10)] private string _english;
        [SerializeField] [TextArea(3, 10)] private string _russian;

        public string Russian => _russian;
        public string English => _english;

        public LocalizedString(string english, string russian)
        {
            _english = english;
            _russian = russian;
        }

        public string GetTranslation(int language) => language switch
        {
            0 => English,
            1 => Russian,
            _ => ""
        };

        public override string ToString() => $"({English}), ({Russian})";
    }
}