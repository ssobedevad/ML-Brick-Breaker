using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Ball : MonoBehaviour
{
    public Game game;
    [SerializeField] Rigidbody2D rb;
    public Paddle paddle;
    float BALL_SPEED = 4f;
    private void Start()
    {
        paddle.hitBall.AddListener(HitBall);
    }
    void HitBall()
    {
        BALL_SPEED = 4f;
    }
    private void Update()
    {
        if(Mathf.Abs(rb.velocity.x) < 0.01f)
        {
            rb.velocity +=new Vector2(Random.Range(-0.05f, 0.05f),0);
        }
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity += new Vector2(0,Random.Range(-0.05f, 0.05f));
        }
        rb.velocity = rb.velocity.normalized * BALL_SPEED;
        if(Mathf.Abs(transform.localPosition.x) > 9 || Mathf.Abs(transform.localPosition.y) > 6)
        {
            game.endGame.Invoke();
        }
        BALL_SPEED += 0.1f * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Lose")
        {
            game.End();
        }
    }
}
