using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScope : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera ScopeCamera;
    RaycastHit hit;
    public GameObject Reticle;
    public int ZoomDistance;
    public Color ReticleColor;
    public Color ReticleEnemyHighlightColor;
    public int MaxEnemyDetectDistance;
    public GameObject FirePoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.TransformDirection(Vector3.forward), out hit, ZoomDistance))
        {
            ScopeCamera.transform.position = hit.point - Vector3.Normalize((hit.point - FirePoint.transform.position)) * .2f;
            
        }
        else
        {
            ScopeCamera.transform.localPosition = new Vector3(0, 0, ZoomDistance);
        }
        if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.TransformDirection(Vector3.forward), out hit, MaxEnemyDetectDistance))
        {
            if (hit.transform.tag == "Enemy")
            {
                Reticle.GetComponent<Renderer>().material.SetColor("_Color", ReticleEnemyHighlightColor);
            }
            else
            {
                Reticle.GetComponent<Renderer>().material.SetColor("_Color", ReticleColor);
            }    
        }
        else
        {
            Reticle.GetComponent<Renderer>().material.SetColor("_Color", ReticleColor);
        }    
    }
}
