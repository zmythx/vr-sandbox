using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TextDisplayDebug : MonoBehaviour
{
    public Player player;
    private PlayerFingerManagement pfm;

    private 
    // Start is called before the first frame update
    void Start()
    {
        pfm = player.GetComponent<PlayerFingerManagement>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
