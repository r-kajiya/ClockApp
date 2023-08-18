using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public interface IUseCase<out TPresenter, out TView>
        where TView : IView
        where TPresenter : IPresenter<TView>
    {
        public TPresenter Presenter { get; }
    }
}