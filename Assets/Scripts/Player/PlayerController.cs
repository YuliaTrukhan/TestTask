using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 2f;
    private float runBlendValue = 0;
    private bool isRunning;

    public bool IsMoving { get; private set; }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetFloat("Forward", 0f);
            animator.SetFloat("Right", 0f);
            animator.SetBool("PlayerAttack", true);
            IsMoving = false;
        }
        else
        {
            animator.SetBool("PlayerAttack", false);
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            animator.SetFloat("Forward", vertical);
            animator.SetFloat("Right", horizontal);

            float movementY = rotationSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0f, movementY, 0f);
            IsMoving = vertical > 0f || vertical < 0f || horizontal > 0f || horizontal < 0f;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("StrongAttack");
        }
    }


    

}
