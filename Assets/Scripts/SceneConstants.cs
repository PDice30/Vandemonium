using System.Collections;

namespace UnityEngine {
	public class SceneConstants {
		public const float PLAYER_VELOCITY = 1.0f;
		public const float PLAYER_VELOCITY_MULTIPLYER = 1.0f;

		public const float BASE_SCENE_SPEED = 3.0f;

		public const float BASIC_CAR_SPAWN_TIME = 1.5f;
		public const float BASIC_BRIDGE_SPAWN_TIME = 90.0f;
		public const float BASIC_TERRAIN_SPAWN_TIME = 1.0f;
		public const int TERRAIN_LEFT_SIDE_ONLY = 0;

		public const float BASE_CAR_VELOCITY = 1.0f;
		//Other car velocities
		public const float BASE_OBJECT_VELOCITY = 1.0f;


		public const float OBJECT_SPAWN_POSITION = 30.0f;


		public const float XFORCE_COLLISION_MIN = 200f;
		public const float XFORCE_COLLISION_MAX = 600f;
		public const float YFORCE_COLLISION_MIN = 200f;
		public const float YFORCE_COLLISION_MAX = 400f;
		public const float ZFORCE_COLLISION_MIN = 600f;
		public const float ZFORCE_COLLISION_MAX = 1000f;
		//Should be Set to 1f or removed for Release
		public const float FORCE_TEST_MULTIPLIER = 1f;


		public const float DESTROY_OBJECT_POSITION = -20f; // z position behind car

		public const float TIME_UNTIL_DESTROY_CAR = 2f;

		public const int NUMBER_OF_PLAYER_BUDDIES = 10;


		//// Coins ////
		public const float BASE_COIN_SPAWN_TIME = 5.0f;
		public const float BASE_COIN_VELOCITY = 1.0f;
		public const int MIN_COIN_RUN = 7;
		public const int MAX_COIN_RUN = 15;
		public const int COIN_RUN_CHANCE = 30;


		//// Player Settings ////
		public const int DEFAULT_PLAYER_HEALTH = 10;




	}
}
		

