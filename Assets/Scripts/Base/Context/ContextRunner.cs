using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    public class ContextRunner : MonoBehaviour
    {
        [SerializeField]
        FirstRunContext _firstRunContext = null;
        
        [SerializeField]
        List<Context> _children = null;

        void Awake()
        {
            RunOnAwakeAllContext();
            AbsolutelyActiveCoroutine.Subscribe(_firstRunContext.LoadFirstContext());
        }
        
        void RunOnAwakeAllContext()
        {
            foreach (var child in _children)
            {
                child.RunOnAwake();
            }
        }
    }
}