using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class win_checker : MonoBehaviour {
    string player;
    bool trigger;
    

    public GameObject win_Panel;
    public TextMeshProUGUI win_Text;

    public EventSystem evSystem;
    public GameObject firstSelectedObj;

    private void Start()
    {
        win_Panel.SetActive(false);
    }

    private void Update()
    {
       if (trigger && win_Panel.activeSelf == false)
        {
            win_Text.text = player + " WIN";
            Time.timeScale = 0;
            win_Panel.SetActive(true);
            evSystem.SetSelectedGameObject(firstSelectedObj);
        }
    }

    public void deathTrigger(string playerName, bool triggerReceived) {
        trigger = triggerReceived;
        player = playerName;
    }
}
