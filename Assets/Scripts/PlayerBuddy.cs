using UnityEngine;
using System.Collections;

//This should probably be a ScriptableObject
//TODO Maybe not a mono behavior?
public class PlayerBuddy : MonoBehaviour {

	public int buddyId;
	public BuddySkillEnum buddySkillEnum;
	public string buddyName;
	public string buddyTitle;

	public bool isUnlocked = false; //Int for PlayerPrefs

	public int buddyLevel;

	//The Chronologist
	public float chronologist_cameraSlowdownPercentage = 0;
	public float chronologist_cameraSlowdownTime = 0;
	public float chronologist_relativePlayerSpeed = 0; // Or just a bool for normal speed?
	// The Rocker
	public bool rocker_carsDoCollide = false;
	public int rocker_numberOfCarCollisions = 0;
	public float rocker_carsCollisionForceMultiplier = 0;
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
	public bool medium_isInvulnerableDuringCrystal = false;
	// The Sidewinder
	public float sidewinder_laneChangeSpeed = 0;
	public bool sidewinder_hasTeleport = false;
	// The Diviner
	public float diviner_shieldDuration = 0;
	public bool diviner_hasDestroyAllCarsOnDamage = false;
	// The Mechanic
	public int mechanic_extraHealth = 0;
	public float mechanic_additionalHealthSpawnFrequency = 0;
	public bool mechanic_canActivateSavior = false;
	// The Jester
	public float jester_laneChangeFrequency = 0;
	public float jester_cameraChangeRefillBonus = 0;
	public bool jester_canIgnoreDamage = false;
	//The Doomsayer
	public float doomsayer_speechBubbleFrequency = 0;
	public float doomsayer_badSpeechBubbleSize = 0;
	public float doomsayer_goodSpeechBubbleSize = 0;
	public bool doomsayer_canEnterBubbleBlast = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//used?
	public void copyValues(PlayerBuddy copiedBuddy) {
		copiedBuddy.buddyId = buddyId;
	}

	public void getBuddyStats() {
		switch (buddySkillEnum) {
		case BuddySkillEnum.Chronologist:
			buddyId = 0;
			buddyName = PlayerConstants.buddy_chronologist_name;
			buddyTitle = PlayerConstants.buddy_chronologist_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedChronologist) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Chronologist_Level);
			chronologist_cameraSlowdownPercentage = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownPercentage);
			chronologist_cameraSlowdownTime = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Chronologist_CameraSlowdownTime);
			chronologist_relativePlayerSpeed = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Chronologist_RelativePlayerSpeed);
			break;
		case BuddySkillEnum.Rocker:
			buddyId = 1;
			buddyName = PlayerConstants.buddy_rocker_name;
			buddyTitle = PlayerConstants.buddy_rocker_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedRocker) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Rocker_Level);
			int temp_rocker_carsDoCollide = PlayerPrefs.GetInt (PlayerConstants.Buddy_Rocker_CarsDoCollide);
			if (temp_rocker_carsDoCollide == 1) {
				rocker_carsDoCollide = true;
			}
			rocker_numberOfCarCollisions = PlayerPrefs.GetInt (PlayerConstants.Buddy_Rocker_NumberOfCarCollisions);
			rocker_carsCollisionForceMultiplier = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Rocker_CarsCollisionForceMultiplier);
			int temp_rocker_hasShieldUpgrade = PlayerPrefs.GetInt (PlayerConstants.Buddy_Rocker_HasShieldUpgrade);
			if (temp_rocker_hasShieldUpgrade == 1) {
				rocker_hasShieldUpgrade = true;
			}
			break;
		case BuddySkillEnum.Radiologist:
			buddyId = 2;
			buddyName = PlayerConstants.buddy_radiologist_name;
			buddyTitle = PlayerConstants.buddy_radiologist_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedRadiologist) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Radiologist_Level);
			radiologist_xrayTime = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Radiologist_XRayTime);
			int temp_radiologist_hasNightVision = PlayerPrefs.GetInt (PlayerConstants.Buddy_Radiologist_HasNightVision);
			if (temp_radiologist_hasNightVision == 1) {
				radiologist_hasNightVision = true;
			}
			break;
		case BuddySkillEnum.Pilferer:
			buddyId = 3;
			buddyName = PlayerConstants.buddy_pilferer_name;
			buddyTitle = PlayerConstants.buddy_pilferer_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedPilferer) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Pilferer_Level);
			pilferer_dayCoinBonus = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Pilferer_DayCoinBonus);
			pilferer_nightCoinBonus = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Pilferer_NightCoinBonus);
			int temp_pilferer_canMarkCarWithTreasure = PlayerPrefs.GetInt (PlayerConstants.Buddy_Pilferer_CanMarkCarWithTreasure);
			if (temp_pilferer_canMarkCarWithTreasure == 1) {
				 pilferer_canMarkCarWithTreasure = true;
			}
			break;
		case BuddySkillEnum.Medium:
			buddyId = 4;
			buddyName = PlayerConstants.buddy_medium_name;
			buddyTitle = PlayerConstants.buddy_medium_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedMedium) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Medium_Level);
			medium_additionalCrystalFrequency = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Medium_AdditionalCrystalFrequency);
			medium_crystalDurationBonus = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Medium_CrystalDurationBonus);
			int temp_medium_isInvulnerableDuringCrystal = PlayerPrefs.GetInt (PlayerConstants.Buddy_Medium_IsInvulnerableDuringCrystal);
			if (temp_medium_isInvulnerableDuringCrystal == 1) {
				medium_isInvulnerableDuringCrystal = true;
			}
			break;
		case BuddySkillEnum.Sidewinder:
			buddyId = 5;
			buddyName = PlayerConstants.buddy_sidewinder_name;
			buddyTitle = PlayerConstants.buddy_sidewinder_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedSidewinder) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Sidewinder_Level);
			sidewinder_laneChangeSpeed = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Sidewinder_LaneChangeSpeed);
			int temp_sidewinder_hasTeleport = PlayerPrefs.GetInt (PlayerConstants.Buddy_Sidewinder_HasTeleport);
			if (temp_sidewinder_hasTeleport == 1) {
				sidewinder_hasTeleport = true;
			}
			break;
		case BuddySkillEnum.Diviner:
			buddyId = 6;
			buddyName = PlayerConstants.buddy_diviner_name;
			buddyTitle = PlayerConstants.buddy_diviner_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedDiviner) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Diviner_Level);
			diviner_shieldDuration = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Diviner_ShieldDuration);
			int temp_diviner_hasDestroyAllCarsOnDamage = PlayerPrefs.GetInt (PlayerConstants.Buddy_Diviner_HasDestroyAllCarsOnDamage);
			if (temp_diviner_hasDestroyAllCarsOnDamage == 1) {
				diviner_hasDestroyAllCarsOnDamage = true;
			}
			break;
		case BuddySkillEnum.Mechanic:
			buddyId = 7;
			buddyName = PlayerConstants.buddy_mechanic_name;
			buddyTitle = PlayerConstants.buddy_mechanic_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedMechanic) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Mechanic_Level);
			mechanic_extraHealth = PlayerPrefs.GetInt (PlayerConstants.Buddy_Mechanic_ExtraHealth);
			mechanic_additionalHealthSpawnFrequency = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Mechanic_AdditionalHealthSpawnFrequency);
			int temp_mechanic_canActivateSavior = PlayerPrefs.GetInt (PlayerConstants.Buddy_Mechanic_CanActivateSavior);
			if (temp_mechanic_canActivateSavior == 1) {
				mechanic_canActivateSavior = true;
			}
			break;
		case BuddySkillEnum.Jester:
			buddyId = 8;
			buddyName = PlayerConstants.buddy_jester_name;
			buddyTitle = PlayerConstants.buddy_jester_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedJester) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Jester_Level);
			jester_laneChangeFrequency = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Jester_LaneChangeFrequency);
			jester_cameraChangeRefillBonus = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Jester_CameraChangeRefillBonus);
			int temp_jester_canIgnoreDamage = PlayerPrefs.GetInt (PlayerConstants.Buddy_Jester_CanIgnoreDamage);
			if (temp_jester_canIgnoreDamage == 1) {
				jester_canIgnoreDamage = true;
			}
			break;
		case BuddySkillEnum.Doomsayer:
			buddyId = 9;
			buddyName = PlayerConstants.buddy_doomsayer_name;
			buddyTitle = PlayerConstants.buddy_doomsayer_title;
			if (PlayerPrefs.GetInt (PlayerConstants.Player_HasUnlockedDoomsayer) == 1) {
				isUnlocked = true;
			}
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.Buddy_Doomsayer_Level);
			doomsayer_speechBubbleFrequency = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Doomsayer_SpeechBubbleFrequency);
			doomsayer_badSpeechBubbleSize = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Doomsayer_BadSpeechBubbleSize);
			doomsayer_goodSpeechBubbleSize = PlayerPrefs.GetFloat (PlayerConstants.Buddy_Doomsayer_GoodSpeechBubbleSize);
			int temp_doomsayer_canEnterBubbleBlast = PlayerPrefs.GetInt (PlayerConstants.Buddy_Doomsayer_CanEnterBubbleBlast);
			if (temp_doomsayer_canEnterBubbleBlast == 1) {
				doomsayer_canEnterBubbleBlast = true;
			}
			break;
		default:
			return;
		}


	}


	/**
	 * Checks if this buddy matches the enum being searched
	 */ 
	public bool buddyCheck(BuddySkillEnum skillEnum) {
		if (buddySkillEnum == skillEnum) {
			return true;
		} else {
			return false;
		}
	}


}



/*
 * 
 * 
		case BuddySkillEnum.:
			buddyId = ;
			buddyName = PlayerConstants.;
			buddyTitle = PlayerConstants.;
			isUnlocked = PlayerPrefs.GetInt (PlayerConstants.);
			buddyLevel = PlayerPrefs.GetInt (PlayerConstants.);
			pilferer_dayCoinBonus = PlayerPrefs.GetFloat (PlayerConstants.);
			pilferer_nightCoinBonus = PlayerPrefs.GetFloat (PlayerConstants.);
			int temp_ = PlayerPrefs.GetInt (PlayerConstants.);
			if (temp_ == 1) {
				  = true;
			}
			break;
*/