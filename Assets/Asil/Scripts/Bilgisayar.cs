using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bilgisayar : MonoBehaviour
{
    public GameObject pcMenu,karakter;
    public int km, motorDurum, dosemeDurum, kaportaDurum,parasi;
    public float motorLitre;
    public GameObject ilan,ilanYeriSUV, ilanYeriBinek, ilanYeriSport, ilanlarSUV,ilanlarSport,ilanlarBinek,secenekPanel,
        geriTus;

    public GameObject[] arabalar;
    void Start()
    {
        ilanBelirleSUV(1,KMgetir(), 1, 2, 3, 1.6f, ParaBelirle(55000,115000));
        ilanBelirleSUV(1, KMgetir(), 1, 2, 2, 1.5f, ParaBelirle(55000, 115000));
        ilanBelirleSUV(1, KMgetir(), 1, 2, 2, 1.5f, ParaBelirle(55000, 115000));
        ilanBelirleSUV(1, KMgetir(), 1, 2, 2, 1.5f, ParaBelirle(55000, 115000));
        ilanBelirleBinek(0, KMgetir(), 1, 1, 3, 1.2f, ParaBelirle(15000, 55000));
        ilanBelirleBinek(3, KMgetir(), 0, 2, 3, 1.4f, ParaBelirle(15000, 55000));
        ilanBelirleBinek(3, KMgetir(), 0, 2, 3, 1.4f, ParaBelirle(15000, 55000));
        ilanBelirleSport(2, KMgetir(), 0, 2, 3, 1.4f, ParaBelirle(95000, 155000));
        ilanBelirleSport(2, KMgetir(), 0, 2, 3, 1.4f, ParaBelirle(95000, 155000));
        ilanBelirleSport(2, KMgetir(), 0, 2, 3, 1.4f, ParaBelirle(95000, 155000));
        geriTus.SetActive(false);
    }

    public int KMgetir()
    {
        int KM = Random.Range(0,250000);
        return KM;
    }

    public int ParaBelirle(int Max, int Min)
    {
        int Pere = Random.Range(Min, Max);
        return Pere;
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
                secenekPanel.SetActive(false);
                ilanlarSUV.SetActive(true);
                geriTus.SetActive(true);
                break;
            case 2:
                secenekPanel.SetActive(false);
                ilanlarSUV.SetActive(false);
                ilanlarSport.SetActive(true);
                geriTus.SetActive(true);

                break;
            case 3:
                secenekPanel.SetActive(false);
                ilanlarSUV.SetActive(false);
                ilanlarBinek.SetActive(true);
                geriTus.SetActive(true);

                break;
            case 4:
                secenekPanel.SetActive(true);
                ilanlarSUV.SetActive(false);
                ilanlarBinek.SetActive(false);
                ilanlarSport.SetActive(false);
                geriTus.SetActive(false);

                break;
        }
    }

    public void ilanBelirleSUV(int _araba=0, int _km=0, int _motorDurum = 0, int _dosemeDurum = 0, int _kaportaDurum = 0, float _motorLitre = 0,int _parasi=0)
    {
        GameObject ilans = Instantiate(ilan, transform.position, transform.rotation);
        ilans.transform.parent = ilanYeriSUV.transform;
        ilans.transform.localScale = new Vector3(1,1,1);
        ilans.GetComponent<IlanBilgileri>().Ayarla(_araba,_km, _motorDurum, _dosemeDurum, _kaportaDurum, _motorLitre, _parasi);
    }
    public void ilanBelirleSport(int _araba = 0, int _km = 0, int _motorDurum = 0, int _dosemeDurum = 0, int _kaportaDurum = 0, float _motorLitre = 0, int _parasi = 0)
    {
        GameObject ilans = Instantiate(ilan, transform.position, transform.rotation);
        ilans.transform.parent = ilanYeriSport.transform;
        ilans.transform.localScale = new Vector3(1, 1, 1);
        ilans.GetComponent<IlanBilgileri>().Ayarla(_araba, _km, _motorDurum, _dosemeDurum, _kaportaDurum, _motorLitre, _parasi);
    }
    public void ilanBelirleBinek(int _araba = 0, int _km = 0, int _motorDurum = 0, int _dosemeDurum = 0, int _kaportaDurum = 0, float _motorLitre = 0, int _parasi = 0)
    {
        GameObject ilans = Instantiate(ilan, transform.position, transform.rotation);
        ilans.transform.parent = ilanYeriBinek.transform;
        ilans.transform.localScale = new Vector3(1, 1, 1);
        ilans.GetComponent<IlanBilgileri>().Ayarla(_araba, _km, _motorDurum, _dosemeDurum, _kaportaDurum, _motorLitre, _parasi);
    }
    public void SatinAl(int arabaa=0)
    {
        Instantiate(arabalar[arabaa], GameObject.Find("AracSpawnYeri").transform.position, transform.rotation);
    }
}
