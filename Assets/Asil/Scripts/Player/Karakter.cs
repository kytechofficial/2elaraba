using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    [SerializeField]private string selectableTag = "Selectable";
    [SerializeField] private Material seciliMat;
    public Text ortaBilgi;
    public float uzunluk;
    public GameObject pc;
    public bool raycastKontrol = true;
    public GameObject aracKamera,aracMerkez,speedoMeter;

    public bool arabadaMi;
    public float aBinisSure;
    public Text kmText, motorText, kaportaText, dosemeText, hacimText, fiyatText;
    GameObject gm;
    GameObject abp;
    void Start()
    {
        arabadaMi = false;
        aBinisSure = 2f;
        gm = GameObject.Find("GamePanel");
        abp = gm.transform.Find("AracBilgiPanel").gameObject;
        kmText = abp.transform.Find("KMText").GetComponent<Text>();
        motorText = abp.transform.Find("MotorText").GetComponent<Text>();
        kaportaText = abp.transform.Find("KaportaText").GetComponent<Text>();
        dosemeText = abp.transform.Find("DosemeText").GetComponent<Text>();
        hacimText = abp.transform.Find("HacimText").GetComponent<Text>();
        fiyatText = abp.transform.Find("FiyatText").GetComponent<Text>();
    }

    void Update()
    {
        if (arabadaMi)
        {
            aBinisSure -= 1f*Time.deltaTime;
            if (aBinisSure <= 0) { arabadaMi = false; }
        }
        /* int layerMask = 1 << 8;
         layerMask = ~layerMask;
         RaycastHit hit;
         if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, uzunluk, layerMask)){

             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
             Debug.Log("Did Hit");
         }
         else
         {
             Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
             Debug.Log("Did not Hit");
         }*/

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,2f) && raycastKontrol)
        {
            uzunluk = hit.distance;
            var selection = hit.transform;
            var selRen = selection.name;
            if(selRen != null)
            {
                
                if (selection.tag == "Bilgisayar")
                {
                    ortaBilgi.text = "Bilgisayarı aç.";
                    if (Input.GetButtonDown("Etus"))
                    {
                        
                        pc.SetActive(true);
                        GetComponent<Controller>().enabled = false;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        raycastKontrol = false;
                        abp.SetActive(false);
                    }
                }
                else if(selection.tag == "Araba"){
                    
                    ortaBilgi.text = selection.gameObject.GetComponent<ArabaBilgi>().aracAdi + " bin.";
                    abp.SetActive(true);
                    kmText.text = "KM: "+ selection.gameObject.GetComponent<ArabaBilgi>().km + " km";
                    motorText.text = "Motor Durum: " + selection.gameObject.GetComponent<ArabaBilgi>().motorDurum;
                    kaportaText.text = "Kaporta Durum: " + selection.gameObject.GetComponent<ArabaBilgi>().kaportaDurum;
                    dosemeText.text = "Döşeme Durum: " + selection.gameObject.GetComponent<ArabaBilgi>().dosemeDurum;
                    hacimText.text = "Motor Hacmi: " + selection.gameObject.GetComponent<ArabaBilgi>().motorLitre;
                    fiyatText.text = "Alınan Fiyat: " + selection.gameObject.GetComponent<ArabaBilgi>().fiyat + "TL";
                    if (Input.GetButton("Etus"))
                    {
                        if (arabadaMi == false)
                        {
                            ArabaBin(selection.gameObject);
                            abp.SetActive(false);
                        }
                        }
                    }
                else {
                    ortaBilgi.text = "" + selRen;
                    abp.SetActive(false);
                }
            }
        }
        else
        {
            ortaBilgi.text = "";
            abp.SetActive(false);
        }
    }
    void ArabaBin(GameObject car)
    {
        speedoMeter.SetActive(true);
        aracKamera.GetComponent<CarCamera>().car = car;
        aracMerkez.GetComponent<Follow>().car= car;
        aracMerkez.GetComponent<Follow>().carMerkezi = car.transform.Find("KameraMerkezi").gameObject;
        aracMerkez.GetComponent<Follow>().carAktif =true;
        aracMerkez.SetActive(true);
        aracKamera.SetActive(true);
        gameObject.SetActive(false);
        car.GetComponent<CarController>().arabaAktif = true;
        car.GetComponent<CarController>().aInisSure = 2f;
        car.GetComponent<CarController>().arabadaMi= false;
        car.GetComponent<CarController>().ses.Play();
        car.GetComponent<CarController>().ses.loop= true;
        ortaBilgi.text = "";
        Debug.Log("Arabaya binildi.");
        
    }
}
