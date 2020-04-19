using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pause_menu : MonoBehaviour {

    public GameObject pause_panel;
    public EventSystem evSystem;
    public GameObject firstSelectObj;

	void Start () {
        pause_panel.SetActive(false);
	}

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && pause_panel.activeSelf == false) {
            Time.timeScale = 0;
            pause_panel.SetActive(true);
            evSystem.SetSelectedGameObject(firstSelectObj);
        } else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && pause_panel.activeSelf == true) {
            pause_panel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume() {
        pause_panel.SetActive(false);
        Time.timeScale = 1;
    }

}
