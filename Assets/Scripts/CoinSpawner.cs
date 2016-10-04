using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public LevelSceneController levelSceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	int coinRunRand;

	public GameObject coinPrefab;

	void Awake () {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController> ();
	}

	void Start () {
		spawnInterval = SceneConstants.BASE_COIN_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASE_COIN_SPAWN_TIME;
	}
	

	void Update () {
		if (timeUntilNextSpawn <= 0) {
			//See if it's a coin run!
			coinRunRand = Random.Range(0, 99);
			if (coinRunRand <= SceneConstants.COIN_RUN_CHANCE) {
				float laneToSpawn = chooseLane ();
				for (int i = 0; i < Random.Range(SceneConstants.MIN_COIN_RUN, SceneConstants.MAX_COIN_RUN); i++) {
					GameObject newCoin = Instantiate (coinPrefab, new Vector3 (laneToSpawn, 1, SceneConstants.OBJECT_SPAWN_POSITION + i), coinPrefab.transform.rotation) as GameObject;
					setupNewCoin (newCoin);
				}
			} else {
				GameObject newCoin = Instantiate (coinPrefab, new Vector3 (chooseLane(), 1, SceneConstants.OBJECT_SPAWN_POSITION), coinPrefab.transform.rotation) as GameObject;
				setupNewCoin (newCoin);
			}

			timeUntilNextSpawn = spawnInterval;

		}

		timeUntilNextSpawn -= (Time.deltaTime * levelSceneController.SCENE_SPEED);
	}

	void setupNewCoin(GameObject coin) {
		CoinMover mover = coin.AddComponent<CoinMover> ();
		mover.rotationSpeed = 1.0f;
		mover.levelSceneController = levelSceneController;
		mover.coinVelocity = SceneConstants.BASE_COIN_VELOCITY;

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
