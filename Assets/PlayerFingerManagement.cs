using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerFingerManagement : MonoBehaviour
{
    public string RCurrentState = "Resting";
    public string LCurrentState = "Resting";

    public Player playerInstance;
    private float indexPos;
    private float middlePos;
    private float ringPos;
    private float pinkyPos;
    private float thumbPos;


    public float thresMin = 0.3f;
    public float thresMax = 0.8f;


    public Text gameText;
    public Text gameTextLeftHand;

    private bool isStraight(float comp)
    {
        return (comp < thresMin);
    }
    private bool isCurl(float comp)
    {
        return (comp > thresMax);
    }
    private string curlState() //0 means curled, 1 means straight!
    {
        string res2 = "00000";
        char[] res = res2.ToCharArray();
        if(isCurl(thumbPos))
        {
            res[0] = '0';
        }
        else
        {
            res[0] = '1';
        }
        if (isCurl(indexPos))
        {
            res[1] = '0';
        }
        else
        {
            res[1] = '1';
        }
        if (isCurl(middlePos))
        {
            res[2] = '0';
        }
        else
        {
            res[2] = '1';
        }
        if (isCurl(ringPos))
        {
            res[3] = '0';
        }
        else
        {
            res[3] = '1';
        }
        if (isCurl(pinkyPos))
        {
            res[4] = '0';
        }
        else
        {
            res[4] = '1';
        }
        string retString = new string(res);
      //  Debug.Log(retString);
        return retString;
    }
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        indexPos = playerInstance.rightHand.skeleton.indexCurl;
        middlePos = playerInstance.rightHand.skeleton.middleCurl;
        ringPos = playerInstance.rightHand.skeleton.ringCurl;
        pinkyPos = playerInstance.rightHand.skeleton.pinkyCurl;
        thumbPos = playerInstance.rightHand.skeleton.thumbCurl;
        switch (curlState())
        {
            case "01100":
                RCurrentState = "Ninja";
                break;
            case "11001":
                RCurrentState = "Rocker";
                break;
            case "00100":
                RCurrentState = "Rude";
                break;
            case "10111":
                RCurrentState = "Firecast";
                break;
            case "00000":
                RCurrentState = "Openpalm";
                break;
            default:
                RCurrentState = "Resting";
                break;
        }
        indexPos = playerInstance.leftHand.skeleton.indexCurl;
        middlePos = playerInstance.leftHand.skeleton.middleCurl;
        ringPos = playerInstance.leftHand.skeleton.ringCurl;
        pinkyPos = playerInstance.leftHand.skeleton.pinkyCurl;
        thumbPos = playerInstance.leftHand.skeleton.thumbCurl;
        switch (curlState())
        {
            case "01100":
                LCurrentState = "Ninja";
                break;
            case "11001":
                LCurrentState = "Rocker";
                break;
            case "00100":
                LCurrentState = "Rude";
                break;
            case "10111":
                LCurrentState = "Firecast";
                break;
            case "00000":
                LCurrentState = "Openpalm";
                break;
            default:
                LCurrentState = "Resting";
                break;
        }
       // gameText.text = RCurrentState;
      //  gameTextLeftHand.text = LCurrentState;
    }
}
