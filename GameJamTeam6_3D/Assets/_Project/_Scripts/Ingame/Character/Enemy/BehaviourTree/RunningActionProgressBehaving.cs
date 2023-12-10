using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningActionProgressBehaving : MonoBehaviour
{
    bool running = false;
    public BehaviourTreeResult Progress(Action _onEnter, Func<bool> _onRunning, ref bool _forceComplete, Action _onComplete)
    {
        if (running == false)
        {
            _onEnter?.Invoke();
            running = true;
        }
        if (running && _onRunning != null)
        {
            bool resultOK = (bool)(_onRunning?.Invoke());
            if (resultOK)
            {
                running = false;
                _onComplete?.Invoke();
                return BehaviourTreeResult.Sucess;
            }
        }
        if (_forceComplete)
        {
            running = false;
            _forceComplete = false;
            _onComplete?.Invoke();
            return BehaviourTreeResult.Sucess;
        }

        return BehaviourTreeResult.Running;
    }




}
