using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntGameEventListener : MonoBehaviour
{

    [SerializeField] IntGameEvent gameEvent = null;
    public Action<int> Action = null;
    // Start is called before the first frame update


    private void OnEnable()
    {
        gameEvent.action += Action;
    }

    private void OnDisable()
    {
        gameEvent.action += Action;
    }
}
