﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float moveSpeed = 0.05f;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    private float originalSpeed;
    public float maxDash = 2f;
    private float dash = 2f;
    public float dashSpeed;
    public Slider slider;
    Quaternion fixedRotation;
    private bool dashStart = false;
    private float sliderTimer = 0;
    public float regenRate = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        dash = maxDash;
        originalSpeed = moveSpeed;
        fixedRotation = slider.GetComponentInParent<Canvas>().transform.rotation;
    }
    private void LateUpdate()
    {
        slider.GetComponentInParent<Canvas>().transform.rotation = fixedRotation;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * moveSpeed);
        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;

        //if (dash > 0 && Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    dash -= Time.deltaTime;
        //}
        //else
        //{
        //    //dash += Time.deltaTime;
        //}
        //dash = Mathf.Clamp(dash, 0, maxDash);
        //slider.value = (maxDash - dash) /maxDash;

    }
    private void Update()
    {

        if (slider.value == 1)
        {
            sliderTimer += Time.deltaTime;
            if (sliderTimer > 1)
            {
                slider.gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            slider.gameObject.SetActive(true);
            sliderTimer = 0;
            dashStart = true;
           
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            moveSpeed = originalSpeed;
            dashStart = false;
        }
        else if (dash <= 0)
        {
            dashStart = false;
        }
        if (dashStart)
        {
            dash -= Time.deltaTime;
            moveSpeed = dashSpeed;
            if (dash <= 0)
            {
                moveSpeed = originalSpeed;
            }
        }
        else
        {
            if (dash < maxDash)
            {
                dash += regenRate * Time.deltaTime;
                if (dash > maxDash)
                {
                    dash = maxDash;
                }
            }
        }
        float complete = 1 - (maxDash - dash) / maxDash;
        slider.value = complete;



    }

}
