using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleAgent : Agent
{
    [SerializeField] Paddle _paddle;
    [SerializeField] Game game;
    bool hitBonus = true;
    private void Start()
    {
        game.breakCube.AddListener(OnBreakCube);
        game.winGame.AddListener(OnWin);
        game.endGame.AddListener(OnLose);
        _paddle.hitBall.AddListener(HitBall);
    }
    void HitBall()
    {
        if (hitBonus)
        {
            hitBonus = false;
            AddReward(0.1f);
        }
    }
    void OnBreakCube()
    {
        AddReward(1f);
        if (hitBonus)
        {
            AddReward(1f);
        }
        hitBonus = true;
    }
    void OnLose()
    {
        EndEpisode();
    }
    void OnWin()
    {
        AddReward(100f);
        EndEpisode();
    }
    public override void OnEpisodeBegin()
    {
        game.StartGame();
        _paddle.transform.localPosition = new Vector2(0, -3);
        _paddle.transform.localPosition = Vector2.zero;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 0)
        {
            return;
        }
        else if (actions.DiscreteActions[0] == 1)
        {
            _paddle.Left();
            AddReward(-0.0005f);
        }
        else if (actions.DiscreteActions[0] == 2)
        {
            _paddle.Right();
            AddReward(-0.0005f);
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discrete = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            discrete[0] = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            discrete[0] = 2;
        }
        else
        {
            discrete[0] = 0;
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (_paddle != null)
        {
            sensor.AddObservation(_paddle.transform.localPosition.x);
            sensor.AddObservation(_paddle.rb.velocity.x);
        }
        else
        {
            return;
        }
        if (game != null && game.ballObject != null)
        {
            sensor.AddObservation(game.ballObject.transform.localPosition.x);
            sensor.AddObservation(game.ballObject.transform.localPosition.y);
            sensor.AddObservation(game.ballObject.GetComponent<Rigidbody2D>().velocity.x);
            sensor.AddObservation(game.ballObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
