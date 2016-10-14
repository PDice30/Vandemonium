using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {

	private Transform playerTransform;

	//This should probably be a list? OrderedList, arraylist, array?
	public List<PlayerBuddy> playerBuddies;
	//public PlayerBuddy[] playerBuddies;
	public GameObject playerBuddyPrefab;

	public int playerHealth;
	public int playerArmor;


	private LevelSceneController levelSceneController;

	//These might best be handled completely by the buddies, no need for two of the same vars
	private float cameraChangeTime;
	private float laneChangeTime;

	//Cameras and transforms
	private Camera playerCamera;
	private Transform playerCamTransform;
	private Transform currentCamTransform;
	private Transform introCam;
	private Transform topCam;
	private Transform sideCam;
	private Transform frontCam;

	//Player Position etc
	public int currentLaneIndex;

	//Booleans
	public bool isPlayerInvulnerable = false;
	public bool isCameraMoving = false;
	private bool isCarMovingLeft = false;
	private bool isCarMovingRight = false;

	//public float cameraChangeTime;
	//Is this used?
	private float journeyLength;

	//Player is moving - allowed to move
	private bool inputEnabled = false;

	//Touch init
	private Vector2 touchOrigin = -Vector2.one;

	void Awake() {
		playerCamera = GameObject.Find ("PlayerCamera").GetComponent<Camera> ();
		introCam = GameObject.Find ("IntroCamera").GetComponent<Transform> ();
		topCam = GameObject.Find ("TopCamera").GetComponent<Transform>();
		sideCam = GameObject.Find ("SideCamera").GetComponent<Transform>();
		frontCam = GameObject.Find ("FrontCamera").GetComponent<Transform>();
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController> ();
	}

	void Start () {

		//Based on buddies and level etc
		//Temp
		playerHealth = SceneConstants.DEFAULT_PLAYER_HEALTH;
		playerArmor = 0;

		playerBuddies = levelSceneController.playerBuddies;

		foreach (PlayerBuddy buddy in playerBuddies) {
			Debug.Log ("Buddy: " + buddy.buddySkillEnum.ToString ());
		}
		//All buddy code will be handled in the title scene, although
		// it is possible some buddy addying code will be used during the game.

		//Buddies will be added on Start from an object in the title screen that will load up the buddies
		//Add buddies from the previous scene.
		//addPlayerBuddy(BuddySkillEnum.Chronologist);

		playerTransform = gameObject.transform;
		playerCamTransform = playerCamera.transform;
		currentCamTransform = introCam;

		//Based on player slowdown cam change level
		//TODO
		//cameraChangeTime = PlayerBuddySkills.checkSkills
		//This will likely not need to be stored at all by the player themselves
		cameraChangeTime = 1.0f;
		laneChangeTime = 0.25f;

		currentLaneIndex = (levelSceneController.lanes.Count / 2);  //Int rounded to mid lane

		StartCoroutine(startIntro(topCam));
	}

	void Update () {

		//#if UNITY_EDITOR || UNITY_STANDALONE
		if (inputEnabled) {
			if (Input.GetKeyDown (KeyCode.A)) {
				StartCoroutine(attemptMoveDirection(-1, laneChangeTime));
			} else if (Input.GetKeyDown (KeyCode.D)) {
				StartCoroutine(attemptMoveDirection(1, laneChangeTime));
			}
		}

		// Speed up or down the scene
		if (Input.GetKeyDown (KeyCode.U)) {
			levelSceneController.SCENE_SPEED -= 0.25f;
		} else if (Input.GetKeyDown (KeyCode.I)) {
			levelSceneController.SCENE_SPEED += 0.25f;
		} 


		//Move Camera - Debug based on keys
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
			if ((horizontal != 0 && currentCamTransform == topCam)
			    || (horizontal != 0 && currentCamTransform == frontCam)) {
				StartCoroutine (attemptMoveDirection (horizontal, laneChangeTime));
			} else if (vertical != 0 && currentCamTransform == sideCam) {
				StartCoroutine (attemptMoveDirection (vertical, laneChangeTime));

				//Determine if its a temp camera change
			} else if (currentCamTransform == topCam && vertical == 1) {
				isCameraMoving = true;
				StartCoroutine(changeCamera(frontCam, cameraChangeTime));
			} else if (currentCamTransform == frontCam && vertical == 1) {
				isCameraMoving = true;
				StartCoroutine(changeCamera(topCam, cameraChangeTime));
			}
		}
		//#endif
			
	}

	// Timescale/Delta.Time should affect this, but check for buddy Skills
	// Also check current Lane and if it can move to the next lane
	public IEnumerator attemptMoveDirection(int direction, float laneChangeTime) {
		//TODO
		//Still need to check lane positions
		//Need to account for player skills!

		//BuddyCheck for Chronologist - If level 5: no slowdown of player
		//BuddyCheck for Sidewinder - Adjust move speed of the player's lane change

		float timeLeft = 0;
		inputEnabled = false;
		if (direction == -1 && !levelSceneController.lanes[currentLaneIndex].isLeftmostLane) { //Move left/Up, check for LANE!
			isCarMovingLeft = true;
			currentLaneIndex -= 1;
			float newXPos = levelSceneController.lanes[currentLaneIndex].transform.position.x;
			Vector3 originalPlayerPosition = playerTransform.position;
			Vector3 newPlayerPosition = new Vector3 (newXPos, playerTransform.position.y, playerTransform.position.z);
			while (timeLeft < laneChangeTime) {
				timeLeft += Time.deltaTime;
				playerTransform.position = Vector3.Lerp(originalPlayerPosition, newPlayerPosition, (timeLeft / laneChangeTime));
				yield return null;
			}



		} else if (direction == 1 && !levelSceneController.lanes[currentLaneIndex].isRightmostLane) { //Move right/down, check for LANE!
			isCarMovingRight = true;
			currentLaneIndex += 1;
			float newXPos = levelSceneController.lanes[currentLaneIndex].transform.position.x;
			Vector3 originalPlayerPosition = playerTransform.position;
			Vector3 newPlayerPosition = new Vector3 (newXPos, playerTransform.position.y, playerTransform.position.z);
			while (timeLeft < laneChangeTime) {
				timeLeft += Time.deltaTime;
				playerTransform.position = Vector3.Lerp(originalPlayerPosition, newPlayerPosition, (timeLeft / laneChangeTime));
				yield return null;
			}


		}

		isCarMovingLeft = false;
		isCarMovingRight = false;
		inputEnabled = true;
	}


	//Function to move camera should have inputs based on the player's camera slowdown level
	private IEnumerator changeCamera(Transform targetCamTransform, float changeTime) {

		/**********
		*BuddyCheck here for Chronologist slowdown and Radiologist XRay
		/*********/
		//GameObject.Find("CarSpawner").GetComponent<CarSpawner>().enemyCarPrefab.GetComponent<MeshRenderer>().material = Resources.Load ("Enemy_Car_XRay") as Material;
		//Cant change the prefab, instead change the material when it is instantiated
		foreach (PlayerBuddy buddy in playerBuddies) {
			//Null check probably not necessary
			if (buddy != null) {
				if (buddy.buddyCheck(BuddySkillEnum.Chronologist)) { // Returns true if buddy has that skill
					Time.timeScale = (Time.timeScale * buddy.chronologist_cameraSlowdownPercentage);
				}
				if (buddy.buddyCheck (BuddySkillEnum.Radiologist)) {
					GameObject[] enemyCarArray = GameObject.FindGameObjectsWithTag ("EnemyCar");
					for (int i = 0; i < enemyCarArray.Length; i++) {
						enemyCarArray [i].GetComponent<MeshRenderer> ().material = Resources.Load ("Enemy_Car_XRay") as Material;
					}
				}	
			}
		}

		//Maybe set timeLeft higher and then subtract delta time? Makes more sense that way.
		float timeLeft = 0;
		while (timeLeft < changeTime) {
			timeLeft += Time.deltaTime;
			playerCamTransform.position = Vector3.Lerp (currentCamTransform.position, targetCamTransform.position, (timeLeft / changeTime));
			//Quaternion.Slerp here maybe?
			playerCamTransform.rotation = Quaternion.Lerp (currentCamTransform.rotation, targetCamTransform.rotation, (timeLeft / changeTime));
			yield return null;
		}

		//Reset timescale if it was affected by a Buddy skill
		Time.timeScale = 1.0f;
		currentCamTransform = targetCamTransform;
		isCameraMoving = false;
	}

	private IEnumerator startIntro(Transform targetCamTransform) {
		float timeLeft = 0;
		float changeTime = 2;
		while (timeLeft < changeTime) {
			timeLeft += Time.deltaTime;
			playerCamTransform.position = Vector3.Lerp (currentCamTransform.position, targetCamTransform.position, (timeLeft / changeTime));
			playerCamTransform.rotation = Quaternion.Lerp (currentCamTransform.rotation, targetCamTransform.rotation, (timeLeft / changeTime));
			yield return null;
		}
		currentCamTransform = targetCamTransform;

		inputEnabled = true;
	}


	void OnCollisionEnter(Collision coll) {
		/*
		 * Collision with Car
		 */ 
		int remainingCarCollisions = 0;
		if (coll.gameObject.tag.Equals("EnemyCar")) {
			if (coll.gameObject.GetComponent<Rigidbody> ().isKinematic) {
				playerHealth -= 1;
				//Check for death
				if (playerHealth <= 0) {
					levelSceneController.beginGameOver ();
				}	

				foreach (PlayerBuddy buddy in levelSceneController.playerBuddies) {
					if (buddy.buddyCheck (BuddySkillEnum.Rocker)) {
						remainingCarCollisions = buddy.rocker_numberOfCarCollisions;
					}
				}

				levelSceneController.numberOfHits += 1;
				levelSceneController.numberOfHitsText.text = "Hits: " + levelSceneController.numberOfHits;
				if (isCarMovingRight) {
					coll.gameObject.GetComponent<EnemyCarMover> ().markForDestroy (0, remainingCarCollisions); // Parameter based on direction
				} else if (isCarMovingLeft) {
					coll.gameObject.GetComponent<EnemyCarMover> ().markForDestroy (1, remainingCarCollisions); // Parameter based on direction
				} else {
					coll.gameObject.GetComponent<EnemyCarMover> ().markForDestroy (2, remainingCarCollisions); // Parameter based on direction
				}

			}
		}

	}

	void OnTriggerEnter(Collider coll) {
		/*
		 * Collision with Car
		 */
		//Depending on type of coin, get value
		//Find an easier way to get component for these checks
		if (coll.gameObject.tag.Equals("Coin")) {
			CoinMover coinMover = coll.gameObject.GetComponent<CoinMover> ();
			if (coinMover.hasBeenCollected == false) {
				levelSceneController.numberOfCoins += 1;
				levelSceneController.numberOfCoinsText.text = "Coins: " + levelSceneController.numberOfCoins;
				coinMover.coinAudioSource.PlayOneShot (coinMover.coinAudioClip);
				coinMover.hasBeenCollected = true;
			}

			//Destroy (coll.gameObject);
		}
	}
		

}
