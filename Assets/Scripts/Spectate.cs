using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectate : MonoBehaviour
{
    Rigidbody cc;
    void Start()
    {
        cc = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            cc.velocity = transform.rotation * Vector3.forward * 10;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            cc.velocity = transform.rotation * Vector3.left * 10;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cc.velocity = transform.rotation * Vector3.back * 10;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cc.velocity = transform.rotation * Vector3.right * 10;
        }
        else
        {
            cc.velocity = new Vector3(0, 0, 0);
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y"), 0 , 0);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
