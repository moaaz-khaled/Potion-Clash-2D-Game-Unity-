using UnityEngine;
using UnityEngine.UI;

public class PlatformSpecificUI : MonoBehaviour
{
    public GameObject MobileUI;

    private PlayerController PlayerController;
    private MenuController MC;

    public bool isMovingLeft = false;
    public bool isMovingRight = false;

    void Start()
    {
        #if UNITY_ANDROID
            MobileUI.SetActive(true);
            PlayerController = FindFirstObjectByType<PlayerController>();
        #else
            MobileUI.SetActive(false);
        #endif
        MC = FindFirstObjectByType<MenuController>();
    }

    void Update() 
    {
        if (PlayerController == null) { 
            return;
        }
        if (isMovingLeft) {
            PlayerController.fr = -1 * PlayerController.Direction;
        }
        else if (isMovingRight) {
            PlayerController.fr = 1 * PlayerController.Direction;
        }
    }

    public void MoveLeftDown() {
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void MoveRightDown() {
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void StopMove() {
        isMovingLeft = false;
        isMovingRight = false;
        PlayerController.fr = 0;
    }

    public void Attack() {
        PlayerController.Attack();
    }

    public void Jump() {
        PlayerController.Jump();
    }

    public void Pause() {
        MC.Pause();
    }
}