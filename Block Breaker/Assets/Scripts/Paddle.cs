using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float paddlePositionMin = 0f;
    [SerializeField] float paddlePositionMax = 16f;
    GameStatus gameStatus;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectsOfType<Ball>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.Equals(null) && FindObjectsOfType<Ball>().Length > 0)
        {
            ball = FindObjectsOfType<Ball>()[0];
        }
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), paddlePositionMin, paddlePositionMax);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (gameStatus.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        } else
        {
            return (Input.mousePosition.x / Screen.width * screenWidthInUnits);
        }
    }
}
