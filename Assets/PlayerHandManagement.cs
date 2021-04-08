using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class PlayerHandManagement : MonoBehaviour
{
    public Player playerInstance;
    private Hand lHand;
    private Hand rHand;
    public Text updateText;
    private LineRenderer testRender;
    private LineRenderer testRender2;
    public GameObject caster;
    public LayerMask msk;
    private Ray rayc;
    private Ray menuc;
    #region Vectors

    public Vector3 RHandForwardVector(Hand hand, float multi)
    {
        return (hand.transform.position + -hand.transform.right * multi);
    }
    public Vector3 RHandBackwardsVector(Hand hand, float multi)
    {
        return (hand.gameObject.transform.position + hand.gameObject.transform.right * multi);
    }
    public Vector3 RHandRightVector(Hand hand, float multi)
    {
        return (hand.gameObject.transform.position + -hand.gameObject.transform.forward * multi + -hand.gameObject.transform.up * multi);
    }
    public Vector3 RHandDownVector(Hand hand, float multi)
    {
        return (hand.gameObject.transform.position + -hand.gameObject.transform.forward * multi + hand.gameObject.transform.up * multi);
    }
    public Vector3 RHandUpVector(Hand hand, float multi)
    {
        return (hand.gameObject.transform.position + hand.gameObject.transform.forward * multi + -hand.gameObject.transform.up * multi);
    }
    public Vector3 RHandLeftVector(Hand hand, float multi)
    {
        return (hand.gameObject.transform.position + hand.gameObject.transform.forward * multi + hand.gameObject.transform.up * multi);
    }
    public Vector3 LHandForwardVector(Hand hand, float multi)
    {
        return RHandBackwardsVector(hand, multi);
    }
    public Vector3 LHandBackwardsVector(Hand hand, float multi)
    {
        return RHandForwardVector(hand, multi);
    }
    public Vector3 LHandRightVector(Hand hand, float multi)
    {
        return RHandLeftVector(hand, multi);
    }
    public Vector3 LHandDownVector(Hand hand, float multi)
    {
        return RHandDownVector(hand, multi);
    }
    public Vector3 LHandUpVector(Hand hand, float multi)
    {
        return RHandUpVector(hand, multi);
    }
    public Vector3 LHandLeftVector(Hand hand, float multi)
    {
         return RHandRightVector(hand, multi);
    }

    public Ray VectorCastFromHand(Vector3 handVector, Hand hand)
    {
        return new Ray(hand.gameObject.transform.position, handVector - hand.gameObject.transform.position);
    }
    #endregion
    
    private GameObject hoveredElement;

    public bool RightFacingLeft()
    {
        RaycastHit hit;
        if(Physics.Raycast(rHand.transform.position, RHandForwardVector(rHand, 1), out hit, 100f))
        {
            updateText.text = hit.collider.tag.ToString();
            if(hit.collider.tag == "LeftHandTag")
            {
                return true;
            }
            return false;
        }
        return false;
    }
    public string GetHandState()
    {
        RaycastHit hit;
        int layerMask = 1 << 10;
        if (Physics.Raycast(VectorCastFromHand(RHandDownVector(rHand, 10), rHand), out hit, 100f, layerMask))
        {
            if(hit.transform.gameObject.tag == "LeftHandTag")
            {
            //    updateText.text = hit.collider.tag.ToString();
                return "RightAboveLeft";
            }
        }
        if (Physics.Raycast(VectorCastFromHand(RHandUpVector(rHand, 10), rHand), out hit, 100f, layerMask))
        {
            if (hit.transform.gameObject.tag == "LeftHandTag")
            {
               // updateText.text = hit.collider.tag.ToString();
                return "RightBelowLeft";
            }
        }
        if (Physics.Raycast(VectorCastFromHand(RHandForwardVector(rHand, 10), rHand), out hit, 100f, layerMask))
        {
            if (hit.transform.gameObject.tag == "LeftHandTag")
            {
             //   updateText.text = hit.collider.tag.ToString();
                return "RightFacingLeft";
            }
        }
        return "";
    }
    // Start is called before the first frame update
    void Start()
    {
        lHand = playerInstance.leftHand;
        rHand = playerInstance.rightHand;
    /*    testRender = lHand.gameObject.AddComponent<LineRenderer>();
        testRender.material = new Material(Shader.Find("Sprites/Default"));
        testRender.widthMultiplier = 0.02f;
        testRender.startColor = Color.blue;
        testRender.startColor = Color.red;
        testRender2 = rHand.gameObject.AddComponent<LineRenderer>();
        testRender2.material = new Material(Shader.Find("Sprites/Default"));
        testRender2.widthMultiplier = 0.02f;
        testRender2.startColor = Color.blue;
        testRender2.startColor = Color.red; */
        Ray rayc = new Ray(rHand.transform.position, RHandForwardVector(rHand, 1));
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
