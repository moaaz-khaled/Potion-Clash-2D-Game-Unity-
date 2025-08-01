using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject PasueMenuUI;

    private ScoreController SC;
    private bool isPause = false;

    void Start() {
        PasueMenuUI.SetActive(false);
        SC = FindFirstObjectByType<ScoreController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if(isPause)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        PasueMenuUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        PasueMenuUI.SetActive(true);
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        SC.Score2.text = "0";
    }

    public void Exit() {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}