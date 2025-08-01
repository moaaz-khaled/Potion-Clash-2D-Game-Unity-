using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour
{
    [SerializeField] GameObject StartPannel;
    [SerializeField] AudioSource sound;


    public void ShowPannel() {
        StartPannel.SetActive(true);
        sound.enabled = true;
    }

    public void StartGame() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings){
            StartPannel.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}