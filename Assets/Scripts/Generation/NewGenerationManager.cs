using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerationManager : MonoBehaviour
{

    [SerializeField] private GenerationOptionsSO options;
    [SerializeField] private MutationOptionsSO mutations;

    [Header("Generators")]
    [SerializeField] private GenerateAgents boatGenerator;
    [SerializeField] private GenerateAgents pirateGenerator;
    [SerializeField] private GenerateAgents boxGenerator;

    private CountdownTimer boxSpawnTimer;

    private void OnEnable()
    {
        boxSpawnTimer = new CountdownTimer(options.boxSpawnrate, options.boxSpawnrate, true);

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
        if (boxSpawnTimer.CountDown()) GenerateBoxes(options.boxesPerTick);
    }

    private void StartSimulation()
    {
        GenerateBabyAgent(options.startingBoat.GetComponent<BoatLogic>().GetData(), 
            options.startingBoat.GetComponent<BoatLogic>().GetData(), 
            options.boatPrefabs, boatGenerator, options.boatStartAmount);

        GenerateBabyAgent(options.startingPirate.GetComponent<PirateLogic>().GetData(), 
            options.startingPirate.GetComponent<PirateLogic>().GetData(), 
            options.piratePrefabs, pirateGenerator, options.pirateStartAmount);

        GenerateBoxes(options.boxStartAmount);
    }

    private void GenerateBabyAgent(AgentData parentAgent1, AgentData parentAgent2, 
        GameObject[] prefabs, GenerateAgents generator, uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            KeyValuePair<uint, GameObject> babyAgentObject = generator.CreateNewAgent(prefabs);
            AgentLogic babyAgent = babyAgentObject.Value.GetComponent<AgentLogic>();
            if (babyAgent == null) return;

            babyAgent.Birth(parentAgent1, parentAgent2, babyAgentObject.Key);
            babyAgent.Mutate(mutations);
            babyAgent.AwakeUp();
        }
    }


    private void GenerateBabyBoat(BoatReproductionEvent reproductionEvent)
    {
        GenerateBabyAgent(reproductionEvent.parent1, reproductionEvent.parent2, options.boatPrefabs, boatGenerator, options.boatOffspringAmount);
    }

    private void GenerateBabyPirate(PirateReproductionEvent reproductionEvent)
    {
        GenerateBabyAgent(reproductionEvent.parent1, reproductionEvent.parent2, options.piratePrefabs, pirateGenerator, options.pirateOffspringAmount);
    }

    private void GenerateBoxes(uint amount)
    {
        for (int i = 0; i < amount; i++)
        {
            KeyValuePair<uint, GameObject> newBox = boxGenerator.CreateNewAgent(options.boxPrefabs);
        }
    }

    /// <summary>
    /// debug code
    /// </summary>
    private void CheckValues()
    {
        if (options == null) Debug.LogError("No options!");
        if (mutations == null) Debug.LogError("No mutations!");
        //generators
        if (boatGenerator == null) Debug.LogError("No boat generator!");
        if (pirateGenerator == null) Debug.LogError("No pirate generator!");
        if (boxGenerator == null) Debug.LogError("No box generator!");

        //prefabs
        if (options.startingBoat == null) Debug.LogError("No starting boat!");
        if (options.startingPirate == null) Debug.LogError("No starting pirate!");
        if (options.boatPrefabs.Length < 1) Debug.LogError("No boat prefabs!");
        if (options.piratePrefabs.Length < 1) Debug.LogError("No pirate prefabs!");
        if (options.boxPrefabs.Length < 1) Debug.LogError("No box prefabs!");

        if (options.boatStartAmount < 1) Debug.LogError("Zero starting boats!");
        if (options.pirateStartAmount < 1) Debug.LogError("Zero starting pirates!");
        if (options.boxStartAmount < 1) Debug.LogError("Zero starting boxes!");

        if (options.boxesPerTick < 1) Debug.LogError("Zero boxes per tick!");
        if (options.boxSpawnrate <= 0) Debug.LogError("Box Countdown = 0!");

        //offspring
        if (options.boatOffspringAmount < 1) Debug.LogError("Zero boat offspring!");
        if (options.pirateOffspringAmount < 1) Debug.LogError("Zero pirate offspring!");

    }
}
