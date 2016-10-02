using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleWithVanSceneController : MonoBehaviour {

	public Camera mainCamera;
	private Transform mainCameraTransform;
	public Transform frontCam;
	public Transform playSideCam;
	public Transform topCam;

	public Transform currentCamTransform;

	// Use this for initialization
	void Start () {
		mainCameraTransform = mainCamera.transform;
		currentCamTransform = frontCam;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void menuButtonClicked(Button buttonClicked) {
		if (buttonClicked.name == "FrontSideToPlaySide") {
			StartCoroutine(changeCamera(playSideCam, 1.0f));
		}
		if (buttonClicked.name == "PlaySideToFrontSide") {
			StartCoroutine(changeCamera(frontCam, 1.0f));
		}
	}
	//Function to move camera should have inputs based on the player's camera slowdown level
	private IEnumerator changeCamera(Transform targetCamTransform, float changeTime) {



		//Maybe set timeLeft higher and then subtract delta time? Makes more sense that way.
		float timeLeft = 0;
		while (timeLeft < changeTime) {

			timeLeft += Time.deltaTime;
			mainCameraTransform.position = Vector3.Lerp (currentCamTransform.position, targetCamTransform.position, (timeLeft / changeTime));
			//Quaternion.Slerp here maybe?
			mainCameraTransform.rotation = Quaternion.Lerp (currentCamTransform.rotation, targetCamTransform.rotation, (timeLeft / changeTime));
			yield return null;
		}

		//Reset timescale if it was affected by a Buddy skill
		//Time.timeScale = 1.0f;
		currentCamTransform = targetCamTransform;
		//isCameraMoving = false;
	}


}
