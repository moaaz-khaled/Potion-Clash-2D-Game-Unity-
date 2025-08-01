using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;

    public static Vector2 minLimits;
    public static Vector2 maxLimits;

    void Start()
    {
        if (player == null) 
            return;
        SpriteRenderer background = GameObject.Find("Background_0").GetComponent<SpriteRenderer>();
        if (background != null)
        {
            Bounds bgBounds = background.bounds;
            float verticalExtent = Camera.main.orthographicSize;
            float horizontalExtent = verticalExtent * Screen.width / Screen.height;
            minLimits = new Vector2(bgBounds.min.x + horizontalExtent , bgBounds.min.y + verticalExtent);
            maxLimits = new Vector2(bgBounds.max.x - horizontalExtent , bgBounds.max.y - verticalExtent);
        }
    }
    void LateUpdate()
    {
        if (player == null)
            return;
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimits.x, maxLimits.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minLimits.y, maxLimits.y);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}