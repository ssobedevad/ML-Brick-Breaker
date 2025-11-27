using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paddle : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    public UnityEvent hitBall = new UnityEvent();
    void Update()
    {
        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x, -8, 8),-3);
        rb.velocity *= 0.95f;
    }
    public void Left()
    {
        rb.velocity = new Vector2(-5f, 0);
    }
    public void Right()
    {
        rb.velocity = new Vector2(5f, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            hitBall.Invoke();
        }
    }
}
