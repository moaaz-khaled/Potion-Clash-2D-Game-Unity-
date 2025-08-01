using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float SpeedPlatform = 4f;
    private bool MoveRight = true;
    private float CurrentPositionX , CurrentPositionY , MaxX , MinX;

    void Start() {
        CurrentPositionX = gameObject.transform.position.x;
        CurrentPositionY = gameObject.transform.position.y;
        MaxX = CurrentPositionX + 2.5f;
        MinX = CurrentPositionX - 2.5f;
    }

    void Update()
    {
        CurrentPositionX = gameObject.transform.position.x;
        CurrentPositionY = gameObject.transform.position.y;
        if(MoveRight)
        {
            CurrentPositionX += SpeedPlatform * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.x >= MaxX) {
                MoveRight = !MoveRight;
            }
        }
        if(!MoveRight)
        {
            CurrentPositionX -= SpeedPlatform * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.x <= MinX) {
                MoveRight = !MoveRight;
            }
        }
    }
}