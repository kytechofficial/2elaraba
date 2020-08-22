using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIControl : MonoBehaviour
{

    public Text hizText;
    public Text rpmText;
    public Text vitesText;
    public Image needle;
    public Image needle2;
    //private float velecity=10;
    public float takipDeger=1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject sp = GameObject.Find("GamePanel").transform.Find("speedometer").gameObject;
        hizText = sp.transform.Find("hizText").GetComponent<Text>();
        vitesText = sp.transform.Find("vitesText").GetComponent<Text>();
        needle = sp.transform.Find("n").transform.Find("needle").GetComponent<Image>();
        needle2 = sp.transform.Find("n").transform.Find("needle2").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HizAyarla(float hiz,float RPM,int vites)
    {
        hizText.text = "" + hiz;
       // rpmText.text = "RPM : " + (int)RPM;
        vitesText.text = "" + vites;
    }
    public void speedoMeter(float deger)
    {
        float z = transform.localRotation.z;
        float don=Mathf.Lerp(z, deger, takipDeger);
        needle.transform.localEulerAngles = new Vector3(0, 0, -deger);
    }
    private void FixedUpdate()
    {
        Dondur();
    }
    void Dondur()
    {
        needle2.transform.localRotation = Quaternion.Lerp(needle2.transform.localRotation, needle.transform.localRotation, 2f*Time.deltaTime);
    }
}
