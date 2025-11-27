using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject ball, cube;
    [SerializeField] Paddle paddle;
    public List<GameObject> cubes = new List<GameObject>();
    public GameObject ballObject;
    
    public UnityEvent breakCube = new UnityEvent();
    public UnityEvent winGame = new UnityEvent();
    public UnityEvent endGame = new UnityEvent();
    private void Awake()
    {       
        StartGame();
    }
    public void StartGame()
    {
        if (ballObject == null)
        {
            ballObject = Instantiate(ball,transform,false);
            ballObject.GetComponent<Ball>().game = this;
            ballObject.GetComponent<Ball>().paddle = paddle;
            ballObject.transform.localPosition = new Vector3(Random.Range(-2f, 2f), 0);
            
        }
        else
        {
            ballObject.transform.localPosition = new Vector3(Random.Range(-2f, 2f), 0);
        }
        float maxSpeed = 4f;
        Vector2 dir = new Vector3(Random.Range(-.2f, .2f), -1).normalized;
        ballObject.GetComponent<Rigidbody2D>().velocity = dir * maxSpeed;

        cubes.ForEach(i => Destroy(i));
        cubes.Clear();
        for (int i = 0; i < 17; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                GameObject cubeObj = Instantiate(cube, transform,false);
                cubeObj.transform.localPosition = new Vector3(-8.5f + i, 3f + k * 0.4f, 0);
                cubeObj.GetComponent<Cube>().game = this;
                cubes.Add(cubeObj);
            }
        }
    }
    public void BreakCube(GameObject cube)
    {
        if (cubes.Contains(cube))
        {
            cubes.Remove(cube);
            Destroy(cube);
            breakCube.Invoke();
            if(cubes.Count == 0)
            {
                winGame.Invoke();
            }
        }
    }
    public void End()
    {
       endGame.Invoke();
    }
}
