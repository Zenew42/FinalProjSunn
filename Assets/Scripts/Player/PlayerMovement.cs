using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Animator animator;
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private AudioSource footStep;
    
    public GameObject tableDialogue;
    //public Animator noHatAnimator;
    public Animator hatAnimator;
    
    private bool facingLeft;
    public bool hasHat;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        AdjustSortingLayer();
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

        if (hasHat)
        {
            animator.SetBool("hasHat", true);
        }
        //animator.SetFloat("InputX", moveInput.x);
        //animator.SetFloat("InputY", moveInput.y);
        
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            
            if (!hasHat)
            {
                animator.SetBool("hasHat", true);
            }
            //animator.SetFloat("LastInputX", moveInput.x);
            //animator.SetFloat("LastInputY", moveInput.y);
        }
    }

    private void AdjustSortingLayer()
    {
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    private void FootStepAudio(AudioSource audio)
    {
        if (audio.isPlaying) return;

        audio.Play();

    }

    public void SelfInteract()
    {

        if (!tableDialogue.activeSelf)
        {
            tableDialogue.SetActive(true);
        }
        else
        {
            tableDialogue.SetActive(false);
        }
    }

    private void FlipScale()
    {
        if (rb.linearVelocityX < 0)
        {
            transform.localScale = new Vector2(1.3f, 1.3f);
            facingLeft = false;
        }
        else if (rb.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(-1.3f, 1.3f);
            facingLeft = true;
        }
    }

    public void isLeft()
    {
        if (facingLeft) return;
        transform.localScale = new Vector2(-1.3f, 1.3f);
    }

}