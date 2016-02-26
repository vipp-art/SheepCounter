using UnityEngine;
using UnityEngine.UI;

namespace Languages
{

    [ExecuteInEditMode]
    [RequireComponent( typeof( Text ) )]
    public class TextLanguage : MonoBehaviour
    {
        [SerializeField]
        private string tag_;

        [SerializeField]
        private LanguageSets settings_;

        private Text text_;

        private LanguageSets.Language oldLanguage_ = LanguageSets.Language.kNone;

        public string Tag
        {
            get { return tag_; }
            set {
                tag_ = value;
                OnLanguageChange( settings_ );
            }
        }

        void Start()
        {
#if UNITY_EDITOR
            if ( !settings_ ) {
                var s = UnityEditor.AssetDatabase.FindAssets( "t:LanguageSets", new string[] { "Assets/Datas" } );
                if ( s != null && s.Length > 0 ) {
                    settings_ = UnityEditor.AssetDatabase.LoadAssetAtPath<LanguageSets>( UnityEditor.AssetDatabase.GUIDToAssetPath( s[0] ) );
                }
            }
#endif
            if (settings_) {
                settings_.OnLanguageChange += OnLanguageChange;
            }
            OnLanguageChange( settings_ );
        }

        void Update()
        {
#if UNITY_EDITOR
            if ( oldLanguage_ != settings_.SelectLanguage ) {
                OnLanguageChange( settings_ );
            }
#endif
        }

        private void OnLanguageChange( LanguageSets set )
        {
            if ( set ) {
                if (text_ == null ) {
                    text_ = GetComponent<Text>();
                }
                text_ = GetComponent<Text>();
                var t = set.FindAtTag( tag_ );
                if ( !string.IsNullOrEmpty( t ) ) {
                    oldLanguage_ = set.SelectLanguage;
                    text_.text = t;
                }
            }
        }
    }

}