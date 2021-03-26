using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerUIManagement : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerHandManagement phm;
    private Hand lHand;
    private Hand rHand;
    private GameObject hoveredElement;
    public Player playerInstance;
    private LineRenderer uiRenderLine;
    public SteamVR_Action_Single inputClick;
    private bool isClicking;

    void Start()
    {
        phm = transform.GetComponent<PlayerHandManagement>();
        lHand = playerInstance.leftHand;
        rHand = playerInstance.rightHand;
        uiRenderLine = transform.gameObject.AddComponent<LineRenderer>();
        uiRenderLine.material = new Material(Shader.Find("Sprites/Default"));
        uiRenderLine.widthMultiplier = 0.02f;
        uiRenderLine.startColor = Color.blue;
        uiRenderLine.endColor = Color.red;
        isClicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isHoveringOnUI = false;
        RaycastHit hit;
        int uicolmask = 1 << 11;
        if (Physics.Raycast(phm.VectorCastFromHand(phm.RHandUpVector(rHand, 10), rHand), out hit, 100f, uicolmask))
        {
            
            uiRenderLine.SetPosition(1, phm.RHandUpVector(rHand, 10));
            isHoveringOnUI = true;

        }
        int layermask = 1 << 12;
        if (Physics.Raycast(phm.VectorCastFromHand(phm.RHandUpVector(rHand, 10), rHand), out hit, 100f, layermask))
        {
            hoveredElement = hit.transform.gameObject;
            uiRenderLine.SetPosition(1, phm.RHandUpVector(rHand, 10));
            hoveredElement.GetComponent<UIElement>().OnHover();
            isHoveringOnUI = true;

        }
        else
        {
            
            if (hoveredElement != null)
            {
                hoveredElement.GetComponent<UIElement>().OnExit();
                hoveredElement = null;
            }
        }
        if(isHoveringOnUI)
        {
            uiRenderLine.enabled = true;
            uiRenderLine.SetPosition(0, rHand.transform.position);
        }
        else
        {
            uiRenderLine.enabled = false;
        }
        if(inputClick.axis >= 0.2 && hoveredElement != null && !isClicking)
        {
            hoveredElement.GetComponent<UIElement>().OnClick();
            isClicking = true;
        }
        if(inputClick.axis >= 0.2 && !isClicking)
        {
            isClicking = true;
        }
        if(inputClick.axis < 0.2 && isClicking)
        {
            isClicking = false;
        }
    }
}
