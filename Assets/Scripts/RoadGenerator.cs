using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private float spawnPosZ = 0;
    [SerializeField] private float roadLength;
    [SerializeField] private int roadNum;

    [SerializeField] private List<GameObject> listRoad;
    [SerializeField] private Transform roadPool;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject road;

    private void Awake()
    {
        listRoad = new List<GameObject>();

        for (int i = 0; i < roadNum; i++)
        {
            SpawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z > spawnPosZ - (roadLength * roadNum))
        {
            SpawnRoad();
        }
    }

    private GameObject GetRoad()
    {
        foreach(var road in listRoad)
        {
            if (!road.activeSelf)
            {
                road.SetActive(true);
                return road;
            }
        }
        if (listRoad.Count < roadNum)
        {
            var newRoad = Instantiate(road, roadPool, true);
            newRoad.SetActive(true);
            listRoad.Add(newRoad);
            return newRoad;
        }
        return null;
    }

    private void SpawnRoad()
    {
        var newRoad = GetRoad();
        if (newRoad == null)
            return;

        spawnPosZ += roadLength;
        newRoad.transform.position = new Vector3(
            road.transform.position.x,
            road.transform.position.y,
            spawnPosZ
        );
    }
}
