using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ShootAction : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;

    //Portée du tir
    public float weaponRange = 200f;

    //Force de l'impact du tir
    public float hitForce = 100f;

    //La caméra
    private Camera _fpsCam;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;

    //Float : mémorise le temps du prochain tir possible
    private float _nextFire;

    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;

    //Le taux de surchauffe actuelle, maximale et le taux de rafréchissement
    private float _overHeatingMax = 100f;
    [HideInInspector] public float overHeating = 1;
    public float overHeatingRate;
    public float refreshrate;
    private bool _overHeat;

    Quaternion lookRifle;


    // Start is called before the first frame update
    void Start()
    {
        //Référence de la caméra. GetComponentInParent<Camera> permet de chercher une Camera
        //dans ce GameObject et dans ses parents.
        _fpsCam = GetComponentInParent<Camera>();
        lookRifle = transform.localRotation;
    }

    // Update is called once per frameS
    void Update()
    {
        //permet de rafraichir l'arme
        if (overHeating > 0)
        {
            overHeating -= Time.deltaTime * refreshrate;
        }
        else
        {
            _overHeat = false;
        }

        if (Time.time >= _nextFire) transform.localRotation = lookRifle;

        // Vérifie si le joueur a pressé le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire && overHeating < _overHeatingMax && !_overHeat)
        {
            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            _nextFire = Time.time + fireRate;
            overHeating += overHeatingRate;
            if (overHeating > _overHeatingMax)
            {
                overHeating = _overHeatingMax;
                _overHeat = true;
            }

            ShootAnimation();


            print(_nextFire);

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;


            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(rayOrigin, _fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {

                //S'assure que la cible touchée a un composant ReceiveAction
                if (hit.collider.gameObject.GetComponent<ReceiveDamage>() != null)
                {
                    //Envoie les dommages à la cible
                    hit.collider.gameObject.GetComponent<ReceiveDamage>().GetDamage(gunDamage);
                }

                if (hit.collider.gameObject.GetComponentInChildren<ReceiveAction>() != null)
                {
                    //Envoie les dommages à la cible
                    hit.collider.gameObject.GetComponentInChildren<ReceiveAction>().GetDamage(gunDamage);
                }

            }
        }
    }

    void ShootAnimation()
    {
        transform.localRotation =
            Quaternion.Euler(lookRifle.eulerAngles.x, lookRifle.eulerAngles.y, lookRifle.eulerAngles.z + 30f);
    }
}