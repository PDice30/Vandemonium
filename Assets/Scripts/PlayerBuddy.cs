using UnityEngine;
using System.Collections;

//This should probably be a ScriptableObject
//TODO Maybe not a mono behavior?
public class PlayerBuddy : MonoBehaviour {

	public int buddyId;
	public BuddySkillEnum buddySkillEnum;
	public string buddyName;
	public string buddyTitle;

	public int isUnlocked = 0; //Int for PlayerPrefs

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

	public void copyValues(PlayerBuddy copiedBuddy) {
		copiedBuddy.buddyId = buddyId;
	}

	public void getBuddyStats() {
		switch (buddySkillEnum) {
		case BuddySkillEnum.Chronologist:
			buddyId = 0;
			buddyName = PlayerConstants.buddy_chronologist_name;
			buddyTitle = PlayerConstants.buddy_chronologist_title;
			isUnlocked = PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedChronologist);
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Chronologist_Level);
			chronologist_cameraSlowdownPercentage = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownPercentage);
			chronologist_cameraSlowdownTime = PlayerPrefs.GetFloat ("Buddy_Chronologist_CameraSlowdownTime");
			chronologist_relativePlayerSpeed = PlayerPrefs.GetFloat ("Buddy_Chronologist_RelativePlayerSpeed");
			break;
		case BuddySkillEnum.Rocker:
			buddyId = 1;
			buddyName = "Oswald Oswaldian";
			buddyTitle = "The Rocker";
			isUnlocked = PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedRocker);
			buddyLevel = PlayerPrefs.GetInt ("Buddy_Rocker_Level");
			int temp_rocker_carsDoCollide = PlayerPrefs.GetInt ("Buddy_Rocker_CarsDoCollide");
			if (temp_rocker_carsDoCollide == 1) {
				rocker_carsDoCollide = true;
			}

			chronologist_cameraSlowdownTime = PlayerPrefs.GetFloat ("Buddy_Chronologist_CameraSlowdownTime");
			chronologist_relativePlayerSpeed = PlayerPrefs.GetFloat ("Buddy_Chronologist_RelativePlayerSpeed");
			break;
		default:
			return;
		}


	}


}
