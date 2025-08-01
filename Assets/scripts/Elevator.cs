using UnityEngine;

public class Elevator : MonoBehaviour
{
    private float SpeedPlatform;
    private bool MoveUp;
    private float CurrentPositionX , CurrentPositionY , MaxY , MinY;

    void Start() {
        CurrentPositionX = gameObject.transform.position.x;
        CurrentPositionY = gameObject.transform.position.y;
        MinY = CurrentPositionY;
        MaxY = CurrentPositionY + 4.6f;

        SpeedPlatform = 1.5f;
        MoveUp = true;
        
    }

    void Update()
    {
        CurrentPositionX = gameObject.transform.position.x;
        CurrentPositionY = gameObject.transform.position.y;
        if(MoveUp)
        {
            CurrentPositionY += SpeedPlatform * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.y >= MaxY) {
                MoveUp = !MoveUp;
            }
        }

        if(!MoveUp)
        {
            CurrentPositionY -= SpeedPlatform * Time.deltaTime; 
            gameObject.transform.position = new Vector3(CurrentPositionX , CurrentPositionY , 0);
            if(gameObject.transform.position.y <= MinY) {
                MoveUp = !MoveUp;
            }
        }
    }
}