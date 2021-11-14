using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CaptionBehaviour : PlayableBehaviour
{
    public DialogScriptableObject dialogScriptableObject = null;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (AudioPlayer.Instance)
        {
        Debug.Log("OnBehaviourPlay");
            AudioPlayer.Instance.PlayAudioClip(dialogScriptableObject.vo);
        }
    }

}
