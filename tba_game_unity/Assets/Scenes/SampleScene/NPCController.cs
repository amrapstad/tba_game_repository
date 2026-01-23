using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameEvents.current.onNPCTouch += TakeDamage;
        Debug.Log("NPCController has started and is listening for NPC touch events.");
    }

    private void TakeDamage()
    {
        // Remove the object when this trigger
        Destroy(gameObject);


        Debug.Log("NPC is taking damage!");
    }

    private void OnDestroy()
    {
        GameEvents.current.onNPCTouch -= TakeDamage;
        Debug.Log("NPCController has been destroyed and unsubscribed from NPC touch events.");
    }
}
