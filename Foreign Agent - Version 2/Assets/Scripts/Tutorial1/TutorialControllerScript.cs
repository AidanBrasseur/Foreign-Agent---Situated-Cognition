using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class TutorialControllerScript : MonoBehaviour
{

    public GameObject player;
    public Image wasd;
    public Image shift;
    private bool endPlayed = false;
    public GameObject pointer;
    private Animator m_Animator;
	private companionSpawn companionScript;
	public Image grayScreen;
    private bool firstCameraPan = false;
    public GameObject zoomedOutCam;
    public RPGTalk IntroTalk;
    void Start()
    {
        m_Animator = player.GetComponent<Animator>();
		companionScript = player.GetComponent<companionSpawn>();
		grayScreen.enabled = true;
    }
    public void CancelControls()
    {
        player.GetComponent<PlayerAlignToGround>().dashStart = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAlignToGround>().enabled = false;
        player.GetComponent<companionSpawn>().enabled = false;
      
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsRunning", false);
		grayScreen.enabled = true;
    }

    //give back the controls to player
    public void GiveBackControls()
    {
        player.GetComponent<PlayerAlignToGround>().enabled = true;
        player.GetComponent<companionSpawn>().enabled = true;
		grayScreen.enabled = false;
	}

    public void DisplayWasd()
    {
        wasd.gameObject.SetActive(true);
    }
    public void DisplayShift()
    {
        shift.gameObject.SetActive(true);
    }
    public void HideShift()
    {
        shift.gameObject.SetActive(false);
    }
    public void HideWasd()
    {
        wasd.gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        if(!firstCameraPan)
        {
            StartCoroutine(cameraPan());
            firstCameraPan = true;
        }
       
    }
    
    public void flashPointer()
    {
        if (pointer.activeSelf)
            pointer.SetActive(false);
        else
            pointer.SetActive(true);
    }
    public void displayPointer1()
    {
        InvokeRepeating("flashPointer", 0.0f, 0.75f);
    }
    public void deactivatepointer(GameObject pointer)
    {
        CancelInvoke();
        pointer.SetActive(false);
        pointer.transform.position = new Vector3(289f, -324.3f, -752.92f);

    }
    public void deactivatepointer2(GameObject pointer)
    {
        CancelInvoke();
        pointer.SetActive(false);
        pointer.transform.position = new Vector3(293.8638f, -324.76f, -750.34f);
        pointer.transform.eulerAngles = new Vector3(0, 164.855f, -38.212f);

    }

    public void hidePointer1()
    {
        deactivatepointer(pointer);
    }
   public void hidePointer2()
    {
        deactivatepointer2(pointer);
    }

    IEnumerator cameraPan()
    {
        CancelControls();
        grayScreen.enabled = false;
        yield return new WaitForSeconds(2);
        zoomedOutCam.SetActive(false);
        yield return new WaitForSeconds(2);
        grayScreen.enabled = true;
        IntroTalk.NewTalk("IntroTalkStart", "IntroTalkEnd", IntroTalk.txtToParse);
    }

}

