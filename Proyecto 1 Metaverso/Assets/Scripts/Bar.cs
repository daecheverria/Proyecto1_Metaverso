using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
public Rigidbody2D RigidBody2D;
private float inputValue;
public float moveSpeed = 25;
private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputValue = Input.GetAxisRaw("Horizontal");
        if (inputValue == 1)
        {
            direction = Vector2.right;
        }
        else if (inputValue == -1)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
        }

        RigidBody2D.AddForce(direction * moveSpeed * Time.deltaTime * 100);

    }
}
