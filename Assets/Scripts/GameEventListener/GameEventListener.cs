using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{

    [SerializeField] GameEvent gameEvent = null;
    public Action Action = null;
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
