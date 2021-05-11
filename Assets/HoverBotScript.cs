using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBotScript : MonoBehaviour
{
    public GameObject player;
    private string State;
    public int Speed;
    public int SecondsToWait;
    private GameObject nextTarget;
    private Vector3 nextTargetCoords;
    private Vector3 origPos;
    private float t;
    public float DetectionRange;
    public float flyingRadius;
    public float timeToReachTarget;
    public float HoverTimeBetweenTargets;
    public float SecondsBeforeFiringAfterDetection;
    private string lastState;
    private Vector3 startPos;
    public float ShotsPerSecond;
    public float k;
    public GameObject bullet;
    public GameObject FirePoint1;
    public GameObject FirePoint2;
    public int bulletShotSpeed;
    bool Fire1;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        origPos = transform.position;
        State = "patrol";
        lastState = "patrol";
        nextTarget = new GameObject("TravelPoint");
       // nextTarget.transform.parent = this.gameObject.transform;
        t = 0;
        Fire1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextTargetCoords != null)
        {
            nextTarget.transform.position = nextTargetCoords;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < DetectionRange)
        {
            if (State != "firing")
            {
                if (State != "fireAtPlayer")
                {
                    lastState = State;
                    State = "fireAtPlayer";
                    k = 0;
                }
                
            }
        }
        else
        {
            if (State == "fireAtPlayer" || State == "firing")
            {
                State = lastState;
            }
        }
        if(State == "patrol")
        {
            t = 0;
            startPos = transform.position;
            nextTarget.transform.position = new Vector3(origPos.x + Random.Range(0, flyingRadius), origPos.y + Random.Range(0, flyingRadius), origPos.z + Random.Range(0, flyingRadius));
            nextTargetCoords = nextTarget.transform.position;
            Debug.Log("Setting new target");
            State = "patrolMove";
        }
        if(State == "patrolMove")
        {
            nextTarget.transform.position = nextTargetCoords;
            transform.LookAt(nextTarget.transform);
            nextTarget.transform.position = nextTargetCoords;
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPos, nextTarget.transform.position, t);
            if (t >= 1)
            {
                t = 0;
                State = "patrolWait";
            }
        }
        if(State == "patrolWait")
        {
            t += Time.deltaTime / HoverTimeBetweenTargets;
            if (t >= 1)
            {
                State = "patrol";
            }
        }
        if(State == "fireAtPlayer")
        {
            transform.LookAt(player.transform);
            k += Time.deltaTime / SecondsBeforeFiringAfterDetection;
            if(k >= 1)
            {
                k = 0;
                State = "firing";
            }
        }
        if(State == "firing")
        {
            transform.LookAt(player.transform);
            k += Time.deltaTime / (1 / ShotsPerSecond);
            if(k >= 1)
            {
                Vector3 newBulletPos = new Vector3();
                k = 0;
                if(Fire1)
                {
                    newBulletPos = FirePoint1.transform.position;
                }
                else
                {
                    newBulletPos = FirePoint2.transform.position;
                }
                GameObject newBullet = Instantiate(bullet);
                newBullet.transform.position = newBulletPos;
                newBullet.transform.LookAt(player.transform);
                newBullet.GetComponent<Rigidbody>().AddForce((transform.forward * bulletShotSpeed));
                newBullet.GetComponent<EnemyBullet>().SetOwner(gameObject);
                Fire1 = !Fire1;
            }
        }
        Debug.Log(State);
    }
}
