using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Director;

namespace Scenes.Sheep
{

    public class AnimalAnimationBehaivour
        : StateMachineBehaviour
    {
        public override void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateEnter( animator, stateInfo, layerIndex );
            animator.SetFloat( "Duration", Random.Range( 1, 2 ) );
        }

        public override void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
        {
            base.OnStateUpdate( animator, stateInfo, layerIndex );
            animator.SetFloat( "Duration", animator.GetFloat( "Duration" ) - Time.deltaTime );
        }
    }
}