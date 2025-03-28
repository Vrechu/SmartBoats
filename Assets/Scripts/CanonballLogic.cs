using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonballLogic : MonoBehaviour
{
    public float _speed;
    public Vector3 _direction;
    public AgentLogic _parent;

    private void Update()
    {
        transform.Translate(_direction * _speed*Time.deltaTime);   
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Enemy") 
            && other.gameObject != _parent)        {
            _parent.AddPoints();

            Destroy(other.gameObject);
            Destroy(this);
            Debug.Log("ouch");
        }
    }
}
