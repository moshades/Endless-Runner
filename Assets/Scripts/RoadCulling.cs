using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class RoadCulling : MonoBehaviour
{
    [SerializeField] private float cullingDis;
    [SerializeField] private Transform player;

    private void Start()
    {
        player = GameObject.Find("KyleRobot").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z < player.position.z - cullingDis)
        {
            gameObject.SetActive(false);
        }
    }
}
