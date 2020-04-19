using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazooka_shoot : MonoBehaviour {

    public float radiusDamage = 5f;
    public float power = 10f;
    private GameObject particleExplosion;

    private void Start()
    {
        particleExplosion = Resources.Load("particle/explosion") as GameObject;
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Player")
            {
                hitColliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                hitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(power, center, radius, 1, ForceMode.Impulse);
                hitColliders[i].GetComponent<status>().life -= 30;
                hitColliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Spawn")
        {
            ExplosionDamage(this.transform.position, radiusDamage);
            Instantiate(particleExplosion, transform.position, particleExplosion.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, radiusDamage);
    }
}
