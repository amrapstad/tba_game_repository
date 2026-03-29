using UnityEngine;

public class HitboxTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.NPCTouch();
    }
}
