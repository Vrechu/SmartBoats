using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CanonballLogic : MonoBehaviour
{
    public float _speed;
    public Vector3 _direction;
    public AgentLogic _parentLogic;
    public GameObject _parentObject;

    private void Update()
    {
        transform.Translate(_direction * _speed*Time.deltaTime);   
    }

    /*private void ontr (Collision other)
    {
        if (other.gameObject != _parentObject)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                _parentLogic.AddPoints();

                Destroy(other.gameObject);
                Debug.Log("ouch");
            }
            Destroy(this);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _parentObject)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                _parentLogic.AddPoints();

                Destroy(other.gameObject);
                Debug.Log("ouch");
            }
            Destroy(this.gameObject);
        }
    }
}
