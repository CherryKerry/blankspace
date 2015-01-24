using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxController2 : MonoBehaviour
{

    Transform backgroundTransform;
    Camera mainCamera;
    Vector3 cameraPreviousPosition;
    public float cameraFollowPercentage = 100.0f;
    Vector3 cameraVelocity;
    public float offset;

    public bool isLooping = false;
    //List of object
    private List<Transform> loopingObjects;

    // Use this for initialization
    void Start()
    {
        backgroundTransform = GetComponent<Transform>();
        mainCamera = Camera.main;
        cameraPreviousPosition = mainCamera.transform.position;

        if (isLooping)
        {
            loopingObjects = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.renderer != null)
                {
                    loopingObjects.Add(child);
                }
            }
            // Sort by position.
            // Note: Get the children from left to right.
            loopingObjects = loopingObjects.OrderBy(
              t => t.position.x
            ).ToList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraVelocity = GetCameraVelocity();
        Vector3 backgroundPosition = backgroundTransform.position;
        backgroundPosition.x += cameraVelocity.x * (cameraFollowPercentage / 100.0f);
        backgroundPosition.y += cameraVelocity.y * (cameraFollowPercentage / 100.0f);
        backgroundTransform.position = backgroundPosition;

        if (isLooping)
        {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            Transform firstChild = loopingObjects.FirstOrDefault();
            

            // If the child is already (partly) before the camera
            // If the child is already on the left of the camera
            if (firstChild != null  && firstChild.position.x < Camera.main.transform.position.x && firstChild.renderer.IsVisibleFrom(Camera.main) == false)
            {
                // Get the last child position.
                Transform lastChild = loopingObjects.LastOrDefault();
                Vector3 lastPosition = lastChild.transform.position;
                Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

                // Set the position of the recyled one to be AFTER the last child.
                // Note: Only work for horizontal scrolling currently.
                firstChild.position = new Vector3(lastPosition.x + lastSize.x + offset, firstChild.position.y, firstChild.position.z);

                // Move the recycled child to the last position of the list.
                loopingObjects.Remove(firstChild);
                loopingObjects.Add(firstChild);
            }
        }
    }

    Vector3 GetCameraVelocity()
    {
        Vector3 cameraCurrentPosition = mainCamera.transform.position;
        cameraVelocity = cameraCurrentPosition - cameraPreviousPosition;
        cameraPreviousPosition = cameraCurrentPosition;
        return cameraVelocity;
    }
}
