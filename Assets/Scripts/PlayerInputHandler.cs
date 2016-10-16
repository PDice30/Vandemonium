using UnityEngine;
using System.Collections;

/********
 * PlayerInputHandler
 * - Handles ALL player input in the Level Scene.  Passes the input to the appropriate controller
 ********/ 
public class PlayerInputHandler : MonoBehaviour {

	//Direct references to other components in the scene
	private LevelSceneController levelSC;
	public PlayerController playerController;

	//Is the camera already moving, or is player input disabled while moving etc
	public bool isCameraMoving = true;
	public bool inputEnabled = false;

	//For actual mobile input detection
	private Vector2 touchOrigin = -Vector2.one;

	void Awake() {
		levelSC = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//#if UNITY_EDITOR || UNITY_STANDALONE
		if (inputEnabled) {
			if (Input.GetKeyDown (KeyCode.A)) {
				StartCoroutine(playerController.attemptMoveDirection(-1, playerController.laneChangeTime));
			} else if (Input.GetKeyDown (KeyCode.D)) {
				StartCoroutine(playerController.attemptMoveDirection(1, playerController.laneChangeTime));
			}
		}

		// Speed up or down the scene
		if (Input.GetKeyDown (KeyCode.U)) {
			levelSC.SCENE_SPEED -= 0.25f;
		} else if (Input.GetKeyDown (KeyCode.I)) {
			levelSC.SCENE_SPEED += 0.25f;
		} 


		//Move Camera - Debug based on keys
		if (!isCameraMoving) {
			if (Input.GetKeyDown (KeyCode.T) && levelSC.currentCamTransform != levelSC.topCam) {
				isCameraMoving = true;
				StartCoroutine(levelSC.changeCamera(levelSC.topCam, levelSC.cameraChangeTime));
			} else if (Input.GetKeyDown (KeyCode.S) && levelSC.currentCamTransform != levelSC.sideCam) {
				isCameraMoving = true;
				StartCoroutine(levelSC.changeCamera(levelSC.sideCam, levelSC.cameraChangeTime));
			} else if (Input.GetKeyDown (KeyCode.F) && levelSC.currentCamTransform != levelSC.frontCam) {
				isCameraMoving = true;
				StartCoroutine(levelSC.changeCamera(levelSC.frontCam, levelSC.cameraChangeTime));
			}
		}



		///////////// Touch Events ///////////// - Uncomment the #s
		//#else
		int horizontal = 0;
		int vertical = 0;
		//Consider TouchScript
		//Reads the player touches and responds with what direction they have swiped.
		if (inputEnabled) {
			if (Input.touchCount > 0) {
				Touch playerTouch = Input.touches [0];
				if (playerTouch.phase == TouchPhase.Began) {
					touchOrigin = playerTouch.position;
				} else if (playerTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
					Vector2 touchEnd = playerTouch.position;
					float x = touchEnd.x - touchOrigin.x;
					float y = touchEnd.y - touchOrigin.y;
					touchOrigin.x = -1;
					if (Mathf.Abs (x) > Mathf.Abs (y)) {
						horizontal = x > 0 ? 1 : -1;
					} else {
						vertical = y > 0 ? 1 : -1;
					}
				}
			}

			// After determining swipe direction, pass the direction into moveDirection
			if ((horizontal != 0 && levelSC.currentCamTransform == levelSC.topCam)
				|| (horizontal != 0 && levelSC.currentCamTransform == levelSC.frontCam)) {
				StartCoroutine (playerController.attemptMoveDirection (horizontal, playerController.laneChangeTime));
			} else if (vertical != 0 && levelSC.currentCamTransform == levelSC.sideCam) {
				StartCoroutine (playerController.attemptMoveDirection (vertical, playerController.laneChangeTime));

				//Determine if its a temp camera change
			} else if (levelSC.currentCamTransform == levelSC.topCam && vertical == 1) {
				isCameraMoving = true;
				StartCoroutine(levelSC.changeCamera(levelSC.frontCam, levelSC.cameraChangeTime));
			} else if (levelSC.currentCamTransform == levelSC.frontCam && vertical == 1) {
				isCameraMoving = true;
				StartCoroutine(levelSC.changeCamera(levelSC.topCam, levelSC.cameraChangeTime));
			}
		}
		//#endif
	}
}
