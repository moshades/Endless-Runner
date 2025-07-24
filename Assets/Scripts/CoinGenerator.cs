using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Lanes lanes;

    private void Awake()
    {
        lanes = FindFirstObjectByType<Lanes>();
    }

    private void OnEnable()
    {
        ReactivateAll(gameObject);

        if (gameObject.activeSelf)
        {
            int lane = UnityEngine.Random.Range(0, 3);
            if (lane == 0)
            {
                transform.position = new Vector3(
                    lanes.left.position.x,
                    transform.position.y,
                    transform.position.z
                );
            }
            else if(lane == 1)
            {
                transform.position = new Vector3(
                    lanes.right.position.x,
                    transform.position.y,
                    transform.position.z
                );
            }
            else
            {
                transform.position = new Vector3(
                    lanes.mid.position.x,
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }

    private void ReactivateAll(GameObject parent)
    {
        parent.SetActive(true);

        foreach(Transform child in parent.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(true);
            }
            ReactivateAll(child.gameObject);
        }
    }
}
