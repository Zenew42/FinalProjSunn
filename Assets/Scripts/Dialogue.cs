using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
    }

    public bool CanInteract()
    {
        Debug.Log("CanInteract");
        return true;
    }
}
