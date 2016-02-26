using UnityEngine;
using System.Collections.Generic;

namespace Scenes.Sheep
{
    using Sys.Audio;
    using Animals;

    public class Sheep
        : MonoBehaviour
    {
        const int kMaxCount = 30;

        public enum AnimalType
        {
            kSheep,
            kRabbit
        }

        [SerializeField]
        private AudioManager voice_;

        [SerializeField]
        private Languages.LanguageSets languages_;

        [SerializeField]
        private References references_;

        [SerializeField]
        private Counter counter_;

        private AnimalType animalType_ = AnimalType.kSheep;

        private IAdder animalAdder_;

        private bool isAutomation_;

        private List<Animal> animals_;

        public References Ref
        {
            get { return this.references_; }
        }

        void Start()
        {
            animalAdder_ = new SheepAdder( this );
            counter_.Count = 0;
            animals_ = new List<Animal>();
        }

        void Update()
        {
            if ( isAutomation_ ) {
                animalAdder_.Add();
            }
        }

        public void OnClick()
        {
            animalAdder_.Add();
        }

        public void OnConvertLanguagePushed()
        {
            if ( languages_.SelectLanguage == Languages.LanguageSets.Language.kJapanese ) {
                languages_.SelectLanguage = Languages.LanguageSets.Language.kEnglish;
            } else {
                languages_.SelectLanguage = Languages.LanguageSets.Language.kJapanese;
            }
        }

        public void OnFreePushed()
        {
            foreach ( var a in animals_ ) {
                a.Escape();
            }
            animals_.Clear();

            if ( animalType_ == AnimalType.kSheep ) {
                voice_.Play( "meee", "meee" );
            }

            counter_.Count = 0;
        }

        public void OnConvertAnimalPushed()
        {
            OnFreePushed();
            if (animalType_ == AnimalType.kSheep) {
                animalType_ = AnimalType.kRabbit;
                animalAdder_ = new RabbitAdder( this );
                Ref.AnimalConvertButtonText.Tag = "sheep";
            } else {
                animalType_ = AnimalType.kSheep;
                animalAdder_ = new SheepAdder( this );
                Ref.AnimalConvertButtonText.Tag = "rabbit";
            }
            counter_.AnimalType = animalType_;
        }

        public void OnQuietPushed()
        {
            voice_.Stop();
        }

        public void OnAutomationPushed()
        {
            isAutomation_ ^= true;
            if ( isAutomation_ ) {
                Ref.AutomationButtonText.Tag = "stop";
            }else {
                Ref.AutomationButtonText.Tag = "auto";
            }
        }

        public bool Add( GameObject go )
        {
            go.transform.SetParent( Ref.ContentArea.transform );
            var animal = go.GetComponent<Animal>();

            bool leftOrRight = Random.Range( -50, 50 ) < 0;
            float top = Random.Range( 140, 960 - 140 );

            animal.Appear( top, leftOrRight );
            counter_.Increment();
            animals_.Add( animal );
            if ( animals_.Count > kMaxCount ) {
                animals_[0].Die();
                animals_.RemoveAt( 0 );
            }
            return leftOrRight;
        }

        public void PlaySe( bool isPositionLeft )
        {
            voice_.Play( "me", "me", isPositionLeft ? -0.5f : 0.5f, Random.Range( 0.8f, 1.2f ) );
        }

        public void OnVolumeChanged( float volume )
        {
            voice_.SetVolume( volume );
        }
    }

}