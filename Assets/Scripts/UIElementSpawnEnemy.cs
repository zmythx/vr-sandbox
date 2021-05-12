using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementSpawnEnemy : UIElement
{
    // Start is called before the first frame update
    public GameObject EnemyToSpawn;
    public int XDistance;
    public int XSpeed;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void OnHover()
    {
        base.OnHover();
        transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.8f, 0.3f, 0.3f));
    }
    public override void OnClick()
    {
        base.OnClick();
        GameObject newTarget = Instantiate(EnemyToSpawn);
        newTarget.transform.position = new Vector3(0, 3, XDistance + Random.Range(1f, 10f));
        MoveBetweenTwoPoints targ = newTarget.GetComponent<MoveBetweenTwoPoints>();
        float zed = newTarget.transform.position.z;
        targ.timeToReachTarget = 10/XSpeed;
        targ.TargetOne.transform.position = new Vector3(XSpeed * Random.Range(1, 8), 3, zed);
        targ.TargetTwo.transform.position = new Vector3(-XSpeed * Random.Range(1, 8), 3, zed);
    }
}
