using UnityEngine;
using Random = UnityEngine.Random;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BoatLogic : AgentLogic
{
    [Header("Energy gain")]
    [SerializeField] private float boxEnergy = 2.0f;
    [SerializeField] private float pirateEnergy = -100.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Box"))
        {
            points += boxEnergy;
            Destroy(other.gameObject);
            AddEnergy(boxEnergy);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //This is a safe-fail mechanism. In case something goes wrong and the Boat is not destroyed after touching
            //a pirate, it also gets a massive negative number of points.
            points += pirateEnergy;
            AddEnergy(pirateEnergy);
        }
        if (other.gameObject.tag.Equals("Boat"))
        {
            if (!CanReproduce()) return;

            AgentLogic possibleMate = other.transform.GetComponent<AgentLogic>();
            if (possibleMate != null && possibleMate.CanReproduce()) GiveReproduction(possibleMate);
        }
    }
    protected override void GiveReproduction(AgentLogic mate)
    {
        base.GiveReproduction(mate);
        EventBus<BoatReproductionEvent>.Publish(new BoatReproductionEvent(GetData(), mate.GetData()));
    }

    protected override void SetReproductionWeight()
    {
        tempBoatWeight = 0;
        if (CanReproduce()) tempBoatWeight = boatWeight;
    }
}
