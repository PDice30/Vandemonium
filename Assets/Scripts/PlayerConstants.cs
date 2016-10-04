using System.Collections;

namespace UnityEngine {
	public class PlayerConstants {
		/**
		 * Buddy Names and skills etc
		 */

		//Player
		public const string Player_FirstTimePlaying = "Player_FirstTimePlaying";

		public const string Player_TotalCoins = "Player_TotalCoins";
		public const string Player_BestScore = "Player_BestScore";
		public const string Player_BestCoins = "Player_BestCoins";
		public const string Player_BestZone = "Player_BestZone";


		//Achievements
		public const string Achievements_NumberCompleted = "Achievements_NumberCompleted";

		//Player Unlocks
		public const string Player_HasUnlockedChronologist = "Player_HasUnlockedChronologist";
		public const string Player_HasUnlockedRocker = "Player_HasUnlockedRocker";
		public const string Player_HasUnlockedRadiologist = "Player_HasUnlockedRadiologist";
		public const string Player_HasUnlockedPilferer = "Player_HasUnlockedPilferer";
		public const string Player_HasUnlockedMedium = "Player_HasUnlockedMedium";
		public const string Player_HasUnlockedSidewinder = "Player_HasUnlockedSidewinder";
		public const string Player_HasUnlockedDiviner = "Player_HasUnlockedDiviner";
		public const string Player_HasUnlockedMechanic = "Player_HasUnlockedMechanic";
		public const string Player_HasUnlockedJester = "Player_HasUnlockedJester";
		public const string Player_HasUnlockedDoomsayer = "Player_HasUnlockedDoomsayer";

		//Chronologist
		public const string buddy_chronologist_name = "Tyme Weaver";
		public const string buddy_chronologist_title = "The Chronologist";
		public const string Buddy_Chronologist_Level = "Buddy_Chronologist_Level";
		public const string Buddy_Chronologist_CameraSlowdownPercentage = "Buddy_Chronologist_CameraSlowdownPercentage";
		public const string Buddy_Chronologist_CameraSlowdownTime = "Buddy_Chronologist_CameraSlowdownTime";
		public const string Buddy_Chronologist_RelativePlayerSpeed = "Buddy_Chronologist_RelativePlayerSpeed";

		//Rocker
		public const string buddy_rocker_name = "Oswald Oswaldian";
		public const string buddy_rocker_title = "The Rocker";
		public const string Buddy_Rocker_Level = "Buddy_Rocker_Level";
		public const string Buddy_Rocker_CarsDoCollide = "Buddy_Rocker_CarsDoCollide";
		public const string Buddy_Rocker_CarsCollisionForceMultiplier = "Buddy_Rocker_CarsCollisionForceMultiplier";
		public const string Buddy_Rocker_HasShieldUpgrade = "Buddy_Rocker_HasShieldUpgrade";

		//Radiologist
		public const string buddy_radiologist_name = "Xavier Rayson";
		public const string buddy_radiologist_title = "The Radiologist";
		public const string Buddy_Radiologist_Level = "Buddy_Radiologist_Level";
		public const string Buddy_Radiologist_XRayTime = "Buddy_Radiologist_XRayTime";
		public const string Buddy_Radiologist_HasNightVision= "Buddy_Radiologist_HasNightVision";

		//Pilferer
		public const string buddy_pilferer_name = "Kat Bourghlier";
		public const string buddy_pilferer_title = "The Pilferer";
		public const string Buddy_Pilferer_Level = "Buddy_Pilferer_Level";
		public const string Buddy_Pilferer_DayCoinBonus = "Buddy_Pilferer_DayCoinBonus";
		public const string Buddy_Pilferer_NightCoinBonus = "Buddy_Pilferer_NightCoinBonus";
		public const string Buddy_Pilferer_CanMarkCarWithTreasure = "Buddy_Pilferer_CanMarkCarWithTreasure";

		//Medium
		public const string buddy_medium_name = "Elma Rutabaga";
		public const string buddy_medium_title = "The Medium";
		public const string Buddy_Medium_Level = "Buddy_Medium_Level";
		public const string Buddy_Medium_AdditionalCrystalFrequency = "Buddy_Medium_AdditionalCrystalFrequency";
		public const string Buddy_Medium_CrystalDurationBonus = "Buddy_Medium_CrystalDurationBonus";
		public const string Buddy_Medium_IsInvulnerableDuringCrystal = "Buddy_Medium_IsInvulnerableDuringCrystal";

		//Sidewinder
		public const string buddy_sidewinder_name = "Sylvester Cwique";
		public const string buddy_sidewinder_title = "The Sidewinder";
		public const string Buddy_Sidewinder_Level = "Buddy_Sidewinder_Level";
		public const string Buddy_Sidewinder_LaneChangeSpeed = "Buddy_Sidewinder_LaneChangeSpeed";
		public const string Buddy_Sidewinder_HasTeleport = "Buddy_Sidewinder_HasTeleport";

		//Diviner
		public const string buddy_diviner_name = "Wendall Wheiyrd";
		public const string buddy_diviner_title = "The Diviner";
		public const string Buddy_Diviner_Level = "Buddy_Diviner_Level";
		public const string Buddy_Diviner_ShieldDuration = "Buddy_Diviner_ShieldDuration";
		public const string Buddy_Diviner_HasDestroyAllCarsOnDamage = "Buddy_Diviner_HasDestroyAllCarsOnDamage";

		//Mechanic
		public const string buddy_mechanic_name = "Leekay Sizzle";
		public const string buddy_mechanic_title = "The Mechanic";
		public const string Buddy_Mechanic_Level = "Buddy_Mechanic_Level";
		public const string Buddy_Mechanic_ExtraHealth = "Buddy_Mechanic_ExtraHealth";
		public const string Buddy_Mechanic_AdditionalHealthSpawnFrequency = "Buddy_Mechanic_AdditionalHealthSpawnFrequency";
		public const string Buddy_Mechanic_CanActivateSavior = "Buddy_Mechanic_CanActivateSavior";

		//Jester
		public const string buddy_jester_name = "Heather Ledge";
		public const string buddy_jester_title = "The Jester";
		public const string Buddy_Jester_Level = "Buddy_Jester_Level";
		public const string Buddy_Jester_LaneChangeFrequency = "Buddy_Jester_LaneChangeFrequency";
		public const string Buddy_Jester_CameraChangeRefillBonus = "Buddy_Jester_CameraChangeRefillBonus";
		public const string Buddy_Jester_CanIgnoreDamage = "Buddy_Jester_CanIgnoreDamage";

		//Doomsayer
		public const string buddy_doomsayer_name = "Carh McGeddin";
		public const string buddy_doomsayer_title = "The Doomsayer";
		public const string Buddy_Doomsayer_Level = "Buddy_Doomsayer_Level";
		public const string Buddy_Doomsayer_SpeechBubbleFrequency = "Buddy_Doomsayer_SpeechBubbleFrequency";
		public const string Buddy_Doomsayer_BadSpeechBubbleSize = "Buddy_Doomsayer_BadSpeechBubbleSize";
		public const string Buddy_Doomsayer_GoodSpeechBubbleSize = "Buddy_Doomsayer_GoodSpeechBubbleSize";
		public const string Buddy_Doomsayer_CanEnterBubbleBlast = "Buddy_Doomsayer_CanEnterBubbleBlast";
	}
}

