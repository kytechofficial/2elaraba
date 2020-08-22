using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{

    public GameObject car;
    public float uzaklik, yukseklik, dampening;
    public float MouseSensitivity = 100.0f,maxAltAci;
    float m_VerticalAngle, m_HorizontalAngle;
    public Transform CameraPosition;
    public GameObject Merkez;

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;
    // Start is called before the first frame update
    [Header("Yeni kod")]

    public float rotationSpeed; 
     float  mouseX, mouseY;
    void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);

    }

    // Update is called once per frame
    void FixedUpdate()
   {
        // transform.position = Vector3.Lerp(transform.position,car.transform.position+car.transform.TransformDirection(new Vector3(0f,yukseklik,uzaklik)),dampening* Time.deltaTime);
        //  CameraPosition = car.transform;
        // transform.LookAt(car.transform);
        /*    Vector3 currentAngles = transform.localEulerAngles;
            currentAngles.y = m_HorizontalAngle;
            transform.localEulerAngles = currentAngles;

            var turnCam = -Input.GetAxis("Mouse Y");
            turnCam = turnCam * MouseSensitivity;
            float turnPlayer = Input.GetAxis("Mouse X") * MouseSensitivity;
            m_HorizontalAngle = m_HorizontalAngle + turnPlayer;

            if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
            if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;
            m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
            currentAngles = CameraPosition.transform.localEulerAngles;
            currentAngles.x = m_VerticalAngle;
            CameraPosition.transform.localEulerAngles = currentAngles;*/

        
    }
    private void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, maxAltAci, 60);

        transform.LookAt(Merkez.transform);
        Merkez.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
     //   gameObject.transform.localPosition=new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z);
    }
}
