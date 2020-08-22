using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IlanBilgileri : MonoBehaviour
{
    
    public Text paraT,kmT,mDrumT,dDrumT,kDrumT,mLitreT;
    int araba;

    public void Ayarla(int _araba=0,int km=0,int motorDurum = 0, int dosemeDurum = 0, int kaportaDurum = 0, float motorLitre = 0, int parasi = 0)
    {
        switch (_araba)
        {
            case 0:
                kmT.text = "Sahibinden Satılık Tipo \n KM: " + km + " MD: " + motorDurum + " KD: " + kaportaDurum + " " + motorLitre + "L";
                break;
            case 1:
                kmT.text = "Satılık Pejo Acill \n KM: " + km + " MD: " + motorDurum + " KD: " + kaportaDurum + " " + motorLitre + "L";
                break;
            case 2:
                kmT.text = "Böyle Lambo Yok \n KM: " + km + " MD: " + motorDurum + " KD: " + kaportaDurum + " " + motorLitre + "L";
                break;
            case 3:
                kmT.text = "Al git temiz fluence \n KM: " + km + " MD: " + motorDurum + " KD: " + kaportaDurum + " " + motorLitre + "L";
                break;

        }
            
        
        paraT.text = parasi + "TL";
        gameObject.transform.Find("SatinAlButon").GetComponent<Button>().onClick.AddListener(SatinAl);
        araba = _araba;

    }

    void SatinAl()
    {
        GameObject.Find("GameSystem").GetComponent<Bilgisayar>().SatinAl(araba);
    }
  
}
