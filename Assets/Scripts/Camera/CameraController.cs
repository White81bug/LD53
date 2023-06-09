using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam; 
    public Transform target; 
    
    [SerializeField]private float maxVerticalRotation = 70;
    [SerializeField]private float minVerticalRotation = 40;
    
    public Vector3 previousPosition;
    
    private const float MaxDistance = 25;
    private const float MinDistance = 10;
    [Range(MinDistance,MaxDistance)] [SerializeField] private float distanceToTarget = 10;

    [Range(0.1f, 0.2f)] [SerializeField]
    private float distanceChangeScale = 0.18f;
    
    private Quaternion rotOffset;
    
    [SerializeField][Range(1, 200f)] public float rotSpeed = 95;
    
    private Vector3  MousePos;
    private float MyAngle = 0F;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        
        if ((distanceToTarget = PlayerPrefs.GetFloat("CameraDistance")) == 0)
            distanceToTarget = Vector3.Distance(transform.position,target.position);
        
        //Установка сохраненных значений
        rotOffset.x = PlayerPrefs.GetFloat("xOffset");
        rotOffset.y = PlayerPrefs.GetFloat("yOffset");
        rotOffset.z = PlayerPrefs.GetFloat("zOffset");
        rotOffset.w = PlayerPrefs.GetFloat("wOffset");
        
        transform.rotation = rotOffset;
    }

    private void SaveChanges()
    {
        PlayerPrefs.SetFloat("xOffset", transform.rotation.x);
        PlayerPrefs.SetFloat("yOffset", transform.rotation.y);
        PlayerPrefs.SetFloat("zOffset", transform.rotation.z);
        PlayerPrefs.SetFloat("wOffset", transform.rotation.w);
    }

    public void ChangeDistance(float value)
    {
        distanceToTarget -= value * distanceChangeScale;
        distanceToTarget = Math.Clamp(distanceToTarget,MinDistance,MaxDistance);
        PlayerPrefs.SetFloat("CameraDistance", distanceToTarget);
    }

    public void moveToTarget()
    {
        transform.position = target.position;
        transform.Translate(new Vector3(0, 0, -distanceToTarget));
    }

    public void Rotate()
    {
        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3  direction= previousPosition - newPosition;
 
        float rotationAroundYAxis = -direction.x  *rotSpeed; // camera moves horizontally
        float rotationAroundXAxis = direction.y *rotSpeed; // camera moves vertically camera moves vertically

        var a = transform;

        transform.eulerAngles += new Vector3(1, 0, 0) * rotationAroundXAxis;
        transform.eulerAngles = new Vector3(Math.Clamp(transform.rotation.eulerAngles.x, minVerticalRotation, maxVerticalRotation),a.eulerAngles.y,0) ;
        transform.eulerAngles += new Vector3(0, 1, 0) * rotationAroundYAxis;
        transform.eulerAngles = new Vector3(Math.Clamp(transform.rotation.eulerAngles.x, minVerticalRotation, maxVerticalRotation),a.eulerAngles.y,0) ;
            
        previousPosition = newPosition;
        SaveChanges();
    }
    
}
