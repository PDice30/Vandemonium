using UnityEngine;
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

	//These are updated via the PlayerController Collisions etc.
	public Text numberOfCoinsText;
	public Text numberOfHitsText;
	public float numberOfCoins = 0;
	public float numberOfHits = 0;

	public float SCENE_SPEED;

	void Awake() { 
		//Application.targetFrameRate = 30;
		titleSceneController = GameObject.Find("TitleSceneController").GetComponent<TitleSceneController>();
		getBuddies (titleSceneController.finalChosenPlayerBuddies);
		SCENE_SPEED = SceneConstants.BASE_SCENE_SPEED;

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
}


