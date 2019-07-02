 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScreen : MonoBehaviour {
    public GameObject help;



    public void startGameBtn(string mainLevel)
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void helpMenu()
    {
        help.SetActive(true);
    }
    public void helpMenuOff()
    {
        help.SetActive(false);
    }
}
