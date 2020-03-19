
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        //Virtual - to be overwritten
    }

    void Update()
    {
        if(isFocus && !hasInteracted)
        {
            //Debug.Log("Interact");
            Interact();
            hasInteracted = true;
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
}
