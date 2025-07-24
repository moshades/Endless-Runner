using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float originY;

    private bool initialized = false;

    private void OnEnable()
    {
        if (!initialized && transform.parent != null)
        {
            originY = transform.parent.position.y;
            initialized = true;
        }

        if (transform.parent != null)
        {
            transform.parent.position = new Vector3(
                transform.parent.position.x,
                originY,
                transform.parent.position.z
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            transform.parent.transform.position = new Vector3(
                transform.parent.transform.position.x,
                other.transform.position.y + 1.5f,
                transform.parent.transform.position.z
            );
        }

        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
