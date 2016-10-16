using UnityEngine;
using System.Collections;

public class TerrainSpawner : MonoBehaviour {

	public LevelSceneController levelSceneController;

	private float spawnInterval;
	private float timeUntilNextSpawn;

	public GameObject[] treePrefabs = new GameObject[5]; 
	// Use this for initialization
	void Awake() {
		levelSceneController = GameObject.Find ("LevelSceneController").GetComponent<LevelSceneController>();
	}

	void Start () {
		spawnInterval = SceneConstants.BASIC_TERRAIN_SPAWN_TIME;
		timeUntilNextSpawn = SceneConstants.BASIC_TERRAIN_SPAWN_TIME;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			GameObject newTerrain = Instantiate (treePrefabs[chooseTerrain(treePrefabs.Length)], new Vector3 (getXPos(), 0, SceneConstants.OBJECT_SPAWN_POSITION), Quaternion.identity) as GameObject;
			setupNewTerrain (newTerrain);
			timeUntilNextSpawn = spawnInterval;
		}

		timeUntilNextSpawn -= (Time.deltaTime * levelSceneController.SCENE_SPEED);
	}

	//Set up the position of the new car, its speed, etc.
	private void setupNewTerrain(GameObject terrain) {
		MiscObjectMover mover = terrain.AddComponent<MiscObjectMover> ();
		mover.levelSceneController = levelSceneController;
		mover.setVelocity (SceneConstants.BASE_OBJECT_VELOCITY);
	}

	//Chooses which prefab to instantiate
	private int chooseTerrain(int numberOfObjects) {
		return Random.Range (0, numberOfObjects);
	}

	//Will need to be based on the current level width to not interfere with lanes
	private float getXPos() {
		//Debugging and testing without terrain
		int side;
		if (SceneConstants.TERRAIN_LEFT_SIDE_ONLY == 1) {
			side = 0;
		} else {
			side = Random.Range (0, 2);
		}
			
		if (side == 0) { //Left Side - x between -8 and -14
			return Random.Range(-13.5f, -7.5f);
		} else { //Right Side - x between 8 and 14
			return Random.Range(8.5f, 14.5f);
		}
	}
}
