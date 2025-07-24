using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Lanes lanes;
    [SerializeField] private GameObject[] obsList;

    private void Awake()
    {
        lanes = FindFirstObjectByType<Lanes>();
    }

    private void OnEnable()
    {
        if (!gameObject.activeSelf) return;

        int obsType = Random.Range(0, obsList.Length + 1);
        if (obsType < obsList.Length && !obsList[obsType].activeSelf)
        {
            obsList[obsType].SetActive(true);
            RandomLane(obsList[obsType]);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject child in obsList)
        {
            if (child.activeSelf)
            {
                child.SetActive(false);
            }
        }
    }

    private void RandomLane(GameObject obj)
    {
        int lane = Random.Range(0, 3);

        switch (lane)
        {
            case 0:
                obj.transform.position = new Vector3(
                    lanes.left.position.x,
                    transform.position.y,
                    transform.position.z
                );
                break;
            case 1:
                obj.transform.position = new Vector3(
                    lanes.right.position.x,
                    transform.position.y,
                    transform.position.z
                );
                break;
            case 2:
                obj.transform.position = new Vector3(
                    lanes.mid.position.x,
                    transform.position.y,
                    transform.position.z
                );
                break;
        }
    }
}
