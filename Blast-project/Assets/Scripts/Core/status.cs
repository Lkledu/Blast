using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status : MonoBehaviour {
    
    public float life;
    public Transform spawn_position;
    public RectTransform rectLife;
    public win_checker deathSendTrigger;
    public string rival;
    public RectTransform wIcon;
    public Image wIconImg;
    
    public bool player = true;
    string weaponCatched;

    public Sprite spriteP, spriteB, spriteD;

    public float m_EnTime = 5.0f;
    bool animState = false;
    bool m_complete = false;
    

    void Start () {
        life = 300;
        transform.position = spawn_position.position;
        transform.rotation = spawn_position.rotation;
    }

    private void Update()
    {
        rectLife.localScale = new Vector3(life * 0.01f, 1, 1);
        if ((this.life <= 0) || (this.transform.position.y <= -8.0f)) {
            deathSendTrigger.deathTrigger(rival, true);
        }

        if (animState) {
            wIcon.anchoredPosition = Vector2.Lerp(wIcon.anchoredPosition, new Vector2(0, 0), Time.deltaTime / m_EnTime);
            m_complete = Vector2.Distance(wIcon.anchoredPosition, new Vector2(0, 0)) < 0.1f;
            if (m_complete)animState = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spawn") {
            weaponCatched = other.GetComponent<spawn_weapon>().weapon.ToString();
            switch (weaponCatched) {
                case "pistol":
                    wIconImg.sprite = spriteP;
                break;
                case "granada":
                    wIconImg.sprite = spriteD;
                break;
                case "bazooka":
                    wIconImg.sprite = spriteB;
                break;
                default:
                    Debug.LogError("sprite not found for weapon icon. " + weaponCatched);
                break;
            }

            if (player){
                wIcon.anchoredPosition = new Vector2(200, 0);
            }
            else {
                wIcon.anchoredPosition = new Vector2(-200, 0);
            }
            animState = true;
        }
    }
}
