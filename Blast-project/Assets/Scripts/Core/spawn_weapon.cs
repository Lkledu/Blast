using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_weapon : MonoBehaviour {

    public enum weaponType {pistol, granada, bazooka };

    public weaponType weapon;
    public GameObject objWeapon;
    GameObject meshInstance;

    public float respawnTime = 0;
    
    private void Start()
    {
        meshInstance = Resources.Load(weapon + "_pref") as GameObject;
        GameObject instance = Instantiate(meshInstance);
        objWeapon.GetComponent<MeshFilter>().mesh = instance.GetComponent<MeshFilter>().mesh;
        objWeapon.transform.localScale = instance.GetComponent<Transform>().transform.localScale;

        Destroy(instance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            objWeapon.SetActive(false);
            respawnTime = 10f;
            GetComponent<Collider>().enabled = false;
        }
    }

    private void Update(){

        if (respawnTime > 0){
            respawnTime -= Time.deltaTime;
        }
        else {
            if(objWeapon.activeSelf == false) {
                objWeapon.SetActive(true);
                GetComponent<Collider>().enabled = true;
            }
        }
    }
}
