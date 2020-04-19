using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status : MonoBehaviour {
    
    public float life;
    public Transform spawn_position;
    public RectTransform rectLife;
    string weaponCatched;
    public win_checker deathSendTrigger;
    public string rival;

    void Start () {
        life = 300;
        transform.position = spawn_position.position;
        transform.rotation = spawn_position.rotation;
    }

    private void Update()
    {
        rectLife.localScale = new Vector3(life * 0.01f, 1, 1);
        if ((this.life == 0) || (this.transform.position.y <= -8.0f)) {
            deathSendTrigger.deathTrigger(rival, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spawn") {weaponCatched = other.GetComponent<spawn_weapon>().weapon.ToString(); }

        //if (other.tag == "Damage") { life -= 30; }
    }
}
