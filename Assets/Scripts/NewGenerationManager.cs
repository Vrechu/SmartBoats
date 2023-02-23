using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerationManager : MonoBehaviour
{
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
            GenerateBabyBoat(startingBoat.GetComponent<BoatLogic>().GetData());
        }
        for (int i = 0; i < pirateStartAmount; i++)
        {
            GenerateBabyPirate(startingBoat.GetComponent<BoatLogic>().GetData());
        }
    }

    private void GenerateBabyBoat(AgentData parentBoat)
    {
        KeyValuePair<uint, GameObject> babyBoat1 = boatGenerator.CreateNewAgent();
        AgentLogic babyBoat = babyBoat1.Value.GetComponent<AgentLogic>();
        babyBoat.Birth(parentBoat, babyBoat1.Key);
        babyBoat.Mutate(mutationFactor, mutationChance);
        babyBoat.AwakeUp();
        Debug.Log("index: "+ babyBoat.GetData().index);
        Debug.Log("gen: " + babyBoat.GetData().generation);
    }

    private void GenerateBabyPirate(AgentData parentPirate)
    {
        AgentLogic babyPirate = pirateGenerator.CreateNewAgent().Value.GetComponent<AgentLogic>();
        babyPirate.Birth(parentPirate);
        babyPirate.Mutate(mutationFactor, mutationChance);
        babyPirate.AwakeUp();
    }
}
