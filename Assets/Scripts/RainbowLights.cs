using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowLights : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer objRender;
    public Material matToChange;
    float curHue = 0;
    public float ColorSpeed = 0.5f;
    void Start()
    {
        objRender = this.GetComponent<Renderer>();
       /* foreach(Material mat in objRender.materials)
        {
            if(mat.name=="Material.002")
            {
                matToChange = mat;
            }
        }
        if(matToChange == null)
        {
            matToChange = objRender.material;
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curHue >= 1)
        {
            curHue = 0;
        }
        curHue += 0.001f * ColorSpeed;
        matToChange.SetColor("_Color", Color.HSVToRGB(curHue, 1, 1));
        matToChange.SetColor("_EmissionColor", Color.HSVToRGB(curHue, 1, 1));
    }
}
