using UnityEngine;
using UnityEngine.Serialization;

public class HeadBobSystem : MonoBehaviour
{
    [Range(0.001f, 0.01f)]
    public float amount = 0.002f;
    
    [Range(1f, 30f)]
    public float frequency = 10.0f;
    
    [Range(10f, 100f)]
    public float smooth = 10f;

    private Vector3 _startPos;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _startPos = transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadbobTrigger();
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0) StartHeadBob();
    }

    private Vector3 StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp (pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp (pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += pos;
        
        return pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == _startPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, 1*Time.deltaTime);
    }
    
    
    
}
