using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishAfterXSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    public float SecondsToWait = 1.5f;
    private float t = 0;
    public VanishAfterXSeconds(float secs)
    {
        SecondsToWait = secs;
    }
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / SecondsToWait;
        if(t >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
