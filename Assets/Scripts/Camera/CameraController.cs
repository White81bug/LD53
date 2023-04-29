using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam; 
    public Transform target; 
    
    [SerializeField]private float maxVerticalRotation = 70;
    [SerializeField]private float minVerticalRotation = 40;
    
    public Vector3 previousPosition;
    
    private const float MaxDistance = 15;
    private const float MinDistance = 10;
    [Range(MinDistance,MaxDistance)] [SerializeField] private float distanceToTarget = 10;

    [Range(0.1f, 0.2f)] [SerializeField]
    private float distanceChangeScale = 0.18f;
    [SerializeField] public Transform Player;
    
    private Quaternion rotOffset;
    
    [SerializeField][Range(1, 200f)] public float rotSpeed = 95;
    
    private Vector3  MousePos;
    private float MyAngle = 0F;
    
    void Start()
    {
        cam = GetComponent<Camera>();
        
        if ((distanceToTarget = PlayerPrefs.GetFloat("CameraDistance")) == 0)
            distanceToTarget = Vector3.Distance(transform.position,Player.position);
        
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

        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.position = target.position;
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis,
            Space.World); // <— This is what makes it work!
            
        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            
        var a = transform.rotation;
        a.eulerAngles = new Vector3(Math.Clamp(transform.rotation.eulerAngles.x, minVerticalRotation, maxVerticalRotation),a.eulerAngles.y,0) ;
        transform.rotation = a;
            
        previousPosition = newPosition;
        SaveChanges();
    }
    
}
