using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/********
 * LevelSceneController
 * - Handles everything related to the level scene itself, like the cameras, object generation, scene speed
 * - Currently handles UI components for the scene as well
 ********/ 
public class LevelSceneController : MonoBehaviour {
	//Controllers and Input Handlers
	private TitleSceneController titleSceneController;
	public PlayerInputHandler inputHandler;
	public LightController lightController;

	public List<PlayerBuddy> playerBuddies; //The list of player buddies received from the TitleSC

	public GameObject playerCarPrefab;
	private GameObject playerCarObj;
	//Lanes created based on criteria set in the generateLanes function based on this prefab
	public List<Lane> lanes;
	public GameObject lanePrefab;

	//Score Canvas Items
	public GameObject scoreCanvasObj;
	public Text numberOfCoinsText; //These are updated via the PlayerController Collisions etc.
	public Text numberOfHitsText;
	public int numberOfCoins = 0;
	public int numberOfHits = 0;

	private float timeInCurrentZone = 0;
	private float gameStartTime = 0;
	private float totalGameTime = 0;

	//Cameras and such
	public Camera playerCamera;
	public Transform playerCamTransform;
	public Transform currentCamTransform; //Which of the possible cam transforms is it actually looking at
	public Transform introCam;
	public Transform topCam;
	public Transform sideCam;
	public Transform frontCam;

	//Game Over Canvas Items
	public GameObject gameOverCanvasObj;
	public GameObject doomsayerPanel;
	public Text currentScoreText;
	public Text currentCoinsText;
	public Text currentZoneText;
	public Text bestScoreText;
	public Text bestCoinsText;
	public Text bestZoneText;
	public Text totalCoinsText;

	public float cameraChangeTime; //Will eventually just be handled by the Buddies

	//Increases and decreases based on zone, power ups, buddies, etc. Handles all objects' speeds!
	public float SCENE_SPEED; 

	//Buddy specific vars
	public float buddy_doomsayerFrequency;
	public float buddy_doomsayerTimeUntilNext;
	public GameObject doomsayerGoodBubblePrefab;
	public GameObject doomsayerBubbleButtonPrefab;
	public bool player_isUsingDoomsayer;

	//Could just be a list
	public ArrayList itemsToDestroy = new ArrayList();

	void Awake() {
		//Set other camera's and canvases not active
		gameOverCanvasObj.SetActive(false);

		//Application.targetFrameRate = 30;
		titleSceneController = GameObject.Find("TitleSceneController").GetComponent<TitleSceneController>();
		getBuddies (titleSceneController.finalChosenPlayerBuddies);
		SCENE_SPEED = SceneConstants.BASE_SCENE_SPEED;

		//When the player leaves this scene, make sure to destroy objects previously set to not do so
		foreach (PlayerBuddy buddy in playerBuddies) {
			itemsToDestroy.Add (buddy.gameObject);
		}
		itemsToDestroy.Add (titleSceneController.gameObject);

		//Based on the function, determine how many, width, etc of lanes
		lanes = generateLanes (5, -6f, 2f, 3f);

	}

	void Start () {
		//TODO Player spawned based on lanes
		//Instantiate the car and set up the components necessary for input etc.
		GameObject playerCar = Instantiate (playerCarPrefab, new Vector3 (0f, 0.5f, -10f), Quaternion.identity) as GameObject;
		lightController.PlayerSpotlight = playerCar.GetComponentInChildren<Light> ();
		playerCarObj = playerCar;
		inputHandler = playerCar.GetComponent<PlayerInputHandler> ();

		//Set the transforms for the playerCam and currentCam for use in the Change camera functions
		playerCamTransform = playerCamera.transform;
		currentCamTransform = introCam;

		cameraChangeTime = 1.0f;


		gameStartTime = Time.time; //Keeping track of the zone time etc.

		/**********
		* BuddyCheck here for DoomSayer
		/*********/
		foreach (PlayerBuddy buddy in playerBuddies) {
			if (buddy.buddyCheck(BuddySkillEnum.Doomsayer)) {
				player_isUsingDoomsayer = true;
				buddy_doomsayerFrequency = buddy.doomsayer_speechBubbleFrequency;
				buddy_doomsayerTimeUntilNext = buddy.doomsayer_speechBubbleFrequency;
			}
		}

		//Start the scene by zooming out from the car
		StartCoroutine(startIntro(topCam));
	}
	void Update () {
		if (player_isUsingDoomsayer) {
			buddy_doomsayerTimeUntilNext -= Time.deltaTime;
			if (buddy_doomsayerTimeUntilNext <= 0) {
				foreach (PlayerBuddy buddy in playerBuddies) {
					if (buddy.buddyCheck(BuddySkillEnum.Doomsayer)) {
						spawnDoomsayerBubble (buddy.doomsayer_badSpeechBubbleSize, buddy.doomsayer_goodSpeechBubbleSize);
					}
				}
				buddy_doomsayerTimeUntilNext = buddy_doomsayerFrequency;
			}
		}
			


		//Testing Zone Transitions etc.
		timeInCurrentZone += Time.deltaTime;
		//Zone time
		if (Time.time - gameStartTime >= 10) {
			Debug.Log ("" + timeInCurrentZone);
			gameStartTime = Time.time;
		}

		if (timeInCurrentZone >= 60) {
			lightController.switchLighting ();
			timeInCurrentZone = 0;
		}
	}

	public void getBuddies(List<PlayerBuddy> buddies) {
		playerBuddies = buddies;
	}

	// Destroy buddies and titleSC when going back to main menu
	public void destroyItemsOnChange() {
		foreach (GameObject item in itemsToDestroy) {
			Destroy (item);
		}
	}


	public void gameOverMenuButtonClicked(Button menuButton) {
		if (menuButton.name == "RestartButton") {
			//Probably not necessary on scene load?
			//Timescale will not actually change eventually
			Time.timeScale = 1f;
			scoreCanvasObj.SetActive (true);
			SceneManager.LoadScene ("LevelScene");
		} else if (menuButton.name == "ChooseBuddiesButton") {
			Time.timeScale = 1f;
			destroyItemsOnChange();
			SceneManager.LoadScene("TitleScene");
			//Set canvas equal to buddy Canvas
		} else if (menuButton.name == "MainMenuButton") {
			Time.timeScale = 1f;
			destroyItemsOnChange();
			SceneManager.LoadScene("TitleScene");
		}
	}


	//This should all probably be in levelSceneController...
	public void beginGameOver() {
		//Present player with menu about stats/etc
		//Save player prefs for coins, stats, score, high scores, achievements etc
		//Give options to play again, choose new buddies, or return to main menu
		//Player should explode or whatever

		//Should probably not do this timescale change when all said and done
		Time.timeScale = 0f;

		//Check to see if any bests need to be updated!
		currentScoreText.text = "Score: 100";
		currentCoinsText.text = "Coins: " + numberOfCoins;
		currentZoneText.text = "Zone: 2";
		bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt (PlayerConstants.Player_BestScore);
		bestCoinsText.text = "Best Coins: " + PlayerPrefs.GetInt (PlayerConstants.Player_BestCoins);
		bestZoneText.text = "Best Zone: " + PlayerPrefs.GetInt (PlayerConstants.Player_BestZone);
		PlayerPrefs.SetInt (PlayerConstants.Player_TotalCoins, PlayerPrefs.GetInt (PlayerConstants.Player_TotalCoins) + numberOfCoins);
		totalCoinsText.text = "Total Coins: " + PlayerPrefs.GetInt (PlayerConstants.Player_TotalCoins);

		PlayerPrefs.Save ();

		scoreCanvasObj.SetActive (false);
		gameOverCanvasObj.SetActive (true);


		//Temp return to main menu
		//levelSceneController.
		//levelSceneController.
		//destroyItemsOnChange();
		//SceneManager.LoadScene("TitleScene");
	}

	//Function to move camera should have inputs based on the player's camera slowdown level
	public IEnumerator changeCamera(Transform targetCamTransform, float changeTime) {
		/**********
		* BuddyCheck here for Chronologist slowdown and Radiologist XRay
		/*********/
		//GameObject.Find("CarSpawner").GetComponent<CarSpawner>().enemyCarPrefab.GetComponent<MeshRenderer>().material = Resources.Load ("Enemy_Car_XRay") as Material;
		//Cant change the prefab, instead change the material when it is instantiated
		foreach (PlayerBuddy buddy in playerBuddies) {
			//Null check probably not necessary
			if (buddy != null) {
				if (buddy.buddyCheck(BuddySkillEnum.Chronologist)) { // Returns true if buddy has that skill
					Time.timeScale = (Time.timeScale * buddy.chronologist_cameraSlowdownPercentage);
				}
				if (buddy.buddyCheck(BuddySkillEnum.Radiologist)) {
					//Transform all car meshes into the Xray material
					GameObject[] enemyCarArray = GameObject.FindGameObjectsWithTag ("EnemyCar");
					for (int i = 0; i < enemyCarArray.Length; i++) {
						enemyCarArray [i].GetComponent<MeshRenderer> ().material = Resources.Load ("Enemy_Car_XRay") as Material;
					}

					//Transform all bridge pieces into the Xray material
					GameObject[] bridgeArray = GameObject.FindGameObjectsWithTag ("Bridge");
					for (int i = 0; i < bridgeArray.Length; i++) {
						foreach (MeshRenderer mesh in bridgeArray[i].GetComponentsInChildren<MeshRenderer>()) {
							mesh.material = Resources.Load ("Enemy_Car_XRay") as Material;
						}
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
		inputHandler.isCameraMoving = false;
	}

	//Starts on level start, transitions camera from the current intro cam spot to the normal top cam position
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

		inputHandler.isCameraMoving = false;
		inputHandler.inputEnabled = true;
	}

	public List<Lane> generateLanes(int numberOfLanes, float xPos, float laneWidth, float laneSpacing) {
		List<Lane> newLaneList = new List<Lane> ();
		for (int i = 0; i < numberOfLanes; i++) {
			GameObject newLane = Instantiate (lanePrefab, new Vector3 (xPos, 0.01f, 0f), lanePrefab.transform.rotation) as GameObject;
			if (i == 0) {
				newLane.GetComponent<Lane> ().isLeftmostLane = true;
			}
			if (i == (numberOfLanes - 1)) {
				newLane.GetComponent<Lane> ().isRightmostLane = true;
			}
			newLane.transform.localScale = new Vector3 (laneWidth, 40f, 1f);
			newLaneList.Add(newLane.GetComponent<Lane>());
			xPos += laneSpacing;
		}
		return newLaneList;
	}


	/************
	 * 
	 * Doomsayer Functions for Bubbles etc.
	 */
	public void spawnDoomsayerBubble(float badBubbleSize, float goodBubbleSize) {
		GameObject newDoomsayerBubble = Instantiate (doomsayerBubbleButtonPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		RectTransform bubbleRect = newDoomsayerBubble.GetComponent<RectTransform> ();
		float anchorRange = Random.Range (0.1f, 0.4f);
		Vector2 newMinAnchor = new Vector2 (anchorRange, anchorRange);
		Vector2 newMaxAnchor = new Vector2 (anchorRange + badBubbleSize, anchorRange + badBubbleSize);
		bubbleRect.anchorMin = newMinAnchor;
		bubbleRect.anchorMax = newMaxAnchor;

		newDoomsayerBubble.GetComponent<RectTransform> ().SetParent (doomsayerPanel.transform, false);
		newDoomsayerBubble.GetComponent<Button>().onClick.AddListener(() => { 
			doomsayerBubbleClicked(newDoomsayerBubble.GetComponent<Button>()); 
		});
	}

	public void doomsayerBubbleClicked(Button doomsayerBubbleButton) {
		Vector2 newAnchorMin = doomsayerBubbleButton.GetComponent<RectTransform> ().anchorMin;
		Vector2 newAnchorMax = doomsayerBubbleButton.GetComponent<RectTransform> ().anchorMax;
		newAnchorMin.x += 0.05f;
		newAnchorMax.x -= 0.05f;
		newAnchorMin.y += 0.05f;
		newAnchorMax.y -= 0.05f;

		//Bubble has grown small and should be destroyed
		if (newAnchorMax.x - newAnchorMin.x <= .1f) {
			Destroy (doomsayerBubbleButton.gameObject);
			//Throw out a good bubble
			doomsayerSpawnGoodBubble();
		} else {
			doomsayerBubbleButton.GetComponent<RectTransform> ().anchorMin = newAnchorMin;
			doomsayerBubbleButton.GetComponent<RectTransform> ().anchorMax = newAnchorMax;
		}
	}

	//Need to destroy this bubble
	public void doomsayerSpawnGoodBubble() {
		Vector3 spawnPos = playerCarObj.transform.position;
		spawnPos.z += 2;
		GameObject goodBubble = Instantiate (doomsayerGoodBubblePrefab, spawnPos, Quaternion.identity) as GameObject;
		goodBubble.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 300, 2000));
		goodBubble.GetComponent<Rigidbody> ().AddTorque (new Vector3 (300, 300, 300));
		goodBubble.AddComponent<MiscObjectMover> ();
	}

	// todo: Handled here or in PlayerBuddy?
	public PlayerBuddy buddyCheck(BuddySkillEnum buddyCheckId) {
		foreach (PlayerBuddy buddy in playerBuddies) {
			if (buddy.buddySkillEnum == buddyCheckId) {
				return buddy;
			}
		}
		return null;
	}
}


