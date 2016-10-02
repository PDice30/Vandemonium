using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TitleSceneController : MonoBehaviour {

	// Use this for initialization
	public GameObject buddyOneChosen;
	public GameObject buddyTwoChosen;
	public GameObject buddyThreeChosen;
	public GameObject buddyFourChosen;
	public GameObject buddyFiveChosen;
	private GameObject buddyCurrentlySelected; // One of these 5 Chosen Buddies

	public GameObject startButtonObject;

	public GameObject selectBuddyPanel;

	public GameObject buddyScrollView;
	public GameObject buddyScrollViewContent;

	public Sprite[] buddyImages = new Sprite[SceneConstants.NUMBER_OF_PLAYER_BUDDIES];

	public List<PlayerBuddy> playerBuddyCatalog = new List<PlayerBuddy> ();
	public List<PlayerBuddy> finalChosenPlayerBuddies = new List<PlayerBuddy> ();
	public List<GameObject> chosenBuddyButtons = new List<GameObject>();

	public GameObject buddySelectButtonPrefab;
	public GameObject playerBuddyPrefab;


	public Camera mainCamera;

	public GameObject playCanvasBackButton;

	//Could all be in an array
	public GameObject mainCanvasObj;
	public GameObject playCanvasObj;
	public GameObject storeCanvasObj;
	public GameObject scoresCanvasObj;
	public GameObject achievementsCanvasObj;
	public GameObject statsCanvasObj;

	private GameObject currentCanvasObj;

	public LevelSceneController levelSceneController;

	void Awake () {

		/**
		 * Set all of the canvas's camera components to the main camera
		 * and set them all not active
		 */
		resetCanvasesAndCameras ();
		currentCanvasObj = mainCanvasObj;

		//Setup/Get PlayerPrefs
	
		//Setup saved chosen buddies
		//If they have chronologist as buddy 1, keep it buddy 1.

		//Will Only happen once
		if (PlayerPrefs.GetInt ("Player_FirstTimePlaying", 1) == 1) {
			setupInitialPlayerPrefs ();
		} 

		float xIncrement = 0f;
		for (int i = 0; i < SceneConstants.NUMBER_OF_PLAYER_BUDDIES; i++) {
			GameObject newObject = new GameObject ();
			PlayerBuddy playerBuddy = newObject.AddComponent<PlayerBuddy> ();
			playerBuddy.buddyId = i;
			playerBuddy.buddySkillEnum = (BuddySkillEnum)i;
			newObject.name = playerBuddy.buddySkillEnum.ToString ();
			playerBuddy.getBuddyStats ();
			playerBuddyCatalog.Add (playerBuddy);


			//Button Setup for Buddy Select
			bool topRow = true;
			if (i % 2 == 1) {
				topRow = false;
			}

			GameObject newPlayerBuddy = Instantiate (buddySelectButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			newPlayerBuddy.name = "ButtonSelect" + i;
			newPlayerBuddy.GetComponent<RectTransform> ().SetParent (buddyScrollViewContent.transform, false);

			if (topRow) {
				newPlayerBuddy.GetComponent<RectTransform> ().anchorMin = new Vector2 (0f + xIncrement, 0.5f);
				newPlayerBuddy.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.1f + xIncrement, 1.0f);
			} else {
				newPlayerBuddy.GetComponent<RectTransform> ().anchorMin = new Vector2 (0f + xIncrement, 0.0f);
				newPlayerBuddy.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.1f + xIncrement, 0.5f);
			}

			if (!topRow) {
				xIncrement += 0.1f;
			}

			//Set Image of this button
			newPlayerBuddy.GetComponent<Button>().image.sprite = buddyImages[i];

			//The Newlw created button references the buddy created from this script
			newPlayerBuddy.GetComponent<Button> ().onClick.AddListener(() => { 
				buddyScrollButtonClicked(playerBuddy); 
			});

		}
	}


	void Start () {
		chosenBuddyButtons.Add (buddyOneChosen);
		chosenBuddyButtons.Add (buddyTwoChosen);
		chosenBuddyButtons.Add (buddyThreeChosen);
		chosenBuddyButtons.Add (buddyFourChosen);
		chosenBuddyButtons.Add (buddyFiveChosen);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startButtonClicked() {
		foreach (GameObject buddyChosen in chosenBuddyButtons) {
			if (buddyChosen.GetComponent<ChosenBuddy> ().buddy != null) {
				finalChosenPlayerBuddies.Add (buddyChosen.GetComponent<ChosenBuddy> ().buddy);
			}
		}	

		foreach (PlayerBuddy buddy in finalChosenPlayerBuddies) {
			DontDestroyOnLoad (buddy);
		}

		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("LevelScene");
	}


	//Instead, pass an int as well to represent the image index
	public void buddyScrollButtonClicked(PlayerBuddy buddy) {
		Debug.Log ("added player buddy object:" + buddy.name);
		buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy = buddy;
		buddyCurrentlySelected.GetComponent<Button> ().image.sprite = buddyImages [buddy.buddyId];
		//Cycles through the chosen buddies and sees if the selected buddy is a duplicate and removes it from the chosen list.
		foreach (GameObject chosenBuddyButton in chosenBuddyButtons) {
			if ((chosenBuddyButton != buddyCurrentlySelected) &&
			    (chosenBuddyButton.GetComponent<ChosenBuddy> ().buddy == buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy)) {
				chosenBuddyButton.GetComponent<ChosenBuddy> ().buddy = null;
				chosenBuddyButton.GetComponent<Button> ().image.sprite = null;
			}
		}
			
		selectBuddyPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		startButtonObject.SetActive (true);
		playCanvasBackButton.SetActive (true);

	}


	public void buddyChosenButtonClicked(Button buddyButton) {
		buddyCurrentlySelected = buddyButton.gameObject;

		selectBuddyPanel.SetActive (true);
		buddyScrollView.SetActive (true);
		playCanvasBackButton.SetActive (false);
		startButtonObject.SetActive (false);

	}

	public void menuButtonClicked(Button menuButton) {
		if (menuButton.name == "ButtonToPlay") {
			currentCanvasObj.SetActive (false);
			playCanvasObj.SetActive (true);
			currentCanvasObj = playCanvasObj;
		} else if (menuButton.name == "ButtonToStore") {
			currentCanvasObj.SetActive (false);
			storeCanvasObj.SetActive (true);
			currentCanvasObj = storeCanvasObj;
		} else if (menuButton.name == "ButtonToScores") {
			currentCanvasObj.SetActive (false);
			scoresCanvasObj.SetActive (true);
			currentCanvasObj = scoresCanvasObj;
		} else if (menuButton.name == "ButtonToAchievements") {
			currentCanvasObj.SetActive (false);
			achievementsCanvasObj.SetActive (true);
			currentCanvasObj = achievementsCanvasObj;
		} else if (menuButton.name == "ButtonToStats") {
			currentCanvasObj.SetActive (false);
			statsCanvasObj.SetActive (true);
			currentCanvasObj = statsCanvasObj;
		} else if (menuButton.name == "ButtonBackToMain") {
			currentCanvasObj.SetActive (false);
			mainCanvasObj.SetActive (true);
			currentCanvasObj = mainCanvasObj;
			//currentCanvas
		}
	}


	void setupInitialPlayerPrefs() {
		PlayerPrefs.SetInt ("Player_FirstTimePlaying", 0);
		//Chronologist Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedChronologist", 1);
		PlayerPrefs.SetInt ("Buddy_Chronologist_Level", 1);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownPercentage", 0.5f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		//Rocket Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedRocker", 1);
		PlayerPrefs.SetInt ("Buddy_Rocker_Level", 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_CarsDoCollide, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Rocker_CarsCollisionForceMultiplier, 3f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		//Radiologist Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedRadiologist", 0);
		PlayerPrefs.SetInt ("Buddy_Chronologist_Level", 1);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownPercentage", 0.5f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		//Pilferer Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedPilferer", 0);
		//Medium Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedMedium", 0);
		//Pilferer Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedSidewinder", 0);
		//Diviner Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedDiviner", 0);
		//Mechanic Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedMechanic", 0);
		//Jester Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedJester", 0);
		//Doomsayer Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedDoomsayer", 0);
		Debug.Log ("First Time Setup complete");

	}

	void getPlayerPrefs() {
		
	}
		
	private void resetCanvasesAndCameras() {
		playCanvasObj.GetComponent<Canvas> ().worldCamera = mainCamera;
		playCanvasObj.SetActive (false);
		storeCanvasObj.GetComponent<Canvas> ().worldCamera = mainCamera;
		storeCanvasObj.SetActive (false);
		scoresCanvasObj.GetComponent<Canvas> ().worldCamera = mainCamera;
		scoresCanvasObj.SetActive (false);
		achievementsCanvasObj.GetComponent<Canvas> ().worldCamera = mainCamera;
		achievementsCanvasObj.SetActive (false);
		statsCanvasObj.GetComponent<Canvas> ().worldCamera = mainCamera;
		statsCanvasObj.SetActive (false);
	}

}
