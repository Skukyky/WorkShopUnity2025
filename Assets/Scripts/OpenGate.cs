using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public bool IsActive = false;
    private Vector3 rotation;
    public OpenGate Open;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = transform.GetChild(0).localEulerAngles;
        UpdateRotation();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPress()
    {
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
    }


    public void Replicate()
    {
        IsActive = !IsActive;
        UpdateRotation();
    }
}
