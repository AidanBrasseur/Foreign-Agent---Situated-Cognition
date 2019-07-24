using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCellMoveForward : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.Translate(Vector3.forward * 0.4f * Time.deltaTime);
        }
    }
}
