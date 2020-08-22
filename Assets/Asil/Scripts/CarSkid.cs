using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSkid : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentFrictionValue;
    public float skidAt = 0.4f;
    public float skidAtSound = 0.5f;
    public GameObject skidSound;
    float soundEm=15;
    private float soundWait; 
    float markWidth=0.2f;
    int skidding;
    Vector3[] lastPs = new Vector3[2];
    public Material skidMaterial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WheelHit hit;
        transform.GetComponent<WheelCollider>().GetGroundHit(out hit);
        currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
        if(skidAtSound<= currentFrictionValue&& soundWait<=0)
        {
            Instantiate(skidSound,hit.point,Quaternion.identity);
            soundWait = 2;
        }
        soundWait -= Time.deltaTime * soundEm;
       
        if (skidAt <= currentFrictionValue)
        {
            SkidMesh();
        }
        else
        {
            skidding = 0;
        }
    }
    

    void SkidMesh()
    {
        WheelHit hit;
        transform.GetComponent<WheelCollider>().GetGroundHit(out hit);
        GameObject mark = new GameObject("Mark");
        mark.AddComponent<MeshFilter>();
        mark.AddComponent<MeshRenderer>();
        Mesh markMesh = new Mesh();
        var verticles = new Vector3[4];
        //var triangles = new int[6];
       // int[] triangles = { 0, 1, 2, 0, 2, 3, 0, 3, 2, 0, 2, 1 };
        int[] triangles = { 0, 1, 2, 0, 2, 3};
        if (skidding == 0)
        {
            verticles[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(markWidth, 0.01f, 0);
            verticles[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(-markWidth, 0.01f, 0);
            verticles[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(-markWidth, 0.01f, 0);
            verticles[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(markWidth, 0.01f, 0);
            lastPs[0] = verticles[2];
            lastPs[1] = verticles[3];
            skidding = 1;
        }
        else
        {
            verticles[1] = lastPs[0];
            verticles[0] = lastPs[1];
            verticles[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(-markWidth, 0.01f, 0);
            verticles[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z) * new Vector3(markWidth, 0.01f, 0);
            lastPs[0] = verticles[2];
            lastPs[1] = verticles[3];
        }
        //triangles[0] = [0,1,2,0,3,2];
        //triangles = { 0, 1, 2, 0, 2 , 3};
       /* triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[0] = 0;
        triangles[3] = 2;
        triangles[2] = 3;*/

        markMesh.vertices = verticles;
        markMesh.triangles = triangles;
        markMesh.RecalculateNormals();
       //var uvm = new Vector2[markMesh.vertices.Length];
       var uvm = new Vector2[4];
        uvm[0] = new Vector2(1, 0);
        uvm[1] = new Vector2(0, 0);
        uvm[2] = new Vector2(0, 1);
        uvm[3] = new Vector2(1, 1);
        /*for(int i=0; i < uvm.Length; i++)
        {
            uvm[i] = new Vector2(markMesh.vertices[i].x, markMesh.vertices[i].z);
        }*/
        markMesh.uv = uvm;
        mark.GetComponent<MeshFilter>().mesh = markMesh;
        mark.GetComponent<Renderer>().material = skidMaterial;
        mark.GetComponent<Renderer>().receiveShadows = false;
        mark.AddComponent<Destroyer>();
    }
}
