using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    public float Seconds;
    private float elapsed;
    void Start()
    {
        elapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime / Seconds;
        if(elapsed > Seconds)
        {
            Destroy(gameObject);
        }
    }
}
