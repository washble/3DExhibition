using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : Singleton<EffectSpawner>
{
    [SerializeField] private int initCrateCount = 5;

    [SerializeField] private SpawnPrefab[] spawnPrefabArray;
    [Serializable]
    private struct SpawnPrefab
    {
        [SerializeField] internal EffectType effectType;
        [SerializeField] internal GameObject prefab;
    }
    
    private Dictionary<EffectType, Queue<GameObject>> queueDictionary = new Dictionary<EffectType, Queue<GameObject>>(2);
    
    private void Start()
    {
        InitCreateInstantiate();
    }

    private void InitCreateInstantiate()
    {
        for (int i = 0; i < spawnPrefabArray.Length; i++)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int j = 0; j < initCrateCount; j++)
            {
                AddInstantiateToQueue(queue, spawnPrefabArray[i].prefab);
            }
            queueDictionary.Add(spawnPrefabArray[i].effectType, queue);
        }
    }
    
    private void AddInstantiateToQueue(Queue<GameObject> queue, GameObject prefab, 
        Transform parent = null, Vector3 position = default, Quaternion rotation = default)
    { 
        GameObject instantiate = Instantiate(prefab, position, rotation, parent);
        instantiate.name = prefab.name;
        instantiate.SetActive(false);
        queue.Enqueue(instantiate);
    }
    
    public GameObject Get(EffectType effectType)
    {
        Queue<GameObject> queue = queueDictionary[effectType];
        GameObject prefab;
        if (queue.Count <= 1)
        {
            prefab = queue.Dequeue();
            AddInstantiateToQueue(queue, prefab);
        }
        else
        {
            prefab = queue.Dequeue();
        }
        prefab.SetActive(true);
        
        return prefab;
    }

    public void Restore(EffectType effectType, GameObject prefab)
    {
        Queue<GameObject> queue = queueDictionary[effectType];
        prefab.SetActive(false);
        queue.Enqueue(prefab);
    }
}
