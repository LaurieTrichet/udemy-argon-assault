using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CaptionClip: PlayableAsset
{

    public DialogScriptableObject dialogScriptableObject = null;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var captionPlayable = ScriptPlayable<CaptionBehaviour>.Create(graph);
        var captionBehaviour = captionPlayable.GetBehaviour();

        captionBehaviour.dialogScriptableObject = dialogScriptableObject;

        return captionPlayable;
    }
}
