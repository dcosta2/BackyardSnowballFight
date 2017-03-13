using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
    void Start (){
		if (autoLoadNextLevelAfter <= 0) {
//			Debug.Log ("Level auto load disabled, use a positive Number in seconds");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}
	
	public void LoadLevel(string name) {
        // Debug.Log ("Level load requested for: "+name);
        SceneManager.LoadScene(name);
	}
	
	public void QuitRequest () {
		// Debug.Log ("Quiting now");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
