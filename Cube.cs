using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Game game;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        game.BreakCube(gameObject);
    }
}
