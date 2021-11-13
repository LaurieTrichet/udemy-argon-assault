using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogScriptableObject", menuName = "ScriptableObjects/Dialog")]
public class DialogScriptableObject : ScriptableObject
{

    public string dialog = "";
    public AudioClip vo = null;

}
