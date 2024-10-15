using UnityEngine;

public class Bar : MonoBehaviour
{
    public Rigidbody2D RigidBody2D;
    public float velocidad = 25f;
    private float input;
    private Vector2 posicionNueva;

    void Start()
    {
        posicionNueva = RigidBody2D.position;
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        posicionNueva += Vector2.right * input * velocidad * Time.deltaTime;
        posicionNueva.x = Mathf.Clamp(posicionNueva.x, -7f, 7f);
        RigidBody2D.MovePosition(posicionNueva);
        
    }
}
