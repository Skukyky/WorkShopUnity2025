using System;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public int maxNombreBallToDestroy;
    public int nombreBallToDestroy;

    private void Start()
    {
        nombreBallToDestroy = maxNombreBallToDestroy;
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
            print("gg");
        }
        else
        {
            print(nombreBallToDestroy);
        }
    }
}
