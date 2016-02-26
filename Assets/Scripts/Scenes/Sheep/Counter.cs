using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Sheep
{

    public class Counter
        : MonoBehaviour
    {

        private long count_;

        [SerializeField]
        private Text text_;

        [SerializeField]
        private Languages.LanguageSets language_;

        private Sheep.AnimalType animalType_;

        void Start()
        {
            count_ = 0;
            language_.OnLanguageChange += ( l ) => { Count = Count; };
        }

        void Update()
        {
        }

        public void Reset()
        {
            Count = 0;
        }

        public void Increment()
        {
            Count += 1;
        }

        public Sheep.AnimalType AnimalType
        {
            set { animalType_ = value; Count = Count; }
        }

        public long Count
        {
            get
            {
                return count_;
            }

            set
            {
                count_ = value;
                string text = animalType_ == Sheep.AnimalType.kSheep
                            ? language_.FindAtTag( "hiki_s" )
                            : language_.FindAtTag( "hiki_r" );
                text_.text = string.Format( text, count_ );
            }
        }
    }

}