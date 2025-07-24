using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float speedChangeLane;
    [SerializeField] private LogicManager logic;
    [SerializeField] private Transform left, right, mid;
    [SerializeField] private RagdollController playerRagdoll;

    private Animator anim;
    private Coroutine coroutineChangeLane;
    private CharacterController charController;
    private Vector3 movementVec = Vector3.forward;

    private float initPos;
    private bool isChangingLane = false;
    private float gravity = -9.8f;
    private int currentLane, nextLane;
    private int animationJumpID, animationLandID;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        playerRagdoll = GetComponentInChildren<RagdollController>();
    }

    void Start()
    {
        initPos = transform.position.z;
        animationJumpID = Animator.StringToHash("Jump");
        animationLandID = Animator.StringToHash("Grounded");
    }

    // Update is called once per frame
    void Update()
    {
        if (!logic.isGameOver)
        {
            if (!isChangingLane && Input.GetKeyDown(KeyCode.D))
            {
                nextLane++;
                if (nextLane > 1)
                {
                    nextLane = 1;
                }
                StartCoroutine(ChangeLane(GetLane()));
            }
            else if (!isChangingLane && Input.GetKeyDown(KeyCode.A))
            {
                nextLane--;
                if (nextLane < -1)
                {
                    nextLane = -1;
                }
                StartCoroutine(ChangeLane(GetLane()));
            }

            if (charController.isGrounded)
            {
                anim.SetBool(animationLandID, true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetTrigger(animationJumpID);
                    anim.SetBool(animationLandID, false);
                    movementVec.y = Mathf.Sqrt(jumpHeight * -1 * gravity);
                }
            }
            else
            {
                movementVec.y += gravity * Time.deltaTime;
            }

            logic.UpdateScore(initPos, transform.position.z);
            charController.Move(movementVec * Time.deltaTime * speed);
        }
    }

    Vector3 GetLane()
    {
        switch (nextLane)
        {
            case -1:
                return left.position;
            case 0:
                return mid.position;
            case 1:
                return right.position;
        }
        return Vector3.zero;
    }

    IEnumerator ChangeLane(Vector3 nextLane)
    {
        isChangingLane = true;
        this.currentLane = this.nextLane;
        float timer = Time.time;
        Vector3 currentPos = transform.position;

        while (transform.position.x != nextLane.x)
        {
            float x = Mathf.Lerp(currentPos.x, nextLane.x, (Time.time - timer) * speedChangeLane);
            transform.position = new Vector3(
                x,
                transform.position.y,
                transform.position.z
            );
            yield return null;
        }
        isChangingLane = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            playerRagdoll.EnableRagdoll();
            logic.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible")
        {
            logic.AddGold();
        }
    }
}
