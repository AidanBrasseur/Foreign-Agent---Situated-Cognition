using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatingEnding : MonoBehaviour
{
    public GameObject zoomedOutCam;
    public Animator m_Animator;
    public GameObject sandwich;
    public Mesh bitten;
    public GameObject player;
    public GameObject loadingScreen;
    void OnTriggerEnter()
    {
        StartCoroutine(cameraPan());
    }
    IEnumerator cameraPan()
    {
        zoomedOutCam.SetActive(true);
        yield return new WaitForSeconds(2);
        //player.SetActive(false);
        m_Animator.SetBool("isEating", true);
        yield return new WaitForSeconds(1.6f);
        sandwich.GetComponent<MeshFilter>().sharedMesh = bitten;
        yield return new WaitForSeconds(1f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        loadingScreen.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(sceneIndex));



    }

}
