using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public float speed = 300;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity.x = Random.Range(-1f,1f);
        velocity.y = 1;
        rigidBody2D.AddForce(velocity*speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
