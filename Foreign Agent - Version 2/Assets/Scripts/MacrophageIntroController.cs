﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class MacrophageIntroController : MonoBehaviour
{
	public GameObject player;
	public RPGTalk introTalk;
	public RPGTalk endTalk;
	private Animator m_Animator;

	public Image grayScreen;

	private bool endPlayed = false;
	public GameObject cell;
	public GameObject endMenu;
	public AudioSource victorySound;
	void Start()
	{
		m_Animator = player.GetComponent<Animator>();
		grayScreen.enabled = true;
	}
	public void CancelControls()
	{
		player.GetComponent<PlayerMovement>().dashStart = false;
		player.GetComponent<PlayerMovement>().enabled = false;
		player.GetComponent<companionSpawn>().enabled = false;

		m_Animator.SetBool("IsWalking", false);
		m_Animator.SetBool("IsRunning", false);
		grayScreen.enabled = true;
	}

	//give back the controls to player
	public void GiveBackControls()
	{
		player.GetComponent<PlayerMovement>().enabled = true;
		player.GetComponent<companionSpawn>().enabled = true;
		grayScreen.enabled = false;
	}
	void LateUpdate()
	{
		if (!endPlayed && cell.GetComponentInChildren<CellCapture>().capped)
		{
			CancelControls();
			endMenu.SetActive(false);
			Time.timeScale = 1.0f;
			endTalk.NewTalk("endLevelStart", "endLevelEnd", endTalk.txtToParse);
			endPlayed = true;
		}
	}
	public void activateEndMenu()
	{
		victorySound.Play();
		endMenu.SetActive(true);
		Time.timeScale = 0f;
	}
}



