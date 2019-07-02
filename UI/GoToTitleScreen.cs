using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour {

	public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
