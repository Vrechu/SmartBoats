using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GenerateAgents : MonoBehaviour
{
    private Bounds _bounds;

    [Header("Objects")]
    [SerializeField, Tooltip("Possible objecst to be created in the area.")]
    private GameObject[] gameObjectToBeCreated;

    [Space(10)]
    [Header("Variation")]
    [SerializeField]
    private Vector3 randomRotationMinimal;
    [SerializeField]
    private Vector3 randomRotationMaximal;

    //ADDED
    [SerializeField]
    private uint amountGenerated = 0;
    public Dictionary<uint, GameObject> livingAgents { get; private set; } = new Dictionary<uint, GameObject>();
    public event Action<uint> OnlivingAgentAdded;
    public event Action<uint> OnlivingAgentRemoved;

    private void Awake()
    {
        _bounds = GetComponent<Renderer>().bounds;
    }

    public KeyValuePair<uint, GameObject> CreateNewAgent()
    {
        GameObject created = Instantiate(gameObjectToBeCreated[Random.Range(0, gameObjectToBeCreated.Length)], GetRandomPositionInWorldBounds(), GetRandomRotation());
        created.transform.parent = transform;
        livingAgents.Add(++amountGenerated, created);
        return new KeyValuePair<uint, GameObject>(amountGenerated, created);
    }


    public void RemoveAgent(uint agentIndex)
    {
        DestroyImmediate(livingAgents[agentIndex]);
        livingAgents.Remove(agentIndex);
        OnlivingAgentRemoved?.Invoke(agentIndex);
    }


    /// <summary>
    /// Gets a random position delimited by the bounds, using its extends and center.
    /// </summary>
    /// <returns>Returns a random position in the bounds of the area.</returns>
    private Vector3 GetRandomPositionInWorldBounds()
    {
        Vector3 extents = _bounds.extents;
        Vector3 center = _bounds.center;
        return new Vector3(
            Random.Range(-extents.x, extents.x) + center.x,
            Random.Range(-extents.y, extents.y) + center.y,
            Random.Range(-extents.z, extents.z) + center.z
        );
    }
    /// <summary>
    /// Gets a random rotation (Quaternion) using the randomRotationMinimal and randomRotationMaximal.
    /// </summary>
    /// <returns>Returns a random rotation.</returns>
    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(Random.Range(randomRotationMinimal.x, randomRotationMaximal.x),
            Random.Range(randomRotationMinimal.y, randomRotationMaximal.y),
            Random.Range(randomRotationMinimal.z, randomRotationMaximal.z));
    }
}
