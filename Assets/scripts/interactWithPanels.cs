using UnityEngine;

public class interactWithPanels : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            transform.parent = collision.transform;
        }
        if (collision.gameObject.CompareTag("Elevator")) {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Elevator")) {
            transform.parent = null;
        }
    }
}