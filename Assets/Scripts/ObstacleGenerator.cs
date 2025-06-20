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
[Serializable]
public class GenerateObj
{
    public List<Objectile> spawnList;
    public GenerateType generateType;
}

public enum GenerateType
{
    RunesAndItems = 0,
    Obstacles ,
    Coins,
}

public class ObstacleGenerator : HalfSingleMono<ObstacleGenerator>
{
    public GenerateObj obstacles;
    public GenerateObj coins;
    public GenerateObj runesAndItems;

    private Dictionary<float, GenerateType> timeTables = new();
    

    public void StartSpawn()
    {
        StartCoroutine(SpawnFlow(obstacles));
        StartCoroutine(SpawnFlow(coins));
    }
    private IEnumerator SpawnFlow(GenerateObj generateSets)
    {
        while (true)
        {
            var index = UnityEngine.Random.Range(0, generateSets.spawnList.Count);
            var waitTime = generateSets.spawnList[index].spawnTime.GetRandom();
            var currentSpawnTime = Time.time + waitTime;
            var canSpawn = true;
            var conflictingTimes = new List<float>();

            foreach (var scheduledTime in timeTables.Keys)
            {
                if (Mathf.Abs(scheduledTime - currentSpawnTime) < 1f)
                {
                    var existingType = timeTables[scheduledTime];

                    if ((int)generateSets.generateType < (int)existingType)
                    {
                        conflictingTimes.Add(scheduledTime);
                    }
                    else
                    {
                        canSpawn = false;
                        break;
                    }
                }
            }

            if (canSpawn)
            {
                foreach (var conflictTime in conflictingTimes)
                {
                    timeTables.Remove(conflictTime);
                }
                timeTables.Add(currentSpawnTime, generateSets.generateType);
            }

            yield return new WaitForSeconds(waitTime);

            if (canSpawn)
            {
                if (timeTables.ContainsKey(currentSpawnTime) &&
                    timeTables[currentSpawnTime] == generateSets.generateType)
                {
                    ObjectPooler.Instance.Get(generateSets.spawnList[index], gameObject.transform.position, Vector3.zero);
                    timeTables.Remove(currentSpawnTime);
                }
            }
        }
    }
}
