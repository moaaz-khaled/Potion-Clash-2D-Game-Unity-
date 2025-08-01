using UnityEngine;

public class door : MonoBehaviour
{

    private Animator animator;
    private PlayerController player;

    public static bool ShowVictoryPanel;

    [SerializeField] private bool OpenDoor;
    private float CenterPositionX;
    private float CenterPositionY;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open" , OpenDoor);
        player = FindFirstObjectByType<PlayerController>();
        CenterPositionX = gameObject.transform.position.x;
        CenterPositionY = gameObject.transform.position.y;
        ShowVictoryPanel = false;
        OpenDoor = false;
    }

    void Update()
    {
        if(CollectObjects.GetKey && player.GetCurrentPositionX() >= (CenterPositionX - 0.25f) &&  player.GetCurrentPositionX() <= (CenterPositionX + 0.25f )
                    && player.GetCurrentPositionY() >= (CenterPositionY - 0.5f) && player.GetCurrentPositionY() <= (CenterPositionY + 0.25)) {
            OpenDoor = true;
            animator.SetBool("Open" , OpenDoor);
            Invoke("VictoryPanel" , 0.5f);
        }
    }

    void VictoryPanel(){
        ShowVictoryPanel = true;
    }
}
