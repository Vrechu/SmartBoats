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
    public int steps;
    public int rayRadius;
    public float sight;
    public float movingSpeed;
    public Vector2 randomDirectionValue;
    public float boxWeight;
    public float distanceFactor;
    public float boatWeight;
    public float boatDistanceFactor;
    public float enemyWeight;
    public float enemyDistanceFactor;
    //ADDED
    public float totalEnergy;
    public uint generation;
    public uint index;
    public float age;


    public AgentData(uint generation, uint index, int steps, int rayRadius, float sight, float movingSpeed, Vector2 randomDirectionValue, float boxWeight, float distanceFactor, float boatWeight, float boatDistanceFactor, float enemyWeight, float enemyDistanceFactor, float totalEnergy, float age)
    {
        this.steps = steps;
        this.rayRadius = rayRadius;
        this.sight = sight;
        this.movingSpeed = movingSpeed;
        this.randomDirectionValue = randomDirectionValue;
        this.boxWeight = boxWeight;
        this.distanceFactor = distanceFactor;
        this.boatWeight = boatWeight;
        this.boatDistanceFactor = boatDistanceFactor;
        this.enemyWeight = enemyWeight;
        this.enemyDistanceFactor = enemyDistanceFactor;
        //ADDED
        this.totalEnergy = totalEnergy;
        this.generation = generation;
        this.index = index;
        this.age = age;
    }

    public AgentData(AgentData parent, uint index)
    {
        this.steps = parent.steps;
        this.rayRadius = parent.rayRadius;
        this.sight = parent.sight;
        this.movingSpeed = parent.movingSpeed;
        this.randomDirectionValue = parent.randomDirectionValue;
        this.boxWeight = parent.boxWeight;
        this.distanceFactor = parent.distanceFactor;
        this.boatWeight = parent.boatWeight;
        this.boatDistanceFactor = parent.boatDistanceFactor;
        this.enemyWeight = parent.enemyWeight;
        this.enemyDistanceFactor = parent.enemyDistanceFactor;
        //ADDED
        this.totalEnergy = parent.totalEnergy;
        this.generation = parent.generation;
        this.index = index;
        this.age = parent.age;
    }
}

