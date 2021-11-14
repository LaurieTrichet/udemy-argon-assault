using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(TextMeshProUGUI))]
[TrackClipType(typeof(CaptionClip))]
public class CaptionTrack : TrackAsset
{
    public TMPro.TextMeshProUGUI captionTextUI = null;

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<CaptionTrackMixer>.Create(graph, inputCount);
    }
}
