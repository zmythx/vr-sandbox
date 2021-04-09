using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : MonoBehaviour
{
    // Start is called before the first frame update
    float t;
    public GameObject TargetOne;
    public GameObject TargetTwo;
    Vector3 startPosition;
    Vector3 targetonepos;
    Vector3 targettwopos;
    public float timeToReachTarget;
    bool moveToOne = true;
    bool arrived = false;
    void Start()
    {
        arrived = false;
        moveToOne = true;
        startPosition = transform.position;
        targetonepos = TargetOne.transform.position;
        targettwopos = TargetTwo.transform.position;
    }
    void Update()
    {
    //    Debug.Log(string.Format("t: {0}", t));
        t += Time.deltaTime / timeToReachTarget;
        if(arrived && t >= 0.5f)
        {
            arrived = false;
            t = 0;
        }
        if (!arrived)
        {
            if (transform.position == targetonepos && moveToOne)
            {
                ArrivedDestination();
                moveToOne = false;
                SetDestination(targettwopos, 3);
            }
            if (transform.position == targettwopos && !moveToOne)
            {
                ArrivedDestination();
                moveToOne = true;
                SetDestination(targettwopos, 3);
            }
            if (moveToOne)
            {
                transform.position = Vector3.Lerp(startPosition, targetonepos, t);
            }
            if (!moveToOne)
            {
                transform.position = Vector3.Lerp(startPosition, targettwopos, t);
            }
        }

        
    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
    }
    public void ArrivedDestination()
    {
        t = 0;
        arrived = true;
    }
}
