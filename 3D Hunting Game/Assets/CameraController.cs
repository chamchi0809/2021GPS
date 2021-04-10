using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotSpeed = 200f;
    float camRotX;
    float camRotY;

    public Vector3 offset;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        camRotX = Camera.main.transform.eulerAngles.x;
        camRotY = Camera.main.transform.eulerAngles.y;

        //offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        camRotX += h * Time.deltaTime * rotSpeed;
        camRotY += v * Time.deltaTime * rotSpeed;

        camRotY = Mathf.Clamp(camRotY, -45, 45);
        //camRotX = Mathf.Clamp(camRotX, -45, 45);

        transform.eulerAngles = new Vector3(-camRotY, camRotX, 0);
        target.root.eulerAngles = new Vector3(0, camRotX, 0);
    }
    private void LateUpdate()
    {
        transform.position = target.position + target.TransformDirection(offset);
    }
}
