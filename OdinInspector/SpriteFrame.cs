using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable, InlineProperty, LabelWidth(15)]
public class SpriteFrame
{
    #region Inspector
    [HorizontalGroup("row1", 50, PaddingRight = 15), VerticalGroup("row1/left", PaddingBottom = 5)]
    [PreviewField(50, ObjectFieldAlignment.Left), HideLabel]
    [SerializeField, OdinSerialize]
    private Sprite _frame;

    [VerticalGroup("row1/right", PaddingTop = 5)]
    [LabelWidth(150)]
    [SerializeField, OdinSerialize]
    private bool _overrideFrameDuration;

    [VerticalGroup("row1/right")]
    [HideLabel, EnableIf("_overrideFrameDuration")]
    [SerializeField, OdinSerialize]
    private float _frameDurationOverride;

    [ListDrawerSettings(Expanded = true)]
    [SerializeField, OdinSerialize]
    private List<string> _onFrameEventListeners;
    #endregion

    #region Constructor
    public SpriteFrame(Sprite frame)
    {
        _frame = frame;
        _onFrameEventListeners = new List<string>();
    }
    #endregion

    #region Properties
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
    #endregion
}