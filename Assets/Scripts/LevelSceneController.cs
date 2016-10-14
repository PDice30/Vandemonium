using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//TODO are two scene controllers even necessary?
public class LevelSceneController : MonoBehaviour {

	public LightController lightController;
	public Camera playerCamera;
	private TitleSceneController titleSceneController;

	public List<PlayerBuddy> playerBuddies;

	public GameObject playerCarPrefab;


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


	//Game Over Canvas Items
	public GameObject gameOverCanvasObj;
	public Text currentScoreText;
	public Text currentCoinsText;
	public Text currentZoneText;
	public Text bestScoreText;
	public Text bestCoinsText;
	public Text bestZoneText;
	public Text totalCoinsText;

	public float SCENE_SPEED;

	//Could just be a list
	public ArrayList itemsToDestroy = new ArrayList();

	void Awake() {
		//Set other camera's and canvases not active
		gameOverCanvasObj.SetActive(false);

		//Application.targetFrameRate = 30;
		titleSceneController = GameObject.Find("TitleSceneController").GetComponent<TitleSceneController>();
		getBuddies (titleSceneController.finalChosenPlayerBuddies);
		SCENE_SPEED = SceneConstants.BASE_SCENE_SPEED;

		foreach (PlayerBuddy buddy in playerBuddies) {
			itemsToDestroy.Add (buddy.gameObject);
		}
		itemsToDestroy.Add (titleSceneController.gameObject);

		lanes = generateLanes (5, -6f, 2f, 3f);

	}

	void Start () {
		//TODO Player spawned based on lanes
		GameObject playerCar = Instantiate (playerCarPrefab, new Vector3 (0f, 0.5f, -10f), Quaternion.identity) as GameObject;
		lightController.PlayerSpotlight = playerCar.GetComponentInChildren<Light> ();
		//playerCarPrefab.GetComponent<PlayerController> ().sceneController = gameObject.GetComponent<SceneController> ();
		gameStartTime = Time.time;
	}
	void Update () {
		//Testing Zone Transitions etc.
		timeInCurrentZone += Time.deltaTime;

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

}


