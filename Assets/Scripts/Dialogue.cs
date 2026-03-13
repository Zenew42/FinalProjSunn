using UnityEngine;

public class Dialogue : MonoBehaviour, IInteractable
{
    [SerializeField] private TextAsset inkJson;
    
    public void Interact()
    {
        Debug.Log("Interact");
        if (DialogueManager.CanContinueToNextLine == true)
        {
            DialogueManager.GetInstance().StartDialogue(inkJson);
        }
        else
        {
            DialogueManager.SkippingDialogue = true;
        }
    }

    public bool CanInteract()
    {
        Debug.Log("CanInteract");
        return true;
    }
}
