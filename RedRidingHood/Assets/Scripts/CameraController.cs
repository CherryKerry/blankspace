using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform characterController;
	public float cameraSmoothnessCoefficient;

	Camera mainCamera;
	float characterPositionXPrevious;
	Vector3 distanceThatCameraNeedsToMove;
	
	void Start () {
		mainCamera = Camera.main;
		characterPositionXPrevious = characterController.position.x;
		distanceThatCameraNeedsToMove = new Vector3(Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, 0)).x - Camera.main.ViewportToWorldPoint (new Vector3 (0.2f, 0, 0)).x, 0, 0);
	}


	void FixedUpdate () {
		Transform mainCameraTransform = mainCamera.transform;
		Vector3 characterPosition = characterController.position;

		characterPositionXPrevious = characterPosition.x;

		Vector3 mainCamPosition = mainCameraTransform.position;
		Vector3 smoothMovementTowardsCharacter;

        smoothMovementTowardsCharacter = Vector3.Lerp(mainCamPosition, characterPosition + distanceThatCameraNeedsToMove, cameraSmoothnessCoefficient * Time.deltaTime);
        mainCamPosition.x = smoothMovementTowardsCharacter.x;
		mainCameraTransform.position = mainCamPosition;
	}
}
