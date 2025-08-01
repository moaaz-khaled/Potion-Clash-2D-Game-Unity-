using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{

    private MenuController Mn;
    [SerializeField] GameObject VectoryPannel;
    public AudioSource sound;
    private GameController gameController;

    private ScoreController SC;
    public static bool Show = false;

    void Start() {
        gameController = FindAnyObjectByType<GameController>();
        Mn = FindAnyObjectByType<MenuController>();
        sound.enabled = false;
    }   

    void Update()
    {
        if(door.ShowVictoryPanel){
            gameController.backgroundMusic.enabled = false;
            ShowPannel();
            sound.enabled = true;
        }
    }

    public void ShowPannel() {
        Time.timeScale = 0;
        VectoryPannel.SetActive(true);
    }


    public void PlayAgain() {
        Mn.restartGame();
        door.ShowVictoryPanel = false;
        Time.timeScale = 1;
        VectoryPannel.SetActive(false);
    }

    public void NextGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings){
            door.ShowVictoryPanel = false;
            VectoryPannel.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        
        else {
            Debug.Log("No more levels available. Returning to main menu.");
        }
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}