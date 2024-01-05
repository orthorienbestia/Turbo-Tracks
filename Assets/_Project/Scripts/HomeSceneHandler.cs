using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class HomeSceneHandler : MonoBehaviour
    {
        [SerializeField] private GameObject carModifyPanel;
        [SerializeField] private Button carModifyButton;

        private void Start()
        {
            carModifyPanel.SetActive(false);
            carModifyButton.onClick.AddListener(() => carModifyPanel.SetActive(true));
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene((int)Scenes.GameScene);
        }
    }
}

internal enum Scenes
{
    HomeScene = 0,
    GameScene = 1
}