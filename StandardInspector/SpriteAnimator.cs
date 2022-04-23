using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{
    [Header("Core Animator Settings")]
    [SerializeField] private int _framesPerSecond;
    [SerializeField] private UnityEvent<Sprite> _onSpriteChanged;

    private bool _isAnimating;
    private float _frameRate;
    private float _targetTime;
    private float _lastFrameTime;
    private SpriteSequence _currentSequence;
    private SpriteFrame _currentFrame;
    private Dictionary<string, List<Action>> _eventListeners;

    protected void Start()
    {
        _frameRate = 1f / _framesPerSecond;
        _eventListeners = new Dictionary<string, List<Action>>();
        Initialise();
    }

    protected virtual void Initialise() { }

    private void FixedUpdate()
    {
        if (_isAnimating && _targetTime < Time.time)
        {
            _currentFrame = _currentSequence.GetNextFrame();

            if (_currentFrame != null)
            {
                _lastFrameTime = Time.time;

                _onSpriteChanged.Invoke(_currentFrame.Frame);
                foreach (string l in _currentFrame.OnFrameEventListeners)
                    InvokeListener(l);

                _targetTime = _lastFrameTime + (_currentFrame.FrameDurationOverride
                    ?? _currentSequence.FpsOverride ?? _frameRate);
            }
            else
                _isAnimating = false;
        }
    }

    public void Play(SpriteSequence spriteSequence)
    {
        if (_isAnimating)
        {
            _isAnimating = false;
            foreach (string l in _currentSequence.OnInteruptEventListeners)
                InvokeListener(l);
            _currentSequence.ResetAnimation();
        }
        _currentSequence = spriteSequence;
        _isAnimating = true;
    }

    private void InvokeListener(string name)
    {
        if (_eventListeners.ContainsKey(name))
            foreach (Action a in _eventListeners[name])
                a.Invoke();
    }

    public void AddListener(string name, Action action)
    {
        if (_eventListeners.ContainsKey(name))
            _eventListeners[name].Add(action);
        else
            _eventListeners.Add(name, new List<Action> { action });
    }

    public bool RemoveListener(string name, Action action)
    {
        if (_eventListeners.ContainsKey(name))
        {
            _eventListeners[name].Remove(action);

            if (_eventListeners[name].Count == 0)
                _eventListeners.Remove(name);

            return true;
        }

        return false;
    }
}
