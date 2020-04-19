using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load_weapon_script : MonoBehaviour {

    public string lastWeapon;
    
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Spawn") {
            if (lastWeapon != ""){
                if (GetComponent(System.Type.GetType(lastWeapon))){
                    Destroy(GetComponent(System.Type.GetType(lastWeapon)));
                }
            }
            gameObject.AddComponent(System.Type.GetType(other.GetComponent<spawn_weapon>().weapon.ToString()));
            lastWeapon = other.GetComponent<spawn_weapon>().weapon.ToString();
        }
    }
}
