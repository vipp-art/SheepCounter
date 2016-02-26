using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Scenes.Sheep.Animals
{
    public class Animal
        : MonoBehaviour
    {
        private RectTransform transform_;

        private bool isRight_ = false;
        private Vector2 target_;
        private Vector2 start_;
        private bool isEscape_ = false;

        public void Appear( float top, bool isRight )
        {
            transform_ = GetComponent<RectTransform>();
            Setup( isRight ? -50 : 690, top, isRight );
        }

        public void Setup( float left, float top, bool isRight )
        {
            isRight_ = isRight;
            transform_.localPosition = new Vector3( left, top, 0 );
            transform_.localScale = new Vector3( isRight ? -1 : 1, 1, 1 );
            target_ = transform_.localPosition + new Vector3( isRight ? 100 : -100, Random.Range( -20, 20 ), 0 );
            start_ = transform_.localPosition;

            if ( target_.y < 40 ) {
                target_.y = transform_.localPosition.y + 20;
            } else if ( target_.y > 640 - 80 ) {
                target_.y = transform_.localPosition.y - 20;
            }
        }

        void Start()
        {
            isEscape_ = false;
            var a = transform.GetChild( 0 ).GetComponent<Animator>();
            var handler = a.GetBehaviour<AnimalAnimationEventHandler>();
            handler.OnUpdate += OnAnimationUpdate;
            handler.OnComplete += OnAnimationComplete;
        }

        void Update()
        {
        }

        private void OnAnimationUpdate( float t )
        {
            if ( isEscape_ ) {
                return;
            }
            transform_.localPosition = Vector2.Lerp( start_, target_, t );
        }

        private void OnAnimationComplete()
        {
            if ( isEscape_ ) {
                return;
            }
            var position = transform_.localPosition;
            bool isRight;
            if ( position.x < 100 ) {
                isRight = true;
            } else if ( position.x > (640 - 100) ) {
                isRight = false;
            } else {
                isRight = Random.Range( -50, 50 ) < 0;
            }
            Setup( position.x, position.y, isRight );
        }

        public void Escape()
        {
            isEscape_ = true;
            StartCoroutine( AnimateEscape() );
        }

        private IEnumerator AnimateEscape()
        {
            yield return new WaitForSeconds( 0.5f );
            var a = transform.GetChild( 0 ).GetComponent<Animator>();
            a.SetTrigger( "Escape" );
            float x = transform_.localPosition.x;
            float y = transform_.localPosition.y;
            while ( x > -50 && x < 690 ) {
                yield return new WaitForEndOfFrame();
                x += Time.deltaTime * 800 * (isRight_ ? 1 : -1);
                transform_.localPosition = new Vector3( x, y, 0 );
            }

            isEscape_ = false;
            Destroy( gameObject );
        }

        public void Die()
        {
            StartCoroutine( AnimateDie() );
        }

        private IEnumerator AnimateDie()
        {
            var image = transform.GetChild( 0 ).GetComponent<Image>();
            image.CrossFadeAlpha( 0, 2, false );
            yield return new WaitForSeconds( 2 );
            Destroy( gameObject );
        }
    }
}