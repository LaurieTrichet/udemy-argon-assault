using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvents/GameEvent")]
public class GameEvent : ScriptableObject
{
    public Action action = null;

    public void Trigger()
    {
        action?.Invoke();
    }

}
