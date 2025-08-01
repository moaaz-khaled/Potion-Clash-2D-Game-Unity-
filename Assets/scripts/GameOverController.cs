using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    private MenuController Mn;
    [SerializeField] GameObject GameOverPanel;
    
    private ScoreController SC;
    public static bool Show;

    void Start() {
        Mn = FindAnyObjectByType<MenuController>();
        Show = false;
    }   

    void Update()
    {
        if(Show){
            Invoke("ShowPannel" , 1f);
        }
    }

    public void ShowPannel() {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }


    public void RestartGame() {
        Mn.restartGame();
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        Show = false;
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}