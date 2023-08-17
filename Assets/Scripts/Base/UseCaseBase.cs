using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ClockApp
{
    public abstract class UseCaseBase<TPresenter> where TPresenter : IPresenter
    {
        protected TPresenter Presenter { get; }

        protected UseCaseBase(TPresenter presenter)
        {
            Presenter = presenter;
        }
    }
}