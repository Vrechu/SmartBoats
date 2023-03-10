using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutationOptionsScriptableObject", menuName = "ScriptableObjects/MutationOptions")]
public class MutationOptionsSO : ScriptableObject
{
    [SerializeField] public float mutationChance = 5;

    [Header("Mutationfactors")]
    public float lifespan;
    public float totalEnergy;
    public float steps;
    public float rayRadius;
    public float sight;
    public float moveSpeed;
    public float boxWeight;
    public float boxDistanceFactor;
    public float boatWeight;
    public float boatDistanceFactor;
    public float pirateWeight;
    public float pirateDistanceFactor;
    public float edgeWeight;
    public float randomDirection;
}
