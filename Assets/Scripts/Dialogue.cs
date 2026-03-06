using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset inkJson;
    
    public void Interact()
    {
        Debug.Log("Interact");
        DialogueManager.GetInstance().StartDialogue(inkJson);
    }

    public bool CanInteract()
    {
        Debug.Log("CanInteract");
        return true;
    }
}
