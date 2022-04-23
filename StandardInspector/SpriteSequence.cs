using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sprite Sequence", menuName = "Sprite Animation/Sprite Sequence")]
public class SpriteSequence : ScriptableObject
{
    #region Inspector
    [Header("Sequence Settings")]
    [SerializeField] private float? _fpsOverride;
    [SerializeField] private bool _loopAnimation;

    [Space]
    [SerializeField] private List<string> _onInteruptEventListeners;

    [Space]
    [SerializeField] private List<SpriteFrame> _sequence;

    [Header("Sequence Generation")]
    [SerializeField] private List<Sprite> _generationSprites;

    private void GenerateSequenceFromSprites()
    {
        foreach (Sprite s in _generationSprites)
            _sequence.Add(GetBlankSpriteFrame(s));
        _generationSprites.Clear();
    }
    #endregion

    #region Fields
    private int _nextFrameIndex; 
    #endregion

    #region Properties
    public float? FpsOverride
    {
        get => _fpsOverride;
    }

    public bool LoopAnimation
    {
        get => _loopAnimation;
    }

    public List<string> OnInteruptEventListeners
    {
        get => _onInteruptEventListeners;
    }

    public bool HasNextFrame
    {
        get => _nextFrameIndex < _sequence.Count;
    }
    #endregion

    #region Constructor
    public SpriteSequence()
    {
        _nextFrameIndex = 0;
        _sequence = new List<SpriteFrame>();
        _generationSprites = new List<Sprite>();
    }
    #endregion

    #region Methods

    private SpriteFrame GetBlankSpriteFrame(Sprite frame = null)
    {
        return new SpriteFrame(frame);
    }

    public SpriteFrame GetNextFrame()
    {
        if (HasNextFrame)
            return _sequence[_nextFrameIndex++];
        else if (_loopAnimation)
        {
            _nextFrameIndex = 0;
            return _sequence[_nextFrameIndex++];
        }

        return null;
    }

    public void ResetAnimation()
    {
        _nextFrameIndex = 0;
    }
    #endregion
}
