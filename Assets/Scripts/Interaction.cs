using UnityEngine;
using UnityEngine.Serialization;


public class Interaction : MonoBehaviour
{
    //Port�e du tir
    public float weaponRange = 200f;

    //La cam�ra
    private Camera _fpsCam;

    //D�termine sur quel Layer on peut tirer
    public LayerMask layerMask;
    



    // Start is called before the first frame update
    void Start()
    {
        _fpsCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frameS
    void Update()
    {

        // V�rifie si le joueur a press� le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : v�rifie si suffisament de temps s'est �coul� pour pouvoir tirer � nouveau
        if (Input.GetButtonDown("Interact"))
        {

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Cr�e un vecteur au centre de la vue de la cam�ra
            Vector3 rayOrigin = _fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a touch�
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, _fpsCam.transform.forward,out hit, weaponRange, layerMask))
            {
                if (hit.collider.gameObject.layer == 6)
                    hit.collider.GetComponent<recupItem>().recupItems();
                else if (hit.collider.gameObject.layer == 3)
                    hit.collider.GetComponent<OpenGate>().OnPress();
                else if (hit.collider.gameObject.layer == 8)
                {
                    hit.collider.GetComponent<RefullSpaceShip>().AddEnergy(gameObject);
                }
                
            }
        }
    }
}
