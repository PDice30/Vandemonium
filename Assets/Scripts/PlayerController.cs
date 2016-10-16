using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/********
 * PlayerController
 * - Handles everything related to the player, like health, where the player is, how it's moving, if it can move
 * - Handles collisions and triggers that occur on the car itself
 ********/ 
public class PlayerController : MonoBehaviour {
	public PlayerInputHandler inputHandler;
	private LevelSceneController levelSC;

	private Transform playerTransform;

	public int playerHealth;
	public int playerArmor;

	//These might best be handled completely by the buddies, no need for two of the same vars
	public float laneChangeTime;

	//Player Position etc
	public int currentLaneIndex;

	//Booleans
	public bool isPlayerInvulnerable = false;
	private bool isCarMovingLeft = false;
	private bool isCarMovingRight = false;

	//public float cameraChangeTime;
	//Is this used?
	private float journeyLength;


	void Awake() {
		levelSC = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController> ();
	}

	void Start () {

		//Based on buddies and level etc
		//Temp
		playerHealth = SceneConstants.DEFAULT_PLAYER_HEALTH;
		playerArmor = 0;

		foreach (PlayerBuddy buddy in levelSC.playerBuddies) {
			Debug.Log ("Buddy: " + buddy.buddySkillEnum.ToString ());
		}
		//All buddy code will be handled in the title scene, although
		// it is possible some buddy addying code will be used during the game.

		//Buddies will be added on Start from an object in the title screen that will load up the buddies
		//Add buddies from the previous scene.
		//addPlayerBuddy(BuddySkillEnum.Chronologist);
		laneChangeTime = 0.25f;
		playerTransform = gameObject.transform;

		currentLaneIndex = (levelSC.lanes.Count / 2);  //Int rounded to mid lane
	}

	void Update () {
		//Input is handled in PlayerInputHandler component attached to the PlayerCar
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
		inputHandler.inputEnabled = false;
		if (direction == -1 && !levelSC.lanes[currentLaneIndex].isLeftmostLane) { //Move left/Up, check for LANE!
			isCarMovingLeft = true;
			currentLaneIndex -= 1;
			float newXPos = levelSC.lanes[currentLaneIndex].transform.position.x;
			Vector3 originalPlayerPosition = playerTransform.position;
			Vector3 newPlayerPosition = new Vector3 (newXPos, playerTransform.position.y, playerTransform.position.z);
			while (timeLeft < laneChangeTime) {
				timeLeft += Time.deltaTime;
				playerTransform.position = Vector3.Lerp(originalPlayerPosition, newPlayerPosition, (timeLeft / laneChangeTime));
				yield return null;
			}
		} else if (direction == 1 && !levelSC.lanes[currentLaneIndex].isRightmostLane) { //Move right/down, check for LANE!
			isCarMovingRight = true;
			currentLaneIndex += 1;
			float newXPos = levelSC.lanes[currentLaneIndex].transform.position.x;
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
		inputHandler.inputEnabled = true;
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
					levelSC.beginGameOver ();
				}	

				foreach (PlayerBuddy buddy in levelSC.playerBuddies) {
					if (buddy.buddyCheck (BuddySkillEnum.Rocker)) {
						remainingCarCollisions = buddy.rocker_numberOfCarCollisions;
					}
				}

				levelSC.numberOfHits += 1;
				levelSC.numberOfHitsText.text = "Hits: " + levelSC.numberOfHits;
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
				levelSC.numberOfCoins += 1;
				levelSC.numberOfCoinsText.text = "Coins: " + levelSC.numberOfCoins;
				coinMover.coinAudioSource.PlayOneShot (coinMover.coinAudioClip);
				coinMover.hasBeenCollected = true;
			}

			//Destroy (coll.gameObject);
		}
	}
		

}
