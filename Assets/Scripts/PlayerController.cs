using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Transform playerTransform;

	//TODO:  Everything's base velocity should be based on the player's velocity
	//That way if the player gets a "speed increase" or w/e, everything else will speed up as well

	private float cameraChangeTime;
	//Will just have the transforms here, not the cameras.
	public Camera playerCamera;
	private Transform playerCamTransform;

	private Transform currentCamTransform;

	public Transform topCam;
	public Transform sideCam;
	public Transform frontCam;

	/******/
	public float playerVelocity; // Used to determine the relative speed of everything else in the scene
	public float playerVelocityMultiplier; // Maybe
	/******/

	public bool isCameraMoving = false;

	//public float cameraChangeTime;
	private float journeyLength;


	private bool inputEnabled = true;
	// Use this for initialization
	void Start () {
		playerTransform = gameObject.transform;
		playerCamTransform = playerCamera.transform;
		currentCamTransform = topCam;

		//Based on player slowdown cam change level
		cameraChangeTime = 1.0f;
	}
	
	// Update is called once per frame

	//Lol just change all this shit to lerps, maybe?
	void Update () {
		if (inputEnabled) {
			if (Input.GetKeyDown (KeyCode.A)) {
				StartCoroutine(moveDirection(0));
			} else if (Input.GetKeyDown (KeyCode.D)) {
				StartCoroutine(moveDirection(1));
			}
		}



		if (Input.GetKeyDown (KeyCode.T) && currentCamTransform != topCam && !isCameraMoving) {
			isCameraMoving = true;
			StartCoroutine(changeCamera(topCam, cameraChangeTime));
		} else if (Input.GetKeyDown (KeyCode.S) && currentCamTransform != sideCam && !isCameraMoving) {
			isCameraMoving = true;
			StartCoroutine(changeCamera(sideCam, cameraChangeTime));
		} else if (Input.GetKeyDown (KeyCode.F) && currentCamTransform != frontCam && !isCameraMoving) {
			isCameraMoving = true;
			StartCoroutine(changeCamera(frontCam, cameraChangeTime));
		}
			
	}


	public IEnumerator moveDirection(int dir) {
		inputEnabled = false;
		if (dir == 0) { // Move Left/Up (negative X)
			Vector3 newPos = playerTransform.position;
			for (int i = 0; i < 10; i++) {
				newPos.x -= 0.3f;
				playerTransform.position = newPos;
				yield return null;
			}
		} else if (dir == 1) { // Move right/down (positive X)
			Vector3 newPos = playerTransform.position;
			for (int i = 0; i < 10; i++) {
				newPos.x += 0.3f;
				playerTransform.position = newPos;
				yield return null;
			}
		}
		inputEnabled = true;
	}


	//Function to move camera should have inputs based on the player's camera slowdown level
	private IEnumerator changeCamera(Transform targetCamTransform, float changeTime) {
		Time.timeScale = 0.33f;

		float timeLeft = 0;
		//while (playerCamTransform.position != targetCamTransform.position) {
		while (timeLeft < changeTime) {

			timeLeft += Time.deltaTime;
			playerCamTransform.position = Vector3.Lerp (currentCamTransform.position, targetCamTransform.position, (timeLeft / changeTime));
			playerCamTransform.rotation = Quaternion.Lerp (currentCamTransform.rotation, targetCamTransform.rotation, (timeLeft / changeTime));
			yield return null;
		}

		Time.timeScale = 1.0f;
		currentCamTransform = targetCamTransform;
		isCameraMoving = false;
		Debug.Log ("exiting coroutine:" + targetCamTransform.position);
	}

}
