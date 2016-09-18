using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

	public SceneController sceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	public GameObject enemyCarPrefab;

	private int playerDifficulty = 1;
	private int playerLevel = 1;
	private int carLevel = 1;

	// Use this for initialization
	void Start () {
		spawnInterval = SceneConstants.BASIC_CAR_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_CAR_SPAWN_TIME;
		enemyCarPrefab.GetComponent<EnemyCarMover> ().sceneController = sceneController;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			GameObject newCar = Instantiate (enemyCarPrefab, new Vector3 (chooseLane (), 0, 25), Quaternion.identity) as GameObject;
			setupNewCar (newCar);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= (Time.deltaTime * sceneController.SCENE_SPEED);
	}

	//Set up the position of the new car, its speed, etc.
	private void setupNewCar(GameObject car) {
		float length = Random.Range (1, 8);
		float height = Random.Range (1, 5);
		float velocity = Random.Range (1.0f, 4.0f) * SceneConstants.BASE_CAR_VELOCITY * playerLevel * playerDifficulty * carLevel;
		EnemyCarMover mover = car.GetComponent<EnemyCarMover> ();
		mover.setVelocity (velocity);
		car.transform.localScale = new Vector3 (1, height, length);
		car.transform.position = new Vector3 (car.transform.position.x, height / 2, car.transform.position.z);
	}

	private float chooseLane() {
		//Will depend on how many lanes there are
		switch (Random.Range(0, 5)) {
		case 0:
			return -6f;
		case 1:
			return -3f;
		case 2:
			return 0f;
		case 3:
			return 3f;
		case 4:
			return 6f;
		default:
			return 0f;
		}
	}
}
