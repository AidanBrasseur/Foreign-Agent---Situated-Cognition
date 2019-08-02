using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class playGame : MonoBehaviour
{
    public GameObject boy;
    public GameObject player;
    public Image fade;
    public GameObject playerCam;
    private Animator m_Animator;
    private Color origColor;
    public GameObject prevMenu;
    public GameObject UI;
    public GameObject grey;
    public GameObject minimap;
    private AudioSource cough;

    public void Start()
    {
        m_Animator = boy.GetComponent<Animator>();
        cough = boy.transform.Find("Cough").gameObject.GetComponent<AudioSource>();
        origColor = fade.color;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void NextLevel(GameObject loadingScreen)
    {
        Time.timeScale = 1.0f;
        player.GetComponent<AudioListener>().enabled = false;
        boy.GetComponent<AudioListener>().enabled = true;
        StartCoroutine(fadeInandOut(loadingScreen));
    }

    IEnumerator fadeInandOut(GameObject loadingScreen)
    {
        if(grey != null)
            grey.SetActive(false);
        UI.SetActive(false);
        minimap.SetActive(false);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }
        float t = 0f;
        Color newColor = origColor;
        while (t < 1f)
        {
            newColor.a = Mathf.Lerp(0f, 1f, t);
            fade.color = newColor;
            t += Time.deltaTime;
            yield return null;
        }
        m_Animator.SetBool("isSneezing", true);
        cough.Play();
        t = 0f;
        playerCam.SetActive(false);
        Color newColor2 = newColor;
        while (t < 1f)
        {
            newColor2.a = Mathf.Lerp(1f, 0f, t);
            fade.color = newColor2;
            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        cough.Play();
        yield return new WaitForSeconds(1f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingScreen.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(sceneIndex));

    } 
}
