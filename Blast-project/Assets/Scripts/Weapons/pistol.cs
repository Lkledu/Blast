using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : MonoBehaviour {

    GameObject prefab_shoot;
    public float velocity = 20;
    int maxShoots = 20;

    GameObject prefab_weapon;
    GameObject weapon_child;

    public Transform muzzleFlashEffect;
    private GameObject particleVFX;

    void Start () {
        prefab_shoot = Resources.Load("pistol_shoot") as GameObject;
        prefab_weapon = Resources.Load("pistol_pref") as GameObject;
        weapon_child = GameObject.Find("Weapon");

        weapon_child = Instantiate(prefab_weapon, gameObject.transform.Find("Weapon"));
        weapon_child.GetComponent<Transform>().localScale = new Vector3(40, 15, 30);

        muzzleFlashEffect = weapon_child.GetComponentInChildren<Transform>().Find("Muzzle");
        muzzleFlashEffect.gameObject.SetActive(false);

        particleVFX = Resources.Load("particle/bangEffect") as GameObject;

        gameObject.GetComponent<status>().wIconImg.color = Color.white;

    }

    void Update () {
        if ((gameObject.name == "Player1" && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick1Button5))) || 
            (gameObject.name == "Player2" && (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Joystick2Button5))))
        {
            maxShoots--;
            StartCoroutine("MuzzleFlash");

            GameObject projectile_pistol = Instantiate(prefab_shoot) as GameObject;
            projectile_pistol.transform.position = transform.position + transform.forward;

            Rigidbody rb = projectile_pistol.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * velocity;

            projectile_pistol.GetComponent<pistol_shoot>().shooter = transform;
        }

        if (maxShoots == 0)
        {
            gameObject.GetComponent<status>().wIconImg.color = Color.clear;
            Destroy(this);
        }
	}

    private void OnDestroy()
    {
        Destroy(weapon_child);
    }


   
    private IEnumerator MuzzleFlash() {
        muzzleFlashEffect.transform.Rotate(new Vector3(Random.Range(0,60),0,0),Space.Self);
        muzzleFlashEffect.gameObject.SetActive(true);
        Instantiate(particleVFX, transform.position, particleVFX.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        muzzleFlashEffect.gameObject.SetActive(false);
    }

}
