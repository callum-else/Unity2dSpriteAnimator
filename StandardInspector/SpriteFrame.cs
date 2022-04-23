using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpriteFrame
{
    [SerializeField] private Sprite _frame;
    [SerializeField] private bool _overrideFrameDuration;
    [SerializeField] private float _frameDurationOverride;
    [SerializeField] private List<string> _onFrameEventListeners;

    public SpriteFrame(Sprite frame)
    {
        _frame = frame;
        _onFrameEventListeners = new List<string>();
    }

    public Sprite Frame
    {
        get => _frame;
    }

    public float? FrameDurationOverride
    {
        get => _overrideFrameDuration
            ? (float?)_frameDurationOverride
            : null;
    }

    public List<string> OnFrameEventListeners
    {
        get => _onFrameEventListeners;
    }
}