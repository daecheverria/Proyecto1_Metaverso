using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public float speed = 10f;
    private Vector2 velocity;
    Vector2 startPosition;
    public AudioSource AudioBall;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        startPosition = transform.position;
        StartCoroutine(Delay(0.5f));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        velocity.x = Random.Range(-1f, 1f);
        velocity.y = 1;
        rigidBody2D.velocity = velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newVelocity = rigidBody2D.velocity;

        if (Mathf.Abs(newVelocity.y) < 0.5f)
        {

            newVelocity.y = newVelocity.y > 0 ? 1f : -1f;
        }

        newVelocity = newVelocity.normalized * speed;
        rigidBody2D.velocity = newVelocity;

        rigidBody2D.velocity = rigidBody2D.velocity.normalized * speed;
        AudioBall.Play();
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            GameManager.instance.BajarVidas();
            ResetBall();
        }
    }

    public void ResetBall()
    {
        transform.position = new Vector2(0, -3.5f);
        rigidBody2D.velocity = Vector2.zero;
        StartCoroutine(Delay(0.5f));
    }
}

