using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health = 100;
    public int currentHealth;

    void Start()
    {
           currentHealth = health;
    }
	void Update ()
    {
        controlls();
	}
    void controlls()
    {

        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 Movement = new Vector3(-MoveHorizontal, 0.0f, -MoveVertical );
        rb.AddForce (Movement * speed);
    }

   public void TakeDmg(int amount )
    {

        currentHealth -= amount;
        Debug.Log("Took Dmg");
    }

}
                                                                                                            