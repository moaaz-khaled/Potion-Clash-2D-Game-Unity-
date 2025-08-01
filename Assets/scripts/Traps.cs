using UnityEngine;

public class Traps : MonoBehaviour
{
    private PlayerController player;

    void Start() {
        player = FindFirstObjectByType<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("fire")) {
            player.TakeHit();
        }

        else if(collision.gameObject.CompareTag("Thorns")) {
            player.TakeHit();
        }
    }
}
