using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    
    [SerializeField] private AudioSource footStep;
    private float stepDelay = 0.1f;
    
    [SerializeField] private GameObject selfInteractable;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PauseController.isGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);

        if (rb.linearVelocityX > 0 || rb.linearVelocityX < 0 || rb.linearVelocityY > 0 || rb.linearVelocityY < 0)
        {
            FootStepAudio(footStep);
        }

        FlipScale();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
        
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
    }

    private void FootStepAudio(AudioSource audio)
    {
        if (audio.isPlaying) return;

        audio.Play();

    }

    public void SelfInteract()
    {

        if (!selfInteractable.activeSelf)
        {
            selfInteractable.SetActive(true);
        }
        else
        {
            selfInteractable.SetActive(false);
        }
    }

    private void FlipScale()
    {
        if (rb.linearVelocityX < 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (rb.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

}