using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Languages
{
    public class LanguageSets
        : ScriptableObject
    {
        public enum Language {
            kNone,
            kJapanese,
            kEnglish
        }

        [SerializeField]
        private List<LanguageSet> sets_;

        [SerializeField]
        private Language language_ = Language.kJapanese;

        public event System.Action<LanguageSets> OnLanguageChange;

        public string FindAtTag( string tag )
        {
            var set = (from x in sets_ where x.Tag == tag select x).DefaultIfEmpty().Single();
            if ( set == null ) {
                return "";
            }
            return language_ == Language.kJapanese
                    ? set.Japan
                    : set.English;
        }

        public Language SelectLanguage
        {
            get { return language_; }
            set
            {
                language_ = value;
                if ( OnLanguageChange != null ) {
                    OnLanguageChange( this );
                }
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Language/言語設定生成")]
        public static void Create()
        {
            var path = UnityEditor.EditorUtility.SaveFilePanel( "保存", "", "languages", "asset" );

            if ( !string.IsNullOrEmpty( path ) ) {
                var l = ScriptableObject.CreateInstance<LanguageSets>();
                path = path.Replace( Application.dataPath, "Assets" );
                UnityEditor.AssetDatabase.CreateAsset( l, path );
            }
        }
#endif
    }

}