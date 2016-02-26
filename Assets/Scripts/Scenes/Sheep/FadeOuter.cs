using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Scenes.Sheep
{

    public class FadeOuter : MonoBehaviour
    {

        void Start()
        {
            StartCoroutine( Fade() );
        }

        IEnumerator Fade()
        {
            yield return new WaitForSeconds( 10 );
            var text = GetComponent<Text>();
            text.CrossFadeAlpha( 0, 1, false );
            yield return new WaitForSeconds( 1 );
            gameObject.SetActive( false );
        }
    }

}