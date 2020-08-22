using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bilgisayar : MonoBehaviour
{
    public GameObject pcMenu,karakter;
    public int km, motorDurum, dosemeDurum, kaportaDurum,parasi;
    public float motorLitre;
    public GameObject ilan,ilanYeri;

    public GameObject[] arabalar;
    void Start()
    {
        ilanBelirle(0,1500, 1, 2, 3, 1.6f, 10000);
        ilanBelirle(1,15000, 1, 2, 2, 1.5f, 33000);
        ilanBelirle(2,151546, 1, 1, 3, 1.2f, 14000);
        ilanBelirle(3,78500, 0, 2, 3, 1.4f, 55000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu(int Secenek)
    {
        switch (Secenek)
        {
            case 0://Pc Ekranını Kapatma
                pcMenu.SetActive(false);
                karakter.GetComponent<Controller>().enabled = true;
                karakter.GetComponent<Karakter>().raycastKontrol = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case 1:
                
                break;
        }
    }

    public void ilanBelirle(int _araba=0, int _km=0, int _motorDurum = 0, int _dosemeDurum = 0, int _kaportaDurum = 0, float _motorLitre = 0,int _parasi=0)
    {
        GameObject ilans = Instantiate(ilan, transform.position, transform.rotation);
        ilans.transform.parent = ilanYeri.transform;
        ilans.transform.localScale = new Vector3(1,1,1);
        ilans.GetComponent<IlanBilgileri>().Ayarla(_araba,_km, _motorDurum, _dosemeDurum, _kaportaDurum, _motorLitre, _parasi);
    }

    public void SatinAl(int arabaa=0)
    {
        Instantiate(arabalar[arabaa], GameObject.Find("AracSpawnYeri").transform.position, transform.rotation);
    }
}
