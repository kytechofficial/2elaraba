using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputManager : MonoBehaviour
{
    public float gaz, direksiyon;
    void Start()
    {

    }


    void Update()
    {
        gaz = Input.GetAxis("Vertical");
        direksiyon = Input.GetAxis("Horizontal");
    }
}
