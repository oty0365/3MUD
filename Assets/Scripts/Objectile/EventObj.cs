using System;
using System.Collections;
using UnityEngine;

public class EventObj : MonoBehaviour
{
    public float checkTime; 
    public float distance;
    [NonSerialized] public GameObject target;
    
    public bool CheckDistance(GameObject obj1, GameObject obj2, float distance)
    {
        return Vector2.Distance(obj1.transform.position, obj2.transform.position) < distance;
    }
}
