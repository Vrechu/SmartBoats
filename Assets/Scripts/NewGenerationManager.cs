using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerationManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject[] boatPrefabs;
    [SerializeField]
    private GameObject[] PiratePrefabs;

    [SerializeField]
    private GenerateAgents boatGenerator;
    [SerializeField]
    private GenerateAgents pirateGenerator;
    [SerializeField] private uint boatStartAmount;
    [SerializeField] private uint pirateStartAmount;

    [Header("Parenting and Mutation")]
    [SerializeField]
    private float mutationFactor;
    [SerializeField]
    private float mutationChance;



    [SerializeField]
     private   GameObject startingBoat;
    [SerializeField]
    private GameObject startingPirate;


    private void Start()
    {
        StartSimulation();
    }

    private void StartSimulation()
    {
        for (int i = 0; i < boatStartAmount; i++)
        {
            GenerateBabyAgent(startingBoat.GetComponent<BoatLogic>().GetData(), startingBoat.GetComponent<BoatLogic>().GetData());
        }
    }

    private void GenerateBabyAgent(AgentData parentAgent1, AgentData parentAgent2)
    {
        KeyValuePair<uint, GameObject> babyAgentObject = boatGenerator.CreateNewAgent(boatPrefabs);
        AgentLogic babyAgent = babyAgentObject.Value.GetComponent<AgentLogic>();
        if (babyAgent == null) return;
        babyAgent.Birth(parentAgent1, parentAgent2, babyAgentObject.Key);
        babyAgent.Mutate(mutationFactor, mutationChance);
        babyAgent.AwakeUp();
    }
}
