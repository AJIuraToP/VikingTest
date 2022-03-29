using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private Vector3 _vector3;
    public float rotateSpeed;
    float rotY = 0.0f;
    float rotX = 0.0f;

    public Transform player;

    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y + 1f, player.postion.z);
        transform.position = Vector3.Lerp(gameObject.transform.position, player.position, rotateSpeed * Time.deltaTime);
        //gameObject.transform.rotation = Quaternion.Euler(0f*Time.deltaTime,0f*Time.deltaTime,0f*Time.deltaTime);
        CameraRotate();
    }

    public void CameraRotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotY += rotateSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotY -= rotateSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow)&& rotX < 45f)
        {
            rotX += rotateSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.DownArrow)&& rotX > -25f)
        {
            rotX -= rotateSpeed * Time.deltaTime;
        }
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localRotation;
    }
}


