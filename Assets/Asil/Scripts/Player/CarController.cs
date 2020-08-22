using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CarInputManager))]
[RequireComponent(typeof(CarUIControl))]
[RequireComponent(typeof(ArabaBilgi))]
public class CarController : MonoBehaviour
{
    internal enum cekisTipi
    {
        onden,
        arkdan,
        dortceker
    }
    internal enum vitesTuru
    {
        otomatik,
        manual
    }

    public bool arabaAktif=false;
 public CarInputManager inp;
    [Tooltip("1.sol ön- 2.sağ ön- 3.sol arka- 4. sağ arka")]
    public List<WheelCollider> gucTeker;
    [Tooltip("sol-sağ")]
    public List<WheelCollider> direksiyonTeker;
    public List<Transform> tekerler;
    public Transform aracInis;
    public float hiz;
    Rigidbody rb;
    [SerializeField]cekisTipi cekmeTipi;
    [SerializeField]vitesTuru vitesTipi;
    public GameObject karaker,carCamera,carKameraMerkez;
    
    public bool arabadaMi = true;
    public bool elFreni = false;
    public float aInisSure;
    public AudioSource ses;
    public float[] slip = new float[4];

    [Header("Araç Bilgileri")]
    public float guc = 2000f;
    public float maxTurn = 20f;
    public float radius = 6f;
    public float mevcutHiz;
    public float frenGucu;
    public float maxHiz;
    public float maxGeriHiz;
    public float motorFreni;
    public Transform arabaMerkezi;
    public int[] vitesRatio;
    public int vites;
    public float surtunme=50f;

    [Header("Motor Bilgileri")]
    public AnimationCurve motorGucu;
    public float wheelsRPM;
    public float smoothTime=0.01f;
    public float engineRPM;
    public float maxRPM;
    public float minRPM;
    public int gearNum;
    public float[] gears;
    public float vitesAraligi = 1500f;

    [Header("Işıklar")]
    public GameObject arkaIsikSol;
    public GameObject arkaIsikSag;
    [Tooltip("Normal-Fren-Geri Material")]
    public Material[] arkaIsikMet;

    private CarUIControl carUI;

    void Start()
    {
        inp = GetComponent<CarInputManager>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass= arabaMerkezi.transform.localPosition;
        aInisSure = 2f;
        ses = GetComponent<AudioSource>();
        carUI = GetComponent<CarUIControl>();
        karaker = GameObject.Find("karakter");
        carKameraMerkez = GameObject.Find("CarCameraMerkez");
        carCamera = carKameraMerkez.transform.Find("CarCamera").gameObject;


    }


    void FixedUpdate()
    {
        
        UpdateWheelPoses();
    }
    void UpdateWheelPoses()
    {
        for (int i = 0; i < 4; i++)
        {
            UpdateWheelPose(gucTeker[i], tekerler[i]);
        }
        
    }
    void DireksiyonHareket()
    {
        if (inp.direksiyon > 0)
        {
           
                direksiyonTeker[0].steerAngle =Mathf.Rad2Deg*Mathf.Atan(2.55f/( radius+(1.5f/2))) * inp.direksiyon;
                direksiyonTeker[1].steerAngle =Mathf.Rad2Deg*Mathf.Atan(2.55f/( radius-(1.5f/2))) * inp.direksiyon;
            
        }else if (inp.direksiyon < 0)
        {

            direksiyonTeker[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * inp.direksiyon;
            direksiyonTeker[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * inp.direksiyon;

        }
        else
        {
            foreach (WheelCollider wheel in direksiyonTeker)
        {
            wheel.steerAngle = 0;
        }
        }
        /*foreach (WheelCollider wheel in direksiyonTeker)
        {
            wheel.steerAngle = maxTurn * inp.direksiyon;
        }*/
        }
    void Kontrol()
    {

        //mevcutHiz = 2 * 22 / 7 * gucTeker[0].radius * gucTeker[0].rpm * 6 / 10000;
        mevcutHiz = rb.velocity.magnitude * 3.6f;
        mevcutHiz = Mathf.Round(mevcutHiz);
        if (mevcutHiz < maxHiz && mevcutHiz>-maxGeriHiz)
        {
            if (cekmeTipi == cekisTipi.arkdan)
            {
                
                    gucTeker[2].motorTorque = (guc / 2) * inp.gaz;
                    gucTeker[3].motorTorque = (guc / 2) * inp.gaz;
                    hiz = gucTeker[3].motorTorque;
                
            }else if (cekmeTipi == cekisTipi.onden)
            {
                gucTeker[0].motorTorque = (guc/2) * inp.gaz;
                gucTeker[1].motorTorque = (guc/2) * inp.gaz;
                hiz = gucTeker[1].motorTorque;
            }else if (cekmeTipi == cekisTipi.dortceker)
            {
                foreach (WheelCollider wheel in gucTeker)
                {
                    wheel.motorTorque = (guc/4) * inp.gaz;
                    hiz = wheel.motorTorque;
                }
            }
        }
        else
        {
            foreach (WheelCollider wheel in gucTeker)
            {
                wheel.motorTorque = 0;
                hiz = wheel.motorTorque;
            }
        }
        rb.drag = Mathf.Clamp((mevcutHiz / maxHiz) * 0.075f, 0.01f, 0.75f);
        float donme = 4;
        if (elFreni)
        {
            donme = 3;
        }
        else
        {
            donme = map((int)mevcutHiz, 0, (int)maxHiz, 3, 15);
        }
        radius= donme;
       // Debug.Log(radius);

    }
    void UpdateWheelPose(WheelCollider _collider,Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;

    }
    void ArkaIsik()
    {
        if(mevcutHiz>0 && inp.gaz < 0)
        {
            arkaIsikSag.GetComponent<Renderer>().material = arkaIsikMet[1];
            arkaIsikSol.GetComponent<Renderer>().material = arkaIsikMet[1];
            Debug.Log("Frene basıldı");
        }else if (mevcutHiz < 0 && inp.gaz > 0)
        {
            arkaIsikSag.GetComponent<Renderer>().material = arkaIsikMet[1];
            arkaIsikSol.GetComponent<Renderer>().material = arkaIsikMet[1];
            Debug.Log("Frene basıldı");
        }
        else if (mevcutHiz < 0 && inp.gaz < 0)
        {
            arkaIsikSag.GetComponent<Renderer>().material = arkaIsikMet[2];
            arkaIsikSol.GetComponent<Renderer>().material = arkaIsikMet[2];
            Debug.Log("Arka ışık");
        }
        else
        {
            arkaIsikSag.GetComponent<Renderer>().material = arkaIsikMet[0];
            arkaIsikSol.GetComponent<Renderer>().material = arkaIsikMet[0];
        }
    }
    private void Update()
    {
        
        if (!arabadaMi)
        {
            aInisSure -= 1f * Time.deltaTime;
            if (aInisSure <= 0) { arabadaMi = true; }
        }
        if (arabaAktif)
        {
            if (Input.GetButtonDown("Etus"))
            {
                if (arabadaMi) { 
                    Debug.Log("arabadan inilecek");
                    carCamera.SetActive(false);
                    carKameraMerkez.GetComponent<Follow>().carMerkezi = karaker;
                    carKameraMerkez.GetComponent<Follow>().carAktif = false;
                    karaker.transform.localPosition = aracInis.transform.position;
                    karaker.GetComponent<Karakter>().aBinisSure = 2f;
                    karaker.GetComponent<Karakter>().arabadaMi = true;
                    karaker.GetComponent<Karakter>().speedoMeter.SetActive(false);
                    karaker.SetActive(true);
                    karaker.transform.Find("FootStep").GetComponent<RandomPlayer>().step = true;
                    Debug.Log("arabadan inildi");
                    arabaAktif = false;
                    ses.loop = false;
                }
               
            }
            if (arabaAktif)
            {
                //Kontrol();
                ElFreni();
                DireksiyonHareket();
                surtunmeKuvveti();
                // getFriction();
                calculateMotorGucu();
               // VitesKontrol();

            }
            ArkaIsik();
            MotorSesi();

        }
    }
    void ElFreni()
    {
        if (Input.GetButton("Jump"))
        {
            elFreni = true;
         //   CarSkidOn();
        }
        else
        {
            elFreni = false;
           // CarSkidOff();
        }
        if (elFreni)
        {
            Debug.Log("El Freni Çekildi!");
            gucTeker[0].motorTorque = 0;
            gucTeker[1].motorTorque = 0;
            gucTeker[2].motorTorque = 0;
            gucTeker[3].motorTorque = 0;
            gucTeker[2].brakeTorque = frenGucu;
            gucTeker[3].brakeTorque = frenGucu;
        }else if (Input.GetButton("Vertical") == false && !elFreni)
        {
            foreach (WheelCollider wheel in gucTeker)
            {
                wheel.brakeTorque = motorFreni;
                //hiz = wheel.motorTorque;
            }
        }
        else
        {
            foreach (WheelCollider wheel in gucTeker)
            {
                wheel.brakeTorque = 0;
                //hiz = wheel.motorTorque;
            }
        }
    }

    void MotorSesi()
    {
       for(int i=0; i < vitesRatio.Length;i++)
        {
            if (vitesRatio[i] > mevcutHiz)
            {
                vites = i;
                gearNum = i;
                break;
            }
        }
        float vitesMinVal;
        float vitesMaxVal;
        if (vites == 0)
        {
            vitesMinVal = 0;
           
        }
        else
        {
            vitesMinVal = vitesRatio[vites-1];
            
        }

        vitesMaxVal = vitesRatio[vites];
        float enginePitch=1;
        float rpmDeger=1000;
        float needPos = 1;

        if (mevcutHiz > 0 &&vites==0)
        {
            enginePitch = ((mevcutHiz - vitesMinVal) / (vitesMaxVal - vitesMinVal)) + 1;
            rpmDeger = map((int)mevcutHiz, (int)vitesMinVal, (int)vitesMaxVal, 1000, (int)maxRPM);
           // needPos = map(rpmDeger,1000,(int)maxRPM,0,-240);
        }
        else if (vites> 0)
        {
            enginePitch = ((mevcutHiz - vitesMinVal) / (vitesMaxVal - vitesMinVal)) + 1.7f;
            rpmDeger = map((int)mevcutHiz, (int)vitesMinVal, (int)vitesMaxVal, (int)maxRPM - (int)vitesAraligi, (int)maxRPM);
            //needPos = map(rpmDeger, (int)maxRPM - (int)vitesAraligi, (int)maxRPM, 0, -240);

        }
        else if(mevcutHiz<0){
            enginePitch = ((mevcutHiz - vitesMinVal) / (vitesMaxVal - vitesMinVal)) - 1;
            rpmDeger = map((int)mevcutHiz, (int)vitesMinVal, (int)vitesMaxVal, 1000, (int)maxRPM);
            
            if (enginePitch < -1.5f)
            {
                enginePitch = -1.5f;
            }
        }

        engineRPM = rpmDeger;
        needPos = Fmap(rpmDeger, 0f, 10000f, 0f, 240f);
        Debug.Log(needPos);
        carUI.speedoMeter(needPos);
        ses.pitch = enginePitch;
    }

    void surtunmeKuvveti()
    {
        rb.AddForce(-transform.up*surtunme*rb.velocity.magnitude);
    }

    private void calculateMotorGucu()
    {
        wheelRPM();
        //guc =4f* motorGucu.Evaluate(engineRPM);
        float velocity = 0.0f;
        if (engineRPM >= maxRPM /*|| flag*/)
        {
           // engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - vitesAraligi, ref velocity, 0.05f);

            /*flag = (engineRPM >= maxRPM - 450) ? true : false;
            test = (lastValue > engineRPM) ? true : false;*/
        }
        else
        {
          //  engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

            /*test = false;*/
        }
      //  if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000;
        Kontrol();
        carUI.HizAyarla(mevcutHiz, engineRPM,gearNum);
    }
    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for(int i = 0; i < 4; i++)
        {
            sum += gucTeker[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
    }

    void getFriction()
    {
        for(int i=0;i<gucTeker.Count;i++)
        {
            WheelHit wheelHit;
            gucTeker[i].GetGroundHit(out wheelHit);
            slip[i] = wheelHit.forwardSlip;
        }
        
    }

    /* void VitesKontrol()
     {
         if (vitesTipi == vitesTuru.otomatik)
         {
             if(engineRPM>maxRPM && gearNum < gears.Length - 1)
             {
                 gearNum++;
                 //engineRPM -= vitesAraligi;
             }
             if (engineRPM < minRPM && gearNum > 0)
             {
                 gearNum--;
                // engineRPM -= vitesAraligi;
             }
         }if (vitesTipi == vitesTuru.manual)
         {
             if(Input.GetKeyDown(KeyCode.Z))
             {
                 gearNum++;
             }if(Input.GetKeyDown(KeyCode.X))
             {
                 gearNum--;
             }
         }
     }*/
     
   

    private static int map(int value, int fromLow, int fromHigh, int toLow, int toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
    private static float Fmap(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}
