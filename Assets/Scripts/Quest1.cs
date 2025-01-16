using System;
using UnityEngine;

public class Quest1 : MonoBehaviour
{
    public ReceiveAction[] ball;
    private int nombreBallToDestroy;
    private AudioInteractionmanager audioInteractionmanager;
    
    private void Start()
    {
        nombreBallToDestroy = ball.Length;
        audioInteractionmanager = GetComponent<AudioInteractionmanager>();
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
            audioInteractionmanager.useCristal();
        }
        else
        {
            print(nombreBallToDestroy);
        }
    }
}
