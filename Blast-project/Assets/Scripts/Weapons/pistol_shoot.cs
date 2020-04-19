using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol_shoot : MonoBehaviour {

    public Transform shooter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            other.GetComponent<status>().life -= 10;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        if (other.tag != "Spawn")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(shooter.position, this.transform.position);
    }
}
