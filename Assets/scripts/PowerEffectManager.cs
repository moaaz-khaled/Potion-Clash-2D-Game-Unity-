using UnityEngine;

public class PowerEffectManager : MonoBehaviour
{
    public static bool hasGreenPower;
    public static bool hasBlackPower;
    public static bool hasRedPower;
    public static bool hasYellowPower;
    public static bool hasOrangePower;

    public static int ReminderCashToRemovOrangePower = 5;
    public static int ReminderCashToRemoveBlackPower = 5;
    public static int ReminderCashToRemoveGreenPower = 3;

    private PlayerController playerController;
    private HealthBarController healthBarController;

    void Start()
    {
        hasGreenPower = false;
        ReminderCashToRemoveGreenPower = 3;

        hasBlackPower = false;
        ReminderCashToRemoveBlackPower = 5;

        hasRedPower = false;
        hasYellowPower = false;

        hasOrangePower = false;
        ReminderCashToRemovOrangePower = 5;

        playerController = FindFirstObjectByType<PlayerController>();
        healthBarController = FindFirstObjectByType<HealthBarController>();
    }

    void Update()
    {
        if(hasGreenPower) {
            playerController.Direction = -1;
        }
        
        if(ReminderCashToRemoveGreenPower == 0 && hasGreenPower) {
            hasGreenPower = false;
            playerController.Direction = 1;
            ReminderCashToRemoveGreenPower = 3;
        }

        if(hasBlackPower) {
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if(ReminderCashToRemoveBlackPower == 0 && hasBlackPower == true) {
            hasBlackPower = false;
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            ReminderCashToRemoveBlackPower = 5;
        }

        if(hasRedPower) {
            playerController.Speed += 2f;
            hasRedPower = false;
        }

        if(hasYellowPower) {
            playerController.Health = Mathf.Min(100 , playerController.Health + 30);
            healthBarController.SetHealth(playerController.Health , 100);
            hasYellowPower = false;
        }

        if(hasOrangePower) {
            playerController.jump+=3.5f;
            hasOrangePower = false;
        }
    }
}