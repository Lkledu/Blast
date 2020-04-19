using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazooka : MonoBehaviour {
    int maxShoots = 10;
    float velocity = 15;
    GameObject prefab_shoot;
    GameObject prefab_weapon;
    GameObject weapon_child;

    private void Start()
    {
        prefab_shoot = Resources.Load("bazooka_shoot") as GameObject;
        prefab_weapon = Resources.Load("bazooka_pref") as GameObject;
        weapon_child = GameObject.Find("Weapon");
        
        weapon_child = Instantiate(prefab_weapon, gameObject.transform.Find("Weapon"));
        weapon_child.GetComponent<Transform>().localScale = new Vector3(100, 100, 300);
        weapon_child.GetComponent<Transform>().localRotation = Quaternion.Euler(0,-90,120);
    }
	
	void Update () {
        if ((gameObject.name == "Player1" && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button5))) ||
            (gameObject.name == "Player2" && (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Joystick2Button5))))
        {
            maxShoots--;

            GameObject projectile_pistol = Instantiate(prefab_shoot) as GameObject;
            projectile_pistol.transform.position = transform.position + transform.forward;

            Rigidbody rb = projectile_pistol.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * velocity;

        }


        if (maxShoots == 0)
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        Destroy(weapon_child);
    }
}
