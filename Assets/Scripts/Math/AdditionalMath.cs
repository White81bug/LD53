using System.Collections.Generic;
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

    public static Transform FindClosestTransform(Transform position, List<GameObject> gameObjects)
    {
        Transform[] transforms = new Transform[gameObjects.Count];
        for (int i = 0; i < gameObjects.Count; i++)
        {
            transforms[i] = gameObjects[i].GetComponent<Transform>();
        }
        return FindClosestTransform(position, transforms);
    }

    public static GameObject FindClosestGameObject(Transform position, List<GameObject> gameObjects)
    {
        return FindClosestTransform(position, gameObjects).gameObject;
    }
}