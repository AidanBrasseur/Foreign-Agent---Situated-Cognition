using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlignToGround : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 pos;
    public Transform raycastPoint;
    private bool firstFrame = true;
    // Update is called once per frame
    void Update()
    {

        pos = transform.position;


        if (firstFrame)
        {
            StartCoroutine(waitTillFirstFrame());
        }
        else
        {
            Physics.Raycast(raycastPoint.position, Vector3.down, out hit);
            transform.up -= (transform.up - hit.normal) * 0.1f;
            Debug.Log(hit.point);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, hit.point.y + 0.5f, pos.z), 20 * Time.deltaTime);
        }
    }
    IEnumerator waitTillFirstFrame()
    {
        firstFrame = false;
        yield return null;
        Physics.Raycast(raycastPoint.position, Vector3.down, out hit);
        transform.up -= (transform.up - hit.normal) * 0.1f;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, hit.point.y + 0.5f, pos.z), 20 * Time.deltaTime);
    }
}
