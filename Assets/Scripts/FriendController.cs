using UnityEngine;

public class FriendController : MonoBehaviour
{
    //This script is specifically for controlling the friend in the cutscenes
    private Animator _animator;
    private bool isPreparingCake;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        AdjustSortingLayer();
        BakeCake(isPreparingCake);
    }
    
    
    public void FlipScale(float x)
    {
        transform.localScale = new Vector2(x, 1.3f);
        //transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    private void AdjustSortingLayer()
    {
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
    
    public void BakeCake(bool isPreparingCake)
    {
        if (isPreparingCake)
        {
            _animator.Play("Friend_Back");
        }

        /*if (!isPreparingCake)
        {
            _animator.Play("Friend_Idle");
        }*/
    }

    public void PlayIdle()
    {
        _animator.Play("Friend_Idle");
    }
    
}
