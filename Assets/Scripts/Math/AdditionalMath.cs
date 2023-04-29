using UnityEngine;

public static class AdditionalMath
{
    public static Transform FindClosestTransform(Transform position, Transform[] transforms)
    {
        Transform closestTransform = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform transform in transforms)
        {
            float distance = Vector3.Distance(position.position, transform.position);
            if (distance < closestDistance)
            {
                closestTransform = transform;
                closestDistance = distance;
            }
        }
        return closestTransform;
    }
}