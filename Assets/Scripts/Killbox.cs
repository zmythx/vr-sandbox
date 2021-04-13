using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided!");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Found player!");
            CharacterController cc = collision.gameObject.GetComponent<CharacterController>();
            cc.enabled = false;
            collision.gameObject.transform.position = new Vector3(0, 10, 0);
            cc.transform.position = new Vector3(0, 10, 0);
            cc.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
