using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

namespace Scenes.Sheep
{

    public class AnimalAnimationEventHandler
        : StateMachineBehaviour
    {
        public event System.Action<float> OnUpdate;
        public event System.Action OnComplete;

        public override void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateEnter( animator, stateInfo, layerIndex );
            animator.SetTime( 0 );
            Fire( 0 );
        }

        public override void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateUpdate( animator, stateInfo, layerIndex );
            Fire( (float) (animator.GetTime() / stateInfo.length) );
        }

        public override void OnStateExit( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateExit( animator, stateInfo, layerIndex );
            Fire( 1 );

            if ( OnComplete != null ) {
                OnComplete();
            }
        }

        private void Fire( float t )
        {
            if ( OnUpdate != null ) {
                OnUpdate( t );
            }
        }
    }
}