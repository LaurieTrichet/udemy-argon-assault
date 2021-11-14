using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CaptionTrackMixer : PlayableBehaviour
{

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var text = playerData as TMPro.TextMeshProUGUI;

        if (!text) { return;  }

        var captionText = "";
        var alpha = 0.0f;
        var inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            var input = playable.GetInput(i);
            var weight = playable.GetInputWeight(i);
            if (weight > 0.0f)
            {
                var captionPlayable = (ScriptPlayable<CaptionBehaviour>)input;
                var captionBehaviour = captionPlayable.GetBehaviour();
                captionText = captionBehaviour.dialogScriptableObject.dialog;
                alpha = weight;
            }
        }

        text.text = captionText;
        text.alpha = alpha;
    }
    //public override void OnBehaviourPlay(Playable playable, FrameData info)
    //{
    //    var inputCount = playable.GetInputCount();

    //    for (int i = 0; i < inputCount; i++)
    //    {
    //        var input = playable.GetInput(i);
    //        var weight = playable.GetInputWeight(i);
    //    Debug.Log("OnBehaviourPlay");
    //        if (weight > 0.0f)
    //        {
    //            var captionPlayable = (ScriptPlayable<CaptionBehaviour>)input;
    //            var captionBehaviour = captionPlayable.GetBehaviour();
    //            AudioPlayer.instance.PlayAudioClip(captionBehaviour.dialogScriptableObject.vo);
    //        }
    //    }
    //}

}
