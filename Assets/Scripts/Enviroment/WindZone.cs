using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private Vector3 Direction = new Vector3(1,0,0); 
    [SerializeField] private float force = 1; 
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(Direction * force, ForceMode.Force);
    }
    
}