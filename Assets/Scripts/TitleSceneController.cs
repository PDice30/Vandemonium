using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TitleSceneController : MonoBehaviour {

	// Use this for initialization
	public Button buddyOneChosenButton;
	public Button buddyTwoChosenButton;
	public Button buddyThreeChosenButton;
	public Button buddyFourChosenButton;
	public Button buddyFiveChosenButton;

	public GameObject startButtonObject;

	public Button buddyOneButton;

	public GameObject chooseBuddyPanel;

	public List<PlayerBuddy> playerBuddies;
	public GameObject playerBuddyPrefab;

	public SceneController sceneController;

	void Start () {
		playerBuddies = new List<PlayerBuddy> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startButtonClicked() {
		DontDestroyOnLoad (gameObject);
		SceneManager.LoadScene ("LevelScene");
	}



	public void buddyButtonClicked(Button buddyButton) {
		if (buddyButton == buddyOneButton) {
			addPlayerBuddy (BuddySkillEnum.Chronologist);
			chooseBuddyPanel.SetActive (false);
			startButtonObject.SetActive (true);
		}
	}

	public void buddySelectButtonClicked(Button buddyButton) {
		chooseBuddyPanel.SetActive (true);
		startButtonObject.SetActive (false);

	}

	public void buddySelected(PlayerBuddy buddy) {
		addPlayerBuddy (buddy.buddySkillEnum);
	}

	public void addPlayerBuddy(BuddySkillEnum skillEnum) {
		//TODO clean this up
		GameObject tempBuddy = Instantiate (playerBuddyPrefab, new Vector3 (0, 20, 0), Quaternion.identity) as GameObject;
		DontDestroyOnLoad (tempBuddy);
		PlayerBuddy newBuddy = tempBuddy.GetComponent<PlayerBuddy> ();
		newBuddy.buddySkillEnum = skillEnum;
		//Temp
		newBuddy.chronologist_cameraSlowdownPercentage = .5f;
		playerBuddies.Add (newBuddy);
	}

}
