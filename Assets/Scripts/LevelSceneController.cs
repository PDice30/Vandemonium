﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//TODO are two scene controllers even necessary?
public class LevelSceneController : MonoBehaviour {

	public LightController lightController;

	private TitleSceneController titleSceneController;


	public List<PlayerBuddy> playerBuddies;

	public GameObject playerCarPrefab;
	private GameObject playerCar;

	public Text numberOfCoinsText;
	public Text numberOfHitsText;

	public float numberOfCoins = 0;
	public float numberOfHits = 0;

	public float SCENE_SPEED;

	void Awake() { 
		//Application.targetFrameRate = 30;
		//RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
		//RenderSettings.ambientIntensity = 1;
		titleSceneController = GameObject.Find("TitleSceneController").GetComponent<TitleSceneController>();

		getBuddies (titleSceneController.chosenPlayerBuddies);
		//getBuddyObjects(titleSceneController.playerBuddyObjects);

		//addPlayerBuddy (BuddySkillEnum.Chronologist);
		SCENE_SPEED = SceneConstants.BASE_SCENE_SPEED;

	}

	void Start () {
		GameObject playerCar = Instantiate (playerCarPrefab, new Vector3 (0f, 0.5f, -10f), Quaternion.identity) as GameObject;
		this.playerCar = playerCar;
		lightController.PlayerSpotlight = playerCar.GetComponentInChildren<Light> ();
		//playerCarPrefab.GetComponent<PlayerController> ().sceneController = gameObject.GetComponent<SceneController> ();
		
	}
	void Update () {
		
	}

	public void transitionToLevel() {
		
	}


	public void getBuddies(List<PlayerBuddy> buddies) {
		playerBuddies = buddies;
	}
	/*
	public void getBuddies(List<PlayerBuddy> buddies) {
		playerBuddies = buddies;
	}

	public void addPlayerBuddy(BuddySkillEnum skillEnum) {
		GameObject tempBuddy = Instantiate (playerBuddyPrefab, new Vector3 (0, 20, 0), Quaternion.identity) as GameObject;
		PlayerBuddy newBuddy = tempBuddy.GetComponent<PlayerBuddy> ();
		newBuddy.buddySkillEnum = skillEnum;
		//Temp
		newBuddy.chronologist_cameraSlowdownPercentage = .5f;
		playerBuddies.Add (newBuddy);
	}
	*/
}


