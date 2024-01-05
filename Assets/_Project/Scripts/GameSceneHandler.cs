using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class GameSceneHandler : MonoBehaviour
    {
        private void Update()
        {
            // Reload scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    
        public void BackToHomeScene()
        {
            SceneManager.LoadScene((int)Scenes.HomeScene);
        }
    }
}
