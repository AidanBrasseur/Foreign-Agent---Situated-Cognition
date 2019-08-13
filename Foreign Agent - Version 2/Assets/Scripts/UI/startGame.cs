using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startGame : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void NextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingScreen.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(sceneIndex));
    }
    public void ToggleLevelSelect()
    {
        bool level = levelSelect.activeInHierarchy;
        levelSelect.SetActive(!level);
        bool main = mainMenu.activeInHierarchy;
        mainMenu.SetActive(!main);
    }
    public void loadLevel(int index)
    {
        loadingScreen.GetComponent<LoadingScreen>().enableQuiz = false;
        loadingScreen.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(index));
        levelSelect.SetActive(false);
    }
}
