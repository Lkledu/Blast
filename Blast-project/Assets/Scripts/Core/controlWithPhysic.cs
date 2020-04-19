using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlWithPhysic : MonoBehaviour
{
    public int id = 2;
    Transform player;

    private void Start()
    {
        player = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float horizontal = -Input.GetAxis("Horizontal_p" + id);
        float vertical = Input.GetAxis("Vertical_p" + id);

        player.Translate(new Vector3(horizontal * 5 * Time.deltaTime, 0, vertical * 5 * Time.deltaTime), Space.World);
        //player.Rotate(new Vector3(0, Input.GetAxis("rotation_p" + id) * 200.0f * Time.deltaTime, 0), Space.Self);
        
        if (horizontal != 0 || vertical != 0) {
            player.localRotation = Quaternion.LookRotation(new Vector3(horizontal, 0, vertical));
        }
        

    }
}