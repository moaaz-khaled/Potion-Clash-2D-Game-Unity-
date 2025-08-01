using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    public static int Score;
    public static bool GetKey;

    public AudioSource moneyCollectSFX , PowerCollectSFX , KeyCollectSFX;

    private void OnTriggerEnter2D(Collider2D cold)
    {
        if(cold.gameObject.CompareTag("Cash"))
        {
            Destroy(cold.gameObject);
            if(PowerEffectManager.hasGreenPower) {
                PowerEffectManager.ReminderCashToRemoveGreenPower--;
            }
            if(PowerEffectManager.hasBlackPower) {
                PowerEffectManager.ReminderCashToRemoveBlackPower--;
            }
            if(PowerEffectManager.hasOrangePower) {
                PowerEffectManager.ReminderCashToRemovOrangePower--;
            }
            moneyCollectSFX.enabled = false;
            moneyCollectSFX.enabled = true;
            Score++;
            Debug.Log("Current Score is : " + Score);
        }

        if(cold.gameObject.CompareTag("Green Magic"))
        {
            Destroy(cold.gameObject);
            PowerEffectManager.hasGreenPower = true;
            PowerCollectSFX.enabled = false;
            PowerCollectSFX.enabled = true;
        }

        if(cold.gameObject.CompareTag("BlackPower"))
        {
            Destroy(cold.gameObject);
            PowerEffectManager.hasBlackPower = true;
            PowerCollectSFX.enabled = false;
            PowerCollectSFX.enabled = true;
        }

        if(cold.gameObject.CompareTag("RedPower"))
        {
            Destroy(cold.gameObject);
            PowerEffectManager.hasRedPower = true;
            PowerCollectSFX.enabled = false;
            PowerCollectSFX.enabled = true;
        }

        if(cold.gameObject.CompareTag("Key"))
        {
            Destroy(cold.gameObject);
            GetKey = true;
            KeyCollectSFX.enabled = true;
        }

        if(cold.gameObject.CompareTag("YellowPower"))
        {
            Destroy(cold.gameObject);
            PowerEffectManager.hasYellowPower = true;
            PowerCollectSFX.enabled = false;
            PowerCollectSFX.enabled = true;
        }

        if(cold.gameObject.CompareTag("OrangePower"))
        {
            Destroy(cold.gameObject);
            PowerEffectManager.hasOrangePower = true;
            PowerCollectSFX.enabled = false;
            PowerCollectSFX.enabled = true;
        }
    }

    void Start() {
        Score = 0;
        GetKey = false;
        moneyCollectSFX.enabled = false;
        PowerCollectSFX.enabled = false;
        KeyCollectSFX.enabled = false;
    }
}
