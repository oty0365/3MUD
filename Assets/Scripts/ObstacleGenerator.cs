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
    Obstacles,
    Coins,
}

public class ObstacleGenerator : HalfSingleMono<ObstacleGenerator>
{
    public GenerateObj obstacles;
    public GenerateObj coins;
    public GenerateObj runesAndItems;

    private Dictionary<float, GenerateType> timeTables = new();
    private List<Coroutine> activeCoroutines = new();

    public void StartSpawn()
    {
        StopAllSpawns();
        
        foreach (var obj in obstacles.spawnList)
        {
            var coroutine = StartCoroutine(SpawnIndividualObject(obj, obstacles.generateType));
            activeCoroutines.Add(coroutine);
        }
        
        foreach (var obj in coins.spawnList)
        {
            var coroutine = StartCoroutine(SpawnIndividualObject(obj, coins.generateType));
            activeCoroutines.Add(coroutine);
        }
        
        foreach (var obj in runesAndItems.spawnList)
        {
            var coroutine = StartCoroutine(SpawnIndividualObject(obj, runesAndItems.generateType));
            activeCoroutines.Add(coroutine);
        }
    }

    public void StopSpawn()
    {
        StopAllSpawns();
    }

    private void StopAllSpawns()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        activeCoroutines.Clear();
        timeTables.Clear();
    }

    private IEnumerator SpawnIndividualObject(Objectile obj, GenerateType generateType)
    {
        while (true)
        {
            var waitTime = obj.spawnTime.GetRandom();
            var currentSpawnTime = Time.time + waitTime;
            var canSpawn = true;
            var conflictingTimes = new List<float>();
            
            foreach (var scheduledTime in timeTables.Keys)
            {
                if (Mathf.Abs(scheduledTime - currentSpawnTime) < 1f)
                {
                    var existingType = timeTables[scheduledTime];

                    if ((int)generateType < (int)existingType)
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
                timeTables.Add(currentSpawnTime, generateType);
            }

            yield return new WaitForSeconds(waitTime);

            if (canSpawn)
            {
                if (timeTables.ContainsKey(currentSpawnTime) && timeTables[currentSpawnTime] == generateType)
                {
                    ObjectPooler.Instance.Get(obj.gameObject, gameObject.transform.position, Vector3.zero);
                    timeTables.Remove(currentSpawnTime);
                    Debug.Log($"Spawned: {obj.gameObject.name}, Type: {generateType}");
                }
            }
        }
    }
}