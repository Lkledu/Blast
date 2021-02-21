using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pause_menu : MonoBehaviour {

    public GameObject pause_panel;
    public EventSystem evSystem;
    public GameObject firstSelectObj;
    private RectTransform rect;
    public bool scaleFlag = false;
    
    [Range(0.1f,10.0f)]
    public float vel = 1.0f;
	void Start () {
        pause_panel.SetActive(false);
        rect = pause_panel.GetComponent<RectTransform>();

    }

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && pause_panel.activeSelf == false) {
            Time.timeScale = 0;
            pause_panel.SetActive(true);
            scaleFlag = true;
            evSystem.SetSelectedGameObject(firstSelectObj);
        } else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && pause_panel.activeSelf == true) {

            scaleFlag = false;
            Time.timeScale = 1;
        }

        if (scaleFlag && rect.localScale.x <= 1) {
            rect.localScale += new Vector3(0.1f, 0.1f, 0);
        } else if (!scaleFlag && rect.localScale.x >= 0) {
            rect.localScale -= new Vector3(0.1f, 0.1f, 0);
            if (rect.localScale.x <= 0) { pause_panel.SetActive(false); }
        }
    }

    public void Resume() {
        pause_panel.SetActive(false);
        Time.timeScale = 1;
    }

}
