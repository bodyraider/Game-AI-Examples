using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    public float XForce = 500;
    public float ZForce = 500;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        Vector3 wander = Wander();
        rb.AddForce(wander);
        ResetPosition();

    }
    Vector3 Wander()
    {
        Vector3 jitter = Random.onUnitSphere;
        jitter.x *= XForce;
        jitter.y = 0;
        jitter.z *= ZForce;
        return jitter;
    }
    void ResetPosition()
    {
        if (transform.position.x > 40)
            transform.position = new Vector3(transform.position.x - 80, transform.position.y, transform.position.z);
        if (transform.position.x < -40)
            transform.position = new Vector3(transform.position.x + 80, transform.position.y, transform.position.z);
        if (transform.position.z > 40)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 80);
        if (transform.position.z < -40)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 80);
    }

}
