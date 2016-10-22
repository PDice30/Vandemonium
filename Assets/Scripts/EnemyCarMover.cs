using UnityEngine;
using System.Collections;

public class EnemyCarMover : MonoBehaviour {

	// PUT THE OBJECT AS A PREFAB, USE THE SCENE CONTROLLER!!
	//public PlayerController playerController;
	private float carVelocity;
	private float sceneVelocity;
	//public GameObject sceneControllerObj;
	public LevelSceneController levelSceneController;

	//private bool isMarkedForDestroy = false; // Currently not in use

	private Rigidbody carRigidbody;
	[SerializeField] //For Debugging
	private int remainingCarCollisions;

	//Make sure this is all hooked up properly with the finding of the levelSceneController
	//Why is this not hooked up in the editor?
	void Awake() {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
		carRigidbody = gameObject.GetComponent<Rigidbody> ();
	}

	void Start () {
		remainingCarCollisions = 0;
	//	sceneControllerObj = GameObject.Find ("SceneController");
	//	sceneController = sceneControllerObj.GetComponent<SceneController> ();
	}
	

	void Update () {
		//Possibly will remove its translate update when getting destroyed
		//if (!isMarkedForDestroy) {
			transform.Translate (0, 0, -(Time.deltaTime * carVelocity * levelSceneController.SCENE_SPEED), Space.World);
			if (transform.position.z < SceneConstants.DESTROY_OBJECT_POSITION) {
				Destroy (gameObject);
			}
		//} 
	}


	public void setVelocity(float velocity) {
		carVelocity = velocity;
	}

	// Set up the car's colliders and physics properites so it can collide with the scene.
	public void markForDestroy(int direction, int remainingCollisions) {
		//isMarkedForDestroy = true;
		carRigidbody.useGravity = true;
		carRigidbody.isKinematic = false;
		remainingCarCollisions = remainingCollisions;
		StartCoroutine (moveAndDestroy(direction));
	}

	/***
	 * Get the direction the player is moving and apply a force to the car in that direction
	 * Destroy the actual object after TIME_UNTIL_DESTROY_CAR finishes
	 */
	private IEnumerator moveAndDestroy(int direction) {
		float rocker_carsCollisionForceMultiplier = 1f;
		float xForce, yForce, zForce;
		//Find the rocker and modify the force
		foreach (PlayerBuddy buddy in levelSceneController.playerBuddies) {
			if (buddy.buddyCheck (BuddySkillEnum.Rocker)) {
				rocker_carsCollisionForceMultiplier = buddy.rocker_carsCollisionForceMultiplier;
			}
		}

		if (direction == 0) { //Right
			xForce = Random.Range (SceneConstants.XFORCE_COLLISION_MIN, SceneConstants.XFORCE_COLLISION_MAX) * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX) * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
		} else if (direction == 1) { //Left
			xForce = Random.Range (-SceneConstants.XFORCE_COLLISION_MAX, -SceneConstants.XFORCE_COLLISION_MIN) * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX) * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
		} else {
			xForce = (Random.Range (-SceneConstants.XFORCE_COLLISION_MIN / 2, SceneConstants.XFORCE_COLLISION_MAX / 2)) * 2 * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
			zForce = Random.Range (SceneConstants.ZFORCE_COLLISION_MIN, SceneConstants.ZFORCE_COLLISION_MAX) * rocker_carsCollisionForceMultiplier * SceneConstants.FORCE_TEST_MULTIPLIER;
		}
			
		yForce = Random.Range (SceneConstants.YFORCE_COLLISION_MIN, SceneConstants.YFORCE_COLLISION_MAX);
		carRigidbody.AddForce (new Vector3 (xForce, yForce, zForce));
		carRigidbody.AddTorque (new Vector3 (xForce, yForce, zForce));

		float timeLeft = 0;
		while (timeLeft < SceneConstants.TIME_UNTIL_DESTROY_CAR) {
			timeLeft += Time.deltaTime;
			yield return null;
		}

		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag.Equals("EnemyCar") && remainingCarCollisions >= 1) {
			/**********
			*BuddyCheck here for if car's should collide with each other
			/*********/
			bool playerHasRockerBuddy = false;
			foreach (PlayerBuddy buddy in levelSceneController.playerBuddies) {
				if (buddy.buddyCheck (BuddySkillEnum.Rocker)) {
					playerHasRockerBuddy = true;
				}
			}

			if (playerHasRockerBuddy) {
				Rigidbody collRigidbody = coll.gameObject.GetComponent<Rigidbody> ();
				if (carRigidbody.isKinematic && collRigidbody.isKinematic) {
					//Do Nothing
				} else if (!carRigidbody.isKinematic && collRigidbody.isKinematic) { // Coll
					coll.gameObject.GetComponent<EnemyCarMover> ().markForDestroy (Random.Range (0, 2), remainingCarCollisions - 1);
				} //else if (carRigidbody.isKinematic && !collRigidbody.isKinematic) { // this
					//markForDestroy(Random.Range(0, 2));
				else { //Both are marked for destroy

				}
			}
		}

		if (coll.gameObject.tag.Equals ("DoomsayerBubble")) {
			this.markForDestroy (Random.Range (0, 2), 5); 
		}
	}
		
		


}
