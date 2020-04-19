using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    bool btnA = Input.GetKey(KeyCode.Joystick1Button0);
    bool btnB = Input.GetKey(KeyCode.Joystick1Button1);
    bool btnX = Input.GetKey(KeyCode.Joystick1Button2);
    bool btnY = Input.GetKey(KeyCode.Joystick1Button3);
    bool btnLB = Input.GetKey(KeyCode.Joystick1Button4);
    bool btnRB = Input.GetKey(KeyCode.Joystick1Button5);
    bool btnBack = Input.GetKey(KeyCode.Joystick1Button6);
    bool btnStart = Input.GetKey(KeyCode.Joystick1Button7);
    
    public bool fireBtn() {
        bool btnFire = false;

        if (btnA || btnB || btnX || btnY || btnLB || btnRB)
        {
            btnFire = true;

            Debug.Log("shoot");
        }
        return btnFire;
    }

}
