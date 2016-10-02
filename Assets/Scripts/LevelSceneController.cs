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

	public GameObject scoreCanvasObj;
	public GameObject gameOverCanvasObj;

	//These are updated via the PlayerController Collisions etc.
	public Text numberOfCoinsText;
	public Text numberOfHitsText;
	public float numberOfCoins = 0;
	public float numberOfHits = 0;

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

	}

	void Start () {
		GameObject playerCar = Instantiate (playerCarPrefab, new Vector3 (0f, 0.5f, -10f), Quaternion.identity) as GameObject;
		lightController.PlayerSpotlight = playerCar.GetComponentInChildren<Light> ();
		//playerCarPrefab.GetComponent<PlayerController> ().sceneController = gameObject.GetComponent<SceneController> ();
		
	}
	void Update () {
		
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
		scoreCanvasObj.SetActive (false);
		gameOverCanvasObj.SetActive (true);


		//Temp return to main menu
		//levelSceneController.
		//levelSceneController.
		//destroyItemsOnChange();
		//SceneManager.LoadScene("TitleScene");
	}

}


