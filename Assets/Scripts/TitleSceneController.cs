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

	public GameObject startButtonObject;

	public Button buddyOneButton;

	public GameObject chooseBuddyPanel;
	public GameObject buddyScrollView;

	public GameObject buddyScrollViewContent;

	public Sprite[] buddyImages = new Sprite[SceneConstants.NUMBER_OF_PLAYER_BUDDIES];

	public List<PlayerBuddy> allPlayerBuddies;
	public List<PlayerBuddy> chosenPlayerBuddies;
	public List<GameObject> listOfChosenBuddies;

	public GameObject buddySelectButtonPrefab;
	public GameObject playerBuddyPrefab;

	public GameObject playCanvas;
	public GameObject mainCanvas;

	private Canvas currentCanvas;

	private GameObject buddyCurrentlySelected;

	public LevelSceneController levelSceneController;

	void Awake () {
		//Setup/Get PlayerPrefs


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
			allPlayerBuddies.Add (playerBuddy);


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

	//RENAME!
	void Start () {
		allPlayerBuddies = new List<PlayerBuddy> ();

		listOfChosenBuddies.Add (buddyOneChosen);
		listOfChosenBuddies.Add (buddyTwoChosen);
		listOfChosenBuddies.Add (buddyThreeChosen);
		listOfChosenBuddies.Add (buddyFourChosen);
		listOfChosenBuddies.Add (buddyFiveChosen);

		//chrono2.AddComponent<Transform> ();
		//chrono2.transform.position = new Vector3 (0, 0, 0);

		//DontDestroyOnLoad (chrono2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startButtonClicked() {
		foreach (GameObject buddyChosen in listOfChosenBuddies) {
			if (buddyChosen.GetComponent<ChosenBuddy> ().buddy != null) {
				chosenPlayerBuddies.Add (buddyChosen.GetComponent<ChosenBuddy> ().buddy);
			}
		}	

		foreach (PlayerBuddy buddy in chosenPlayerBuddies) {
			DontDestroyOnLoad (buddy);
		}

		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("LevelScene");
	}


	//Instead, pass an int as well to represent the image index
	public void buddyScrollButtonClicked(PlayerBuddy buddy) {

		Debug.Log ("added player buddy object:" + buddy.name);

		//buddyCurrentlySelected.GetComponent<Button>().image.sprite = buddy.GetComponent<Button>().image.sprite;
		buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy = buddy;
		Debug.Log (buddy.buddyId);
		buddyCurrentlySelected.GetComponent<Button> ().image.sprite = buddyImages [buddy.buddyId];
		chooseBuddyPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		startButtonObject.SetActive (true);

	}
	/*
	public void buddyScrollButtonClicked(PlayerBuddy buddy) {

		Debug.Log ("added player buddy object:" + buddy.name);

		//buddyCurrentlySelected.GetComponent<Button>().image.sprite = buddy.GetComponent<Button>().image.sprite;
		buddyCurrentlySelected.GetComponent<ChosenBuddy> ().buddy = buddy;
		//buddyCurrentlySelected
		chooseBuddyPanel.SetActive (false);
		buddyScrollView.SetActive (false);
		startButtonObject.SetActive (true);

	}*/

	public void buddySelectedButtonClicked(Button buddyButton) {
		buddyCurrentlySelected = buddyButton.gameObject;
		chooseBuddyPanel.SetActive (true);
		buddyScrollView.SetActive (true);
		startButtonObject.SetActive (false);

	}

	public void menuButtonClicked(Button menuButton) {
		if (menuButton.name == "ButtonToPlay") {
			mainCanvas.SetActive (false);
			playCanvas.SetActive (true);
		}
	}


	void setupInitialPlayerPrefs() {
		PlayerPrefs.SetInt ("Player_FirstTimePlaying", 0);
		//Chronologist Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedChronologist", 0);
		PlayerPrefs.SetInt ("Buddy_Chronologist_Level", 1);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownPercentage", 0.5f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
		//Rocket Setup
		PlayerPrefs.SetInt ("Player_HasUnlockedRocker", 0);
		PlayerPrefs.SetInt ("Buddy_Rocker_Level", 1);
		PlayerPrefs.SetInt ("Buddy_Rocker_CarsDoCollide", 1);
		PlayerPrefs.SetFloat ("Buddy_Chronologist_CameraSlowdownTime", 0f);
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


	void setChronoStats() {
		PlayerPrefs.SetInt ("Buddy_ChronologistLevel", 1);
		PlayerPrefs.SetFloat ("Buddy_CameraSlowdownPercentage", 0.5f);
	}
		


}
