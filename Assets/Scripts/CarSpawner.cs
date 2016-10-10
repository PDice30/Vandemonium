using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {

	public LevelSceneController levelSceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	public GameObject[] enemyCarPrefabs = new GameObject[3];

	//To be used for the speed multipliers, etc.
	private int playerDifficulty = 1;
	private int playerLevel = 1;
	private int carLevel = 1;

	// Use this for initialization
	void Awake() {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
	}

	void Start () {
		spawnInterval = SceneConstants.BASIC_CAR_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_CAR_SPAWN_TIME;
		for (int i = 0; i < enemyCarPrefabs.Length; i++) {
			enemyCarPrefabs[i].GetComponent<EnemyCarMover> ().levelSceneController = levelSceneController;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			int randomCar = Random.Range (0, 3);
			GameObject newCar = Instantiate (enemyCarPrefabs[randomCar], new Vector3 (chooseLane (), 0, SceneConstants.OBJECT_SPAWN_POSITION), Quaternion.identity) as GameObject;
			setupNewCar (newCar);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= (Time.deltaTime * levelSceneController.SCENE_SPEED);
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
		int laneChoice = Random.Range(0, levelSceneController.lanes.Count);
		return levelSceneController.lanes [laneChoice].transform.position.x;
	}
}
