using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPos;
    private Quaternion startRot;
    public GameObject MasterItem;
    private Vector3 posOffset;
    private Quaternion rotOffset;
    
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        posOffset = startPos - MasterItem.transform.position;
        rotOffset = startRot * MasterItem.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
