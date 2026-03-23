using UnityEngine;

public class FriendController : MonoBehaviour
{
    //This script is specifically for controlling the friend in the cutscenes
    private Animator _animator;
    private bool isPreparingCake;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        BakeCake(isPreparingCake);
    }
    
    
    public void FlipScale(float x)
    {
        transform.localScale = new Vector2(x, 1f);
        //transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    public void BakeCake(bool isPreparingCake)
    {
        if (isPreparingCake)
        {
            _animator.Play("Friend_Back");
        }
    }
    
}
