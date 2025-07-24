using UnityEngine;

public class Lanes : MonoBehaviour
{
    public Transform left, right, mid;

    private void Awake()
    {
        if (!left) left = transform.Find("Left");
        if (!right) right = transform.Find("Right");
        if (!mid) mid = transform.Find("Mid");
    }
}
