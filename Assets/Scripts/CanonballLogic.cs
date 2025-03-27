using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonballLogic : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private GameObject parent;

    private void Update()
    {
        transform.Translate(direction * speed*Time.deltaTime);   
    }

    private void OnCollisionEnter(Collision other)
    {
        PirateLogic logic;
        if (other.gameObject.tag.Equals("Enemy") 
            && other.gameObject != parent)
        {
            if ( parent.TryGetComponent<PirateLogic>(out logic))
            {
                logic.AddPoints();
            }
            else 
            Destroy(other.gameObject);
        }
    }
}
