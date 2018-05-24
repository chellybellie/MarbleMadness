using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAI : MonoBehaviour {
    public GameObject Target;
    public NavMeshAgent ai;
    public RaycastHit rHit;
    public NavMeshHit hit;
    public EnemyManager em;
      
    public bool isWander = false;
    public bool isEngaged = false;
    public float wanderTimer;
    public float timer;
    public float speed;
    public float searchRadius;
    public Vector3 DebugSphere;
    float targetdis;



    void Start ()
    {
        ai = GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("Player");
        timer = 0;
	}
	//void OnDrawGizmos()
 //   {
 //      Gizmos.DrawSphere(DebugSphere, 5);
 //   }

    // allows Ai to wandernavmesh till playerTarget is found
   public void Wander()
    {
        //useing hit to notify ai when random positon  is "hit"

        if ((transform.position - ai.destination).sqrMagnitude < 100 || timer <= 0) 
        {
            ai.speed = 3;
            ai.acceleration = 3;
            timer = wanderTimer;
           

            
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

        //Draws enemy's line of sight
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10.0f, Color.red);

        // finding target using physics.raycast
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out rHit, 10.0f))
        {
            Debug.Log(rHit.collider.name);
            if (rHit.collider.name == "Player")
            {  
                Debug.Log("I FOUND THE TARGET!");
                isEngaged = true;
                em.EnemySpawner(rHit);
            }
        }

    }

    public void Engaged()
    {
        // when target is fouhd movment is increased
        ai.speed = 3;
        ai.acceleration = 3;
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
