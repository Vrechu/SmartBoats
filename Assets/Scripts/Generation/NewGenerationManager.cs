using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerationManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject[] boatPrefabs;
    [SerializeField] private GameObject[] piratePrefabs;
    [SerializeField] private GameObject[] boxPrefabs;

    [Header("Generation")]
    [SerializeField] private GenerateAgents boatGenerator;
    [SerializeField] private GenerateAgents pirateGenerator;
    [SerializeField] private GenerateAgents boxGenerator;

    [SerializeField] private GameObject startingBoat;
    [SerializeField] private GameObject startingPirate;

    [SerializeField] private uint boatStartAmount;
    [SerializeField] private uint pirateStartAmount;

    [Header("Boxes")]
    [SerializeField] private uint boxStartAmount;
    [SerializeField] private uint boxesPerTick;
    [SerializeField] private float boxSpawnrate = 0;
    private CountdownTimer boxSpawnTimer;

    [Header("Parenting and Mutation")]
    [SerializeField] private uint boatOffspringAmount = 1;
    [SerializeField] private uint pirateOffspringAmount = 1;
    [SerializeField] private float mutationFactor = 5;
    [SerializeField] private float mutationChance = 5;



    private void OnEnable()
    {
        boxSpawnTimer = new CountdownTimer(boxSpawnrate, boxSpawnrate, true);

        CheckValues();
    }

    private void Start()
    {
        StartSimulation();
        EventBus<BoatReproductionEvent>.Subscribe(GenerateBabyBoat);
        EventBus<PirateReproductionEvent>.Subscribe(GenerateBabyPirate);
    }

    private void OnDestroy()
    {
        EventBus<BoatReproductionEvent>.UnSubscribe(GenerateBabyBoat);
        EventBus<PirateReproductionEvent>.UnSubscribe(GenerateBabyPirate);
    }

    private void Update()
    {
        if (boxSpawnTimer.CountDown()) GenerateBoxes(boxesPerTick);
    }

    private void StartSimulation()
    {

        GenerateBabyAgent(startingBoat.GetComponent<BoatLogic>().GetData(), startingBoat.GetComponent<BoatLogic>().GetData(), boatPrefabs, boatGenerator, boatStartAmount);


        GenerateBabyAgent(startingPirate.GetComponent<PirateLogic>().GetData(), startingPirate.GetComponent<PirateLogic>().GetData(), piratePrefabs, pirateGenerator, pirateStartAmount);


        GenerateBoxes(boxStartAmount);
    }

    private void GenerateBabyAgent(AgentData parentAgent1, AgentData parentAgent2, GameObject[] prefabs, GenerateAgents generator, uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            KeyValuePair<uint, GameObject> babyAgentObject = generator.CreateNewAgent(prefabs);
            AgentLogic babyAgent = babyAgentObject.Value.GetComponent<AgentLogic>();
            if (babyAgent == null) return;

            babyAgent.Birth(parentAgent1, parentAgent2, babyAgentObject.Key);
            babyAgent.Mutate(mutationFactor, mutationChance);
            babyAgent.AwakeUp();
        }
    }


    private void GenerateBabyBoat(BoatReproductionEvent reproductionEvent)
    {
        GenerateBabyAgent(reproductionEvent.parent1, reproductionEvent.parent2, boatPrefabs, boatGenerator, boatOffspringAmount);
    }

    private void GenerateBabyPirate(PirateReproductionEvent reproductionEvent)
    {
        GenerateBabyAgent(reproductionEvent.parent1, reproductionEvent.parent2, piratePrefabs, pirateGenerator, pirateOffspringAmount);
    }

    private void GenerateBoxes(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            KeyValuePair<uint, GameObject> newBox = boxGenerator.CreateNewAgent(boxPrefabs);
        }
    }

    /// <summary>
    /// debug code
    /// </summary>
    private void CheckValues()
    {
        //generators
        if (boatGenerator == null) Debug.LogError("No boat generator");
        if (pirateGenerator == null) Debug.LogError("No pirate generator");
        if (boxGenerator == null) Debug.LogError("No box generator");

        //prefabs
        if (startingBoat == null) Debug.LogError("No starting boat!");
        if (startingPirate == null) Debug.LogError("No starting pirate!");
        if (boatPrefabs.Length < 1) Debug.LogError("No boat prefabs!");
        if (piratePrefabs.Length < 1) Debug.LogError("No pirate prefabs!");
        if (boxPrefabs.Length < 1) Debug.LogError("No box prefabs!");

        if (boatStartAmount < 1) Debug.LogError("Zero starting boats!");
        if (pirateStartAmount < 1) Debug.LogError("Zero starting pirates!");
        if (boxStartAmount < 1) Debug.LogError("Zero starting boxes!");

        if (boxesPerTick < 1) Debug.LogError("Zero boxes per tick!");
        if (boxSpawnrate <= 0) Debug.LogError("Box Countdown = 0");

        //offspring
        if (boatOffspringAmount < 1) Debug.LogError("Zero boat offspring!");
        if (pirateOffspringAmount < 1) Debug.LogError("Zero pirate offspring!");
        if (mutationChance <= 0) Debug.LogError("Zero mutation chance!");
        if (mutationFactor <= 0) Debug.LogError("Zero mutation factor!");

    }
}
