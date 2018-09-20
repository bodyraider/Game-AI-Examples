using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviors : MonoBehaviour {
    public double arrivetime = 1;     //到达时间的快慢
    public double MaxSpeed = 5;
    public float XForce = 50;
    public float ZForce = 100;
    public Vector3 direction;
    private Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        direction = rb.velocity.normalized;
        Vector3 separate = GetComponentInChildren<TagNeighbor>().Separation();
        Vector3 bossseparate = GetComponentInChildren<TagNeighbor>().BossSeparation();
        Vector3 alignment = GetComponentInChildren<TagNeighbor>().Alignment();
        Vector3 cohesion = GetComponentInChildren<TagNeighbor>().Cohesion();
        Vector3 wander = Wander();
        rb.AddForce(alignment);
        rb.AddForce(wander);
        rb.AddForce(cohesion);
        rb.AddForce(separate);
        rb.AddForce(bossseparate);
        ResetPosition();
        
	}

    Vector3 Seek(Vector3 TargetPos)   //返回一个向量，最终通过AddForce发力
    {
        Vector3 DesiredVelocity = TargetPos - transform.position;
        return DesiredVelocity.normalized;
    }

    Vector3 Arrive(Vector3 TargetPos)   //返回一个向量，最终通过AddForce发力
    {
        Vector3 zero = new Vector3(0, 0, 0);
        Vector3 ToTarget = TargetPos - transform.position;
        double dist = ToTarget.magnitude;
        if (dist > 0)
        {
            const double DecelerationTweaker = 0.3;
            double speed = dist / (arrivetime * DecelerationTweaker);
            speed = Mathf.Min((float)speed,(float)MaxSpeed);
            float magnify = (float)speed / (float)dist;
            Vector3 bigger = new Vector3(magnify, 0, magnify);
            Vector3 DesiredVelocity = Vector3.Scale(ToTarget, bigger);
            return (DesiredVelocity - rb.velocity);


        }
        return zero;
    }

    Vector3 Jitter(Vector3 wanderpoint)   //为Wander制造一个随机抖动
    {
        Vector3 jitter = Random.onUnitSphere;
        jitter.x *= XForce;
        jitter.y = 0;
        jitter.z *= ZForce;
        return (wanderpoint + jitter);
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
