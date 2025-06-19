using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FloatRange
{
    public float min;
    public float max;

    public FloatRange(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    public float GetRandom()
    {
        return UnityEngine.Random.Range(min, max);
    }
}


public class ObstacleGenerator : HalfSingleMono<ObstacleGenerator>
{
    public List<APoolingObject> spawnList;
    public FloatRange spawnDuration;
    

    public void StartSpawn()
    {
        StartCoroutine(SpawnFlow());
    }
    private IEnumerator SpawnFlow()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDuration.GetRandom());
            var index = UnityEngine.Random.Range(0, spawnList.Count);
            ObjectPooler.Instance.Get(spawnList[index], gameObject.transform.position, Vector3.zero);
        }
    }
}
