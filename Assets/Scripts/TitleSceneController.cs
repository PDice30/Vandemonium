using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TitleSceneController : MonoBehaviour {

	//TODO
	//All of these game objects should probably just be their components
	//when referring to the game object just use .gameobject

	// Use this for initialization
	public GameObject buddyOneChosen;
	public GameObject buddyTwoChosen;
	public GameObject buddyThreeChosen;
	public GameObject buddyFourChosen;
	public GameObject buddyFiveChosen;

	private GameObject buddyCurrentlySelected; // One of these 5 Chosen Buddies
	private PlayerBuddy buddyScrollCurrentlySelected; //Buddy from the scroll view, not yet chosen

	public GameObject startButtonObject;

	public GameObject selectBuddyPanel;

	//Buddy Stats Panel
	//These might want to be prefabs for easier loading of buddy info, and then instantiate.
	public GameObject buddyStatsPanel;
	public Image buddyStatsImage;
	public Text buddyStatsName;
	public Text buddyStatsTitle;


	public GameObject buddyScrollView;
	public GameObject buddyScrollViewContent;

	public Sprite[] buddyImages = new Sprite[SceneConstants.NUMBER_OF_PLAYER_BUDDIES];
	public Sprite lockedBuddyImage;

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

	public Button resetPlayerPrefsButton;

	private GameObject currentCanvasObj;

	public LevelSceneController levelSceneController;

	void Awake () {

		//TODO Currently when returning to the scene, all of this stuff needs to be reloaded
		//Is this the proper way to do this?
		//What about when someone level's up their buddy?
		//Just change that buddy in the list?

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

		//Probably put this in a function that can also be called when a player levels up a buddy
		// or unlocks a buddy
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
			//If not unlocked, have different sprite and not clickable
			newPlayerBuddy.GetComponent<Button>().image.sprite = buddyImages[i];
			if (!playerBuddy.isUnlocked) {
				newPlayerBuddy.GetComponent<Button>().image.color = Color.black;
			} 


			//The Newlw created button references the buddy created from this script
			newPlayerBuddy.GetComponent<Button> ().onClick.AddListener(() => { 
				initialBuddyScrollButtonClicked(playerBuddy); 
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


	/********** UI and Button Navigation *************/

	public void buddyChosenButtonClicked(Button buddyButton) {
		buddyCurrentlySelected = buddyButton.gameObject;

		selectBuddyPanel.SetActive (true);
		buddyScrollView.SetActive (true);
		playCanvasBackButton.SetActive (false);
		startButtonObject.SetActive (false);

	}

	//This will open a panel that displays the buddy's stats and such
	public void initialBuddyScrollButtonClicked(PlayerBuddy buddy) {
		buddyScrollCurrentlySelected = buddy;
		buddyStatsImage.sprite = buddyImages [buddy.buddyId];
		if (!buddy.isUnlocked) {
			buddyStatsImage.color = Color.black;
		} else {
			buddyStatsImage.color = Color.white;
		}
		buddyStatsName.text = buddy.buddyName;
		buddyStatsTitle.text = buddy.buddyTitle;

		selectBuddyPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		buddyStatsPanel.SetActive (true);
	}

	// This will actually be the buddy CONFIRMATION Button function
	//Instead, pass an int as well to represent the image index
	public void buddyStatsPanelConfirmClicked() {
		Debug.Log ("added player buddy object:" + buddyScrollCurrentlySelected.name);
		buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy = buddyScrollCurrentlySelected;
		buddyCurrentlySelected.GetComponent<Button> ().image.sprite = buddyImages [buddyScrollCurrentlySelected.buddyId];
		//Cycles through the chosen buddies and sees if the selected buddy is a duplicate and removes it from the chosen list.
		foreach (GameObject chosenBuddyButton in chosenBuddyButtons) {
			if ((chosenBuddyButton != buddyCurrentlySelected) &&
			    (chosenBuddyButton.GetComponent<ChosenBuddy> ().buddy == buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy)) {
				chosenBuddyButton.GetComponent<ChosenBuddy> ().buddy = null;
				chosenBuddyButton.GetComponent<Button> ().image.sprite = null;
			}
		}

		buddyScrollCurrentlySelected = null;
		selectBuddyPanel.SetActive (false);
		buddyStatsPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		startButtonObject.SetActive (true);
		playCanvasBackButton.SetActive (true);

	}

	public void buddyStatsPanelCancelClicked() {
		buddyScrollCurrentlySelected = null;
		selectBuddyPanel.SetActive (false);
		buddyStatsPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		startButtonObject.SetActive (true);
		playCanvasBackButton.SetActive (true);

	}

	public void menuButtonClicked(Button menuButton) {
		// Convert to switch
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


	/* Setup all of the players initial prefs for first time playing */
	void setupInitialPlayerPrefs() {
		PlayerPrefs.SetInt (PlayerConstants.Player_FirstTimePlaying, 0);

		//Player Setup
		PlayerPrefs.SetInt(PlayerConstants.Player_TotalCoins, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestScore, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestCoins, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestZone, 0);
		//Chronologist Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedChronologist, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Chronologist_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownPercentage, 0.5f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownTime, 0f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_RelativePlayerSpeed, 0f);
		//Rocker Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedRocker, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_Level, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_CarsDoCollide, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_NumberOfCarCollisions, 5);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Rocker_CarsCollisionForceMultiplier, 1.5f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_HasShieldUpgrade, 0);
		//Radiologist Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedRadiologist, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Radiologist_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Radiologist_XRayTime, 2.0f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Radiologist_HasNightVision, 0);
		//Pilferer Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedPilferer, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Pilferer_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Pilferer_DayCoinBonus, 1.05f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Pilferer_NightCoinBonus, 1.1f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Pilferer_CanMarkCarWithTreasure, 0);
		//Medium Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedMedium, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Medium_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Medium_AdditionalCrystalFrequency, 1.2f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Medium_CrystalDurationBonus, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Medium_IsInvulnerableDuringCrystal, 0);
		//Sidewinder Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedSidewinder, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Sidewinder_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Sidewinder_LaneChangeSpeed, 1.5f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Sidewinder_HasTeleport, 0);
		//Diviner Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedDiviner, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Diviner_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Diviner_ShieldDuration, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Diviner_HasDestroyAllCarsOnDamage, 0);
		//Mechanic Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedMechanic, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_Level, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_ExtraHealth, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Mechanic_AdditionalHealthSpawnFrequency, 1.2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_CanActivateSavior, 0);
		//Jester Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedJester, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Jester_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Jester_LaneChangeFrequency, 10f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Jester_CameraChangeRefillBonus, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Jester_CanIgnoreDamage, 0);
		//Doomsayer Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedDoomsayer, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Doomsayer_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_SpeechBubbleFrequency, 10f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_BadSpeechBubbleSize, .5f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_GoodSpeechBubbleSize, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Doomsayer_CanEnterBubbleBlast, 0);

		PlayerPrefs.Save ();

		//
		Debug.Log ("First Time Setup complete");

	}

	void getPlayerPrefs() {
		
	}

	public void resetPlayerPrefs() {
		//Player Setup
		PlayerPrefs.SetInt(PlayerConstants.Player_TotalCoins, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestScore, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestCoins, 0);
		PlayerPrefs.SetInt(PlayerConstants.Player_BestZone, 0);
		//Chronologist Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedChronologist, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Chronologist_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownPercentage, 0.5f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownTime, 0f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Chronologist_RelativePlayerSpeed, 0f);
		//Rocket Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedRocker, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_Level, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_CarsDoCollide, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_NumberOfCarCollisions, 5);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Rocker_CarsCollisionForceMultiplier, 1.5f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Rocker_HasShieldUpgrade, 0);
		//Radiologist Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedRadiologist, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Radiologist_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Radiologist_XRayTime, 2.0f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Radiologist_HasNightVision, 0);
		//Pilferer Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedPilferer, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Pilferer_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Pilferer_DayCoinBonus, 1.05f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Pilferer_NightCoinBonus, 1.1f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Pilferer_CanMarkCarWithTreasure, 0);
		//Medium Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedMedium, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Medium_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Medium_AdditionalCrystalFrequency, 1.2f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Medium_CrystalDurationBonus, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Medium_IsInvulnerableDuringCrystal, 0);
		//Sidewinder Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedSidewinder, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Sidewinder_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Sidewinder_LaneChangeSpeed, 1.5f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Sidewinder_HasTeleport, 0);
		//Diviner Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedDiviner, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Diviner_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Diviner_ShieldDuration, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Diviner_HasDestroyAllCarsOnDamage, 0);
		//Mechanic Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedMechanic, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_Level, 1);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_ExtraHealth, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Mechanic_AdditionalHealthSpawnFrequency, 1.2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Mechanic_CanActivateSavior, 0);
		//Jester Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedJester, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Jester_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Jester_LaneChangeFrequency, 10f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Jester_CameraChangeRefillBonus, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Jester_CanIgnoreDamage, 0);
		//Doomsayer Setup
		PlayerPrefs.SetInt (PlayerConstants.Player_HasUnlockedDoomsayer, 0);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Doomsayer_Level, 1);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_SpeechBubbleFrequency, 10f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_BadSpeechBubbleSize, .5f);
		PlayerPrefs.SetFloat (PlayerConstants.Buddy_Doomsayer_GoodSpeechBubbleSize, 2f);
		PlayerPrefs.SetInt (PlayerConstants.Buddy_Doomsayer_CanEnterBubbleBlast, 0);

		PlayerPrefs.Save ();

		Debug.Log ("Prefs Reset");
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
