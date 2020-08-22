using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject car;
    public GameObject carMerkezi;
    public bool carAktif;
    public float takipHizi=10f;
    public float takipHizi2=10f;


    // Start is called before the first frame update
    void Start()
    {
        carMerkezi = GameObject.Find("karakter");
    }
    private void Update()
    {
        if (carAktif)
        {
            if (car.GetComponent<CarController>().mevcutHiz < 10)
            {
                takipHizi = 1;
            }
            else
            {
                takipHizi = car.GetComponent<CarController>().mevcutHiz / 5;
            }
        }else
        {
            takipHizi = 10;
        }

       /* if (car.GetComponent<CarController>().mevcutHiz > 30)
        {
            takipHizi = 13f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 50)
        {
            takipHizi = 15f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 60)
        {
            takipHizi = 17f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 80)
        {
            takipHizi = 20f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 100)
        {
            takipHizi = 25f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 120)
        {
            takipHizi = 30f;
        }
        else if (car.GetComponent<CarController>().mevcutHiz > 130)
        {
            takipHizi = 35f;
        }
        else
        {
            takipHizi = 10f;*
        }*/
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
            transform.position = Vector3.Lerp(transform.position, carMerkezi.transform.position, takipHizi * Time.deltaTime);
        
    }
}
