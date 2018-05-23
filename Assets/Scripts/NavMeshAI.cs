using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {
    public GameObject Target;
    public NavMeshAgent ai;
    public bool isWander = false;
    public bool isEngaged = false;
    public float wanderTimer;
    public float timer;
    public float speed;
    public float searchRadius;
    public Vector3 DebugSphere;
    float targetdis;
    float POVdis;
    void Start ()
    {
        ai = GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("Player");
        timer = 0;
	}
	void OnDrawGizmos()
    {
       Gizmos.DrawSphere(DebugSphere, 5);
    }

    // allows Ai to wandernavmesh till playerTarget is found
   public void Wander()
    {   NavMeshHit hit;
        if ((transform.position - ai.destination).sqrMagnitude < 100 || timer <= 0) 
        {
            ai.speed = 6;
            ai.acceleration = 6;
            timer = wanderTimer;
           
            //useing hit to notify ai when random positon  is "hit"
            
            //useing insideunitsphere to return randoms points in a sphere
            Vector3 temploc = transform.position + Random.insideUnitSphere * searchRadius;
            
            NavMesh.SamplePosition(temploc, out hit, searchRadius, 1);
            Vector3 RandomPos = hit.position;
            DebugSphere = RandomPos;
            ai.destination = RandomPos;
            ai.isStopped = false;
        }
        // checks target dis 
        targetdis = Vector3.Distance(Target.transform.position, transform.position);
        // finding target using navmesh raycast
       
        
        
        //if (POV )
        //{

        //    isEngaged = true;
        //}

    }

    public void Engaged()
    {
        // when target is fouhd movment is increased
        ai.speed = 6;
        ai.acceleration = 6;
        if(isEngaged == true)
        {
         // when target found set ai destination to target location
         ai.destination = Target.transform.position;
        }

    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (Target != null)
        {
            // makes ai look at target maybe for futur affects!
            transform.LookAt(ai.nextPosition);
        }
        if (!isEngaged)
        {
            isWander = true;
            Wander();
        }
        else
        {
            Engaged();
        }
	}
}
