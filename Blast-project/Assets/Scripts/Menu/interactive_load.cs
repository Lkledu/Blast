using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactive_load : MonoBehaviour
{
    private GameObject bombPref;
    private granada_shoot bombInstance;

    void Start()
    {
        bombPref = Resources.Load("granada_shoot") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button5) ||
            Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Joystick2Button5))
        {
            Vector3 origin = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f,1.0f), 1, Camera.main.nearClipPlane+10));
            bombInstance = Instantiate(bombPref, origin, Quaternion.identity).GetComponent<granada_shoot>();
            bombInstance.fuseTime = 2f;
        }
    }
}
