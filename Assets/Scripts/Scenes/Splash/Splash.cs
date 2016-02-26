using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Splash
{

    public class Splash
        : MonoBehaviour
    {
        [SerializeField]
        private string targetScene_ = "Sheep";

        void Start()
        {
            Application.targetFrameRate = 30;
            SceneManager.LoadScene( targetScene_, LoadSceneMode.Single );
        }
   }
}