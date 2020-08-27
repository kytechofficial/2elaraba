using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{

    public float saat, gunHizi=0.2f;
    public GameObject gunes;
    public float gunesZ;
    // Start is called before the first frame update
    void Start()
    {
        //saat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        saat += gunHizi * Time.deltaTime;

        if (saat > 24)
        {
            saat = 0;
        }
        gunesZ = Fmap(saat, 0, 24, 0, 360);
        gunes.transform.localEulerAngles =new Vector3(gunesZ,0,0);
        
    }

    private static int map(int value, int fromLow, int fromHigh, int toLow, int toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
    private static float Fmap(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
