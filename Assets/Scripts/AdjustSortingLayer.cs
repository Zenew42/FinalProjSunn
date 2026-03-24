using UnityEngine;

public class AdjustSortingLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }
}
