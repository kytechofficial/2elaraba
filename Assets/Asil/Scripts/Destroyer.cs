using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    float silmeSure = 2;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (silmeSure <= timer)
        {
            Destroy(gameObject);
        }
    }
}
