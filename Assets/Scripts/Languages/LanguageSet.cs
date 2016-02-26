using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Languages
{
    [System.Serializable]
    public class LanguageSet
    {
        [SerializeField]
        private string tag_;

        [SerializeField]
        private string japan_;

        [SerializeField]
        private string english_;

        public string Tag { get { return tag_; }  private set { tag_ = value; } }

        public string Japan { get { return japan_; }  private set { japan_ = value; } }

        public string English { get { return english_; } private set { english_ = value; } }

        public LanguageSet( string tag = "", string japan = "", string english = "" )
        {
            Tag = tag_;
            Japan = japan;
            English = english;
        }
    }
}