using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";
	
	public static void Initialize () {
		PlayerPrefs.DeleteAll();
	}
	
	// ------------------------ Volume ------------------------------------
	public static void SetMasterVolume (float volume) {
		if (volume >= 0f && volume <= 1f) {
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master volume '"+volume+"' out of range");
		}
	}
	
	public static float GetMasterVolume() {
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}
	// ------------------------ End Volume ---------------------------------
	
	
	// ------------------------ Unlocks ------------------------------------
	public static void UnlockLevel (int level) {
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			PlayerPrefs.SetInt (LEVEL_KEY + level.ToString (), 1); // Use 1 for true
		} else {
		 	Debug.LogError ("Trying to unlock a level that is not in the build order");
		}
	}
	
	public static bool IsLevelUnlocked (int level) {
		if (level <= SceneManager.sceneCountInBuildSettings - 1) {
			int lockStatus = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString ());
			return (lockStatus == 1); 
			
/*			{
				return true;
			} else {
				return false;
			}	
*/
		} else {
			Debug.LogError ("Asking about a level that is not in the build order");
			return false;
		}
	}
	// ------------------------ End Unlocks ---------------------------------
	
	
	// ------------------------ Difficulty ----------------------------------
	public static void SetDifficulty (float difficulty) { 
		if (difficulty >= 1f && difficulty <= 3f) {
			PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError ("Difficulty '"+difficulty+"' out of range");
		}
	}
	
	public static float GetDifficulty() {
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}
	// ------------------------ End Difficulty -------------------------------
	
	
	
}
