using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Transform startMarker;
    public Transform playerPosition;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    public float smooth = 5.0F;
    public float verticleOffset = 10.0f;
    public float horizontalOffset;
    void Start()
    {
        startMarker = Camera.main.transform;
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, playerPosition.position);
    }
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        Vector3 position = transform.position;
        Vector3 offsetVector = new Vector3(horizontalOffset, verticleOffset, 0);
        position.x = Vector2.Lerp(startMarker.position, (playerPosition.position + offsetVector), fracJourney).x;
        position.y = Vector2.Lerp(startMarker.position, (playerPosition.position + offsetVector), fracJourney).y;
        transform.position = position;
    }
}