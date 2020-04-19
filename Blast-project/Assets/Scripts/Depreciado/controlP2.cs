using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlP2 : MonoBehaviour {

    public float speed = 6.0f;
    public float gravity = -10.0f;
    public CharacterController _charCont;

    void Start()
    {
        _charCont = GetComponent<CharacterController>();
    }

    void Update () {
        float _deltaX = Input.GetAxis("Vertical_p2") * speed;
        float _deltaY = Input.GetAxis("Horizontal_p2") * speed;

        Vector3 movement = new Vector3(0, 0, _deltaX);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);
		
		transform.Rotate(0, _deltaY * 20 * Time.deltaTime, 0);

    }
	
	private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position,transform.position + this.transform.forward * 3);
    }
}
