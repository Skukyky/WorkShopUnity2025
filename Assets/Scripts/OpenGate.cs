using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public bool IsActive = false;
    private Vector3 rotation;
    public OpenGate Open;
    public Game2 PillonneGame2;
    private AudioInteractionmanager audioInteractionmanager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = transform.GetChild(0).localEulerAngles;
        UpdateRotation();
        audioInteractionmanager = GetComponent<AudioInteractionmanager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPress()
    {
        audioInteractionmanager.useLeviert();
        IsActive = !IsActive;
        UpdateRotation();
        Open.Replicate();
    }

    public void UpdateRotation()
    {
        if (IsActive)
        {
            //transform.GetChild(0).localRotation = Quaternion.Euler(-rotation.x,0,rotation.z + 45);
            transform.GetChild(0).localEulerAngles = new Vector3(rotation.x, rotation.y, rotation.z + 45);
        }
        else
        {
            //transform.GetChild(0).localRotation = Quaternion.Euler(-rotation.x,0,rotation.z - 45);
            transform.GetChild(0).localEulerAngles = new Vector3(rotation.x, rotation.y, rotation.z - 45);
        }
        PillonneGame2.Verify();
    }


    public void Replicate()
    {
        IsActive = !IsActive;
        UpdateRotation();
    }
}
