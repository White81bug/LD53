using UnityEngine;

[CreateAssetMenu(fileName = "OxygenData", menuName = "OxygenSystem/OxygenData")]
public class OxygenData : ScriptableObject
{
    [Min(0f)] public float MaxOxygenAmount;
    [Min(0f)] public float CurrentOxygenAmount;
}
