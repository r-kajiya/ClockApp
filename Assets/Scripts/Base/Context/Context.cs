using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClockApp
{
    
    public class Context : MonoBehaviour
    {
        static readonly Dictionary<string, Context> _contexts = new();
        protected static Dictionary<string, Context> Contexts => _contexts;
        
        protected Dictionary<int, IUseCase> UseCases { get; } = new();

        bool _isCurrent;
        
        public IEnumerator LoadFirstContext()
        {
            yield return DoPreLoad(null);
            yield return DoLoad(null);
            yield return DoLoaded(null);
        }
        
        public virtual void RunOnAwake()
        {
            if (!_contexts.ContainsKey(gameObject.name))
            {
                _contexts.Add(gameObject.name, this);
            }
        }

        protected virtual void OnUpdate(float dt)
        {
            foreach (var useCase in UseCases)
            {
                useCase.Value.OnUpdate(dt);
            }
        }
        
        void Update()
        {
            if (!_isCurrent)
            {
                return;
            }
            
            float dt = Time.deltaTime;

            OnUpdate(dt);
        }

        protected virtual IEnumerator DoPreLoad(ContextContainer container)
        {
            foreach (var pair in container.CommonUseCases.Map)
            {
                UseCases.Add(pair.Key, pair.Value);
            }
            
            yield break;
        }
        protected virtual IEnumerator DoLoad(ContextContainer container) { yield break; }
        protected virtual IEnumerator DoLoaded(ContextContainer container) { yield break; }

        protected virtual IEnumerator DoPreUnload() { yield break; }
        protected virtual IEnumerator DoUnload() { yield break; }
        protected virtual IEnumerator DoUnloaded()
        {
            UseCases.Clear();
            yield break;
        }

        protected void ChangeContext(
            Context next,
            ContextContainer container = null,
            Func<IEnumerator> onCurPreUnload = null,
            Func<IEnumerator> onCurUnload = null,
            Func<IEnumerator> onCurUnloaded = null,
            Func<IEnumerator> onNextPreLoad = null,
            Func<IEnumerator> onNextLoad = null,
            Func<IEnumerator> onNextLoaded = null)
        {
            var changer = new ContextChanger();

            changer.onCurPreUnload = onCurPreUnload;
            changer.onCurUnload = onCurUnload;
            changer.onCurUnloaded = onCurUnloaded;
            changer.onNextPreLoad = onNextPreLoad;
            changer.onNextLoad = onNextLoad;
            changer.onNextLoaded = onNextLoaded;

            changer.Execute(this, next, container);
        }

        class ContextChanger
        {
            public Func<IEnumerator> onCurPreUnload;
            public Func<IEnumerator> onCurUnload;
            public Func<IEnumerator> onCurUnloaded;

            public Func<IEnumerator> onNextPreLoad;
            public Func<IEnumerator> onNextLoad;
            public Func<IEnumerator> onNextLoaded;

            public void Execute(Context self, Context next, ContextContainer container)
            {
                AbsolutelyActiveCoroutine.Subscribe(DoExecute(self, next, container));
            }

            IEnumerator DoExecute(Context self, Context next, ContextContainer container)
            {
                self._isCurrent = false;
                
                yield return onCurPreUnload?.Invoke();
                yield return self.DoPreUnload();

                yield return onCurUnload?.Invoke();
                yield return self.DoUnload();

                yield return onCurUnloaded?.Invoke();
                yield return self.DoUnloaded();

                yield return onNextPreLoad?.Invoke();
                yield return next.DoPreLoad(container);

                yield return onNextLoad?.Invoke();
                yield return next.DoLoad(container);

                yield return onNextLoaded?.Invoke();
                yield return next.DoLoaded(container);

                next._isCurrent = true;
            }
        }
    }
}


