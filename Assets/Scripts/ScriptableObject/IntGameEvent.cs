using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntGameEvent", menuName = "ScriptableObjects/GameEvents/IntGameEvent")]
public class IntGameEvent : ScriptableObject
{
    public Action<int> action = null;

    public void Trigger(int value)
    {
        action?.Invoke(value);
    }

}
