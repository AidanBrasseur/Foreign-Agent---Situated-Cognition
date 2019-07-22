using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAlignToGround : MonoBehaviour
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
    [HideInInspector]
    public bool dashStart = false;
    private float sliderTimer = 0;
    public float regenRate = 0.5f;
    public GameObject parent;
    private RaycastHit hit;
    private Vector3 pos;
    public Transform raycastPoint;
    private bool firstFrame = true;
    private Vector3 offset;
    private Vector3 myDirection;
    Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
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
        
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsRunning", dashStart);
        //Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        //m_Rotation = Quaternion.LookRotation(desiredForward, parent.transform.up);
       // pos = parent.transform.position;
       
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        Physics.Raycast(raycastPoint.position, -parent.transform.up, out hit);
        parent.transform.up -= (parent.transform.up - hit.normal) * 0.1f;
        offset = hit.point + hit.normal * 0.1f;// new Vector3(pos.x, hit.point.y + 0.05f, pos.z);
       // Debug.Log(hit.point);
        Vector3 temp = Vector3.Cross(hit.normal, m_Movement);
        myDirection = Vector3.Cross(temp, hit.normal);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, myDirection, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward, parent.transform.up);
        transform.rotation = m_Rotation;
        // Debug.Log(hit.normal);


    }
    void OnAnimatorMove()
    {
        Debug.Log(offset);
        parent.transform.position -= (parent.transform.position - offset) * 0.05f;
        //Debug.Log(offset);
        // m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * moveSpeed * m_Animator.deltaPosition.magnitude);
        parent.transform.position += (myDirection * moveSpeed * m_Animator.deltaPosition.magnitude);
        //m_Rigidbody.MoveRotation(m_Rotation);
        // transform.rotation = m_Rotation;
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            slider.gameObject.SetActive(true);
            sliderTimer = 0;
            dashStart = true;
        }
        //if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        //{
        //    slider.gameObject.SetActive(true);
        //    sliderTimer = 0;
        //    dashStart = true;

        //}
        else if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) || (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0))
        {
            moveSpeed = originalSpeed;
            dashStart = false;
        }
        if (dash <= 0)
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


        if (GameController.Instance.numCaptures == GameController.Instance.numCellsInLevel)
        {
            GetComponent<Collider>().enabled = false;
            slider.gameObject.SetActive(false);
            this.enabled = false;
            Debug.Log("slider");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "companion" || other.gameObject.tag == "Tcell")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }

}

