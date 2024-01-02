using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class HomeSceneHandler : MonoBehaviour
    {
        public void LoadGameScene()
        {
            SceneManager.LoadScene((int) Scenes.GameScene);
        }
    }
}

internal enum Scenes
{
    HomeScene = 0,
    GameScene = 1
}
