using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IT : MonoBehaviour
{
    public float radius = 3f;
    public bool isFocus = false;
    public bool hasInteracted = false;

    Transform player;
    public Transform interactionTransform;


    // Update is called once per frame
    void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, 
                interactionTransform.position);            
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public virtual void Interact()
    {
        //player.gameObject.GetComponent<Controller>()
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

    //Gizmo그리는 함수
    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;

        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
