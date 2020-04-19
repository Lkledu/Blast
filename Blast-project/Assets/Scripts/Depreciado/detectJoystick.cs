using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectJoystick : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.Joystick1Button0)){
            Debug.Log("A");
        }


        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Debug.Log("B");
        }

        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Debug.Log("X");
        }

        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Debug.Log("Y");
        }

        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            Debug.Log("LB");
        }

        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Debug.Log("RB");
        }

        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            Debug.Log("Back");
        }

        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Debug.Log("Start");
        }

        if (Input.GetKey(KeyCode.Joystick1Button8))
        {
            Debug.Log("LStick");
        }

        if (Input.GetKey(KeyCode.Joystick1Button9))
        {
            Debug.Log("RStick");
        }

    }
}
