using UnityEngine;
using Random = UnityEngine.Random;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BoatLogic : AgentLogic
{
    #region Static Variables
    private static float _boxPoints = 2.0f;
    private static float _piratePoints = -100.0f;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Box"))
        {
            points += _boxPoints;
            Destroy(other.gameObject);
            AddEnergy(_boxPoints);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //This is a safe-fail mechanism. In case something goes wrong and the Boat is not destroyed after touching
            //a pirate, it also gets a massive negative number of points.
            points += _piratePoints;
            AddEnergy(_piratePoints);
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
