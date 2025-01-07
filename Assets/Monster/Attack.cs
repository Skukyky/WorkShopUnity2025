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

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    // Quand MeleeWeapon entre en collision avec objet
    private void OnTriggerEnter(Collider other)
    {

        if (!isAttacking)
        {
            return;
        }

        // Vérifie le LayerMask
        if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        // Vérifie si l'objet peut recevoir des dégâts
        ReceiveDamage target = other.GetComponent<ReceiveDamage>();
        if (target == null)
        {
            return;
        }

        // Inflige les dégâts
        target.GetDamage(damage);
    }
}