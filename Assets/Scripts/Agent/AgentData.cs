using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This struct stores all genes / weights from an Agent.
/// It is used to pass this information along to other Agents, instead of using the MonoBehavior itself.
/// Also, it makes it easier to inspect since it is a Serializable struct.
/// </summary>
[Serializable]
public struct AgentData
{
    public uint generation;
    public uint index;

    public float lifespan;
    public float totalEnergySeconds;

    public int steps;
    public int rayRadius;
    public float sight;
    public float movingSpeed;

    public float boxWeight;
    public float boxDistanceFactor;
    public float boatWeight;
    public float boatDistanceFactor;
    public float enemyWeight;
    public float enemyDistanceFactor;

    public float edgeWeight;
    public Vector2 randomDirectionValue;

    public AgentData(uint generation, uint index,
        float lifespan, float totalEnergy,
        int steps, int rayRadius, float sight, float movingSpeed,
        float boxWeight, float boxDistanceFactor,
        float boatWeight, float boatDistanceFactor,
        float enemyWeight, float enemyDistanceFactor,
        float edgeWeight, Vector2 randomDirectionValue)
    {
        this.generation = generation;
        this.index = index;

        this.lifespan = lifespan;
        this.totalEnergySeconds = totalEnergy;

        this.steps = steps;
        this.rayRadius = rayRadius;
        this.sight = sight;
        this.movingSpeed = movingSpeed;

        this.boxWeight = boxWeight;
        this.boxDistanceFactor = boxDistanceFactor;
        this.boatWeight = boatWeight;
        this.boatDistanceFactor = boatDistanceFactor;
        this.enemyWeight = enemyWeight;
        this.enemyDistanceFactor = enemyDistanceFactor;

        this.edgeWeight = edgeWeight;
        this.randomDirectionValue = randomDirectionValue;
    }

    public AgentData(AgentData parent, uint index)
    {
        this.generation = parent.generation;
        this.index = index;

        this.lifespan = parent.lifespan;
        this.totalEnergySeconds = parent.totalEnergySeconds;

        this.steps = parent.steps;
        this.rayRadius = parent.rayRadius;
        this.sight = parent.sight;
        this.movingSpeed = parent.movingSpeed;

        this.boxWeight = parent.boxWeight;
        this.boxDistanceFactor = parent.boxDistanceFactor;
        this.boatWeight = parent.boatWeight;
        this.boatDistanceFactor = parent.boatDistanceFactor;
        this.enemyWeight = parent.enemyWeight;
        this.enemyDistanceFactor = parent.enemyDistanceFactor;
        
        this.edgeWeight = parent.edgeWeight;
        this.randomDirectionValue = parent.randomDirectionValue;
    }
}

