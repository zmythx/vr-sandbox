using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScaler : MonoBehaviour
{
    // Start is called before the first frame update
    public float FixeScale = 0.5f;
    public GameObject parent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(FixeScale + parent.transform.localScale.x * 2, FixeScale + parent.transform.localScale.y * 2, FixeScale + parent.transform.localScale.z * 2);
    }
}
