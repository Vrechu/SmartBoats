using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PirateLogic : AgentLogic
{
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
            if (!CanReproduce()) return;

            AgentLogic possibleMate = other.transform.GetComponent<AgentLogic>();
            if (possibleMate != null && possibleMate.CanReproduce()) GiveReproduction(possibleMate);
        }

        if (other.gameObject.tag.Equals("Boat"))
        {
            points += boatEnergy;
            Destroy(other.gameObject);
            AddEnergy(boatEnergy);
        }
    }

    protected override void GiveReproduction(AgentLogic mate)
    {
        base.GiveReproduction(mate);
        EventBus<PirateReproductionEvent>.Publish(new PirateReproductionEvent(GetData(), mate.GetData()));
    }

    protected override void SetReproductionWeight()
    {
        tempEnemyWeight = 0;
        if (CanReproduce()) tempEnemyWeight = pirateWeight;
    }
}
