using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIndication : MonoBehaviour {

    public Transform transformP;
    Vector3 offset;
    private void Start()
    {
        offset = new Vector3(0,1,1);
    }

    void LateUpdate()
    {
        transform.position = transformP.position + offset;
    }
}
