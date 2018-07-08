using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene() {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    public void LoadMainMenu() {

        SceneManager.LoadScene(0);

    }

    public void LoadSettingsMenu() {

        SceneManager.LoadScene(1);

    }

    public void LoadGameScreen() {

        SceneManager.LoadScene(2);

    }

    public void QuitGame() {

        Application.Quit();
        Debug.Log("Quitting Application");

    }

}
