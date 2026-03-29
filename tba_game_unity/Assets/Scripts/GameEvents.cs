using UnityEngine;
using System;
using System.Collections;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onNPCTouch;
    public void NPCTouch()
    {
        if (onNPCTouch != null)
        {
            onNPCTouch();
        }
    }
}
