using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 2f;
    [Header("Acceleration")]
    [SerializeField] private float accelerationDampTime = 1f;
    [SerializeField] private float accelerationTime = 5f;
    [SerializeField] private float stopDampTime = 1f;
    [SerializeField] private float stopTime = 5f;
    [Header("Move")]
    [SerializeField] private float moveDampTime = 1f;
    [SerializeField] private float animationMoveTime = 10f;
    private float currentAccelerationDuration = 0f;

    public bool IsMoving { get; private set; }

    private void Update()
    {
        if (InputManager.Attack)
        {
            animator.SetFloat("Forward", 0f);
            animator.SetFloat("Right", 0f);
            animator.SetBool("PlayerAttack", true);
            IsMoving = false;
            StopPlayer();
        }
        else
        {
            Movement();
        }


        if (InputManager.StrongAttack)
        {
            animator.SetTrigger("StrongAttack");
        }
    }

    private void StartPlayer()
    {
        animator.SetFloat("Speed", 1f, accelerationDampTime, Time.deltaTime * accelerationTime) ;
    }
    
    private void StopPlayer()
    {
        animator.SetFloat("Speed", 0f, stopDampTime, Time.deltaTime * stopTime);
    }

    private void Movement()
    {
        animator.SetBool("PlayerAttack", false);
        var vertical = InputManager.VerticalAxis;
        var horizontal = InputManager.HorizontalAxos;

        animator.SetFloat("Forward", vertical, moveDampTime, Time.deltaTime * animationMoveTime);
        animator.SetFloat("Right", horizontal, moveDampTime, Time.deltaTime * animationMoveTime);
        Rotate();
        float movementY = rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(0f, movementY, 0f);

        IsMoving = vertical > 0f || vertical < 0f || horizontal > 0f || horizontal < 0f;

        if (IsMoving)
        {
            StartPlayer();
        }
        else
        {
            StopPlayer();
        }
    }

    private void Rotate()
    {
        float movementY = rotationSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(0f, movementY, 0f);
    }
}
