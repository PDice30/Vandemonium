using UnityEngine;
using System.Collections;

public class PlayerBuddy : MonoBehaviour {

	public int buddyId;
	public BuddySkillEnum buddySkillEnum;
	public string buddyName;
	public string buddyTitle;

	public int buddyLevel;

	//The Chronologist
	public float chronologist_cameraSlowdownPercentage = 0;
	public float chronologist_cameraSlowdownTime = 0;
	public float chronologist_relativePlayerSpeed = 0; // Or just a bool for normal speed?
	// The Rocker
	public bool rocker_carsDoCollide = false;
	public float rocker_carsCollisionForce = 0;
	public bool rocker_hasShieldUpgrade = false;
	// The Radiologist
	public float radiologist_xrayTime = 0;
	public bool radiologist_hasNightVision = false;
	// The Pilferer
	public float pilferer_dayCoinBonus = 0;
	public float pilferer_nightCoinBonus = 0;
	public bool pilferer_canMarkCarWithTreasure = false;
	// The Medium
	public float medium_additionalCrystalFrequency = 0;
	public float medium_crystalDurationBonus = 0;
	public bool medium_invulnerableDuringCrystal = false;
	// The Sidewinder
	public float sidewinder_laneChangeSpeed = 0;
	public bool sidewinder_hasTeleport = false;
	// The Diviner
	public float diviner_shieldDuration = 0;
	public bool diviner_destroyAllCarsOnDamage = false;
	// The Mechanic
	public int mechanic_extraHealth = 0;
	public float mechanic_additionalHealthSpawnFrequency = 0;
	public bool mechanic_canActivateSavior = false;
	// The Jester
	public float jester_laneChangeFrequency = 0;
	public float jester_cameraChangeRefillBonus = 0;
	//The Doomsayer
	public float doomsayer_speechBubbleFrequency = 0;
	public bool doomsayer_canEnterBubbleBlast = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
