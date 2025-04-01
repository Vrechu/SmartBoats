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
    public float _maxTravelDistance;
    private float _distanceTravelled = 0;

    private void Update()
    {
        float frameTravelDistance = _speed * Time.deltaTime;
        Move(frameTravelDistance);
        MeasureDistanceTraveled(frameTravelDistance);
    }

    private void Move(float distance)
    {
        transform.Translate(_direction * distance);
    }

    private void MeasureDistanceTraveled(float distance)
    {
        _distanceTravelled += distance;
        if (_distanceTravelled > _maxTravelDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _parentObject)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                _parentLogic.AddPoints();

                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    
}
