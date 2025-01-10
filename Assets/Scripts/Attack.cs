using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    // Damage que fait l'arme
    public int damage = 1;

    // Détermine quel Layer on peut toucher
    public LayerMask layerMask;

    // Est-ce que l'arme est en train d'être utilisée ?
    public bool isAttacking = false;

    Collider playerCollider;
    public PlayerFPS playerFPS;


    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        playerCollider = null;
    }

    private void Update()
    {

        if (!isAttacking)
        {
            return;
        }
        
        // Inflige les dégâts
        playerFPS.GetComponent<receiveDamagePlayer>().GetDamage(damage);
        print(damage);
        StopAttack();
        return;
    }
}