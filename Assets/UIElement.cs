using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    // Start is called before the first frame update
    private Material thisMat;
    private bool isHoveredOn;
    private Color origColor;
    

    protected virtual void Start()
    {
        thisMat = transform.GetComponent<Renderer>().material;
        origColor = thisMat.color;
    }

    public virtual void OnHover()
    {
        thisMat.SetColor("_Color", new Color(0.8f, 0.3f, 0.8f));
        isHoveredOn = true;
    }
    public virtual void OnExit()
    {
        thisMat.SetColor("_Color", origColor);
        isHoveredOn = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void OnClick()
    {
        if(isHoveredOn)
        {
            //do stuff
        }
    }
}
