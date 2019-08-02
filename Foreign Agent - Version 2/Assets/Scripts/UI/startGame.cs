﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void NextLevel(GameObject loadingScreen)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingScreen.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(sceneIndex));
    }
}
