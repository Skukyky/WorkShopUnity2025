using System;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public ReceiveAction[] ball;
    private int nombreBallToDestroy;

    private void Start()
    {
        nombreBallToDestroy = ball.Length;
    }


    private void DestroyBall()
    {
        nombreBallToDestroy--;
    }

    public void updateBall()
    {
        DestroyBall();
        
        if (nombreBallToDestroy <= 0)
        {
            GetComponent<recupItem>().canRecupItem = true;
        }
        else
        {
            print(nombreBallToDestroy);
        }
    }
}
