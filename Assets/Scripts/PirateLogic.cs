using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PirateLogic : AgentLogic
{
    #region Static Variables
    private static float _boxPoints = 0.1f;
    private static float _boatPoints = 5.0f;
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
            if (!CanReproduce()) return;

            AgentLogic possibleMate = other.transform.GetComponent<AgentLogic>();
            if (possibleMate != null && possibleMate.CanReproduce()) GiveReproduction(possibleMate);
        }

        if (other.gameObject.tag.Equals("Boat"))
        {
            points += _boatPoints;
            Destroy(other.gameObject);
            AddEnergy(_boatPoints);
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
        if (CanReproduce()) tempEnemyWeight = enemyWeight;
    }
}
