using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenerationOptionsScriptableObject", menuName = "ScriptableObjects/GenerationOptions")]
public class GenerationOptionsSO : ScriptableObject
{
    [Header("Objects")]
    [SerializeField] public GameObject[] boatPrefabs;
    [SerializeField] public GameObject[] piratePrefabs;
    [SerializeField] public GameObject[] boxPrefabs;

    [SerializeField] public GameObject startingBoat;
    [SerializeField] public GameObject startingPirate;

    [SerializeField] public uint boatStartAmount;
    [SerializeField] public uint pirateStartAmount;

    [Header("Boxes")]
    [SerializeField] public uint boxStartAmount;
    [SerializeField] public uint boxesPerTick;
    [SerializeField] public float boxSpawnrate = 0;

    [Header("Parenting and Mutation")]
    [SerializeField] public uint boatOffspringAmount = 1;
    [SerializeField] public uint pirateOffspringAmount = 1;
}
