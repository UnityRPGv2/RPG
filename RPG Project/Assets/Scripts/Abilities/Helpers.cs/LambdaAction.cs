using System;
using RPG.Core;

public class LambdaAction : IAction
{
    Action activate;
    Action cancel;

    public LambdaAction(Action activate, Action cancel = null)
    {
        this.activate = activate;
        this.cancel = cancel;
    }

    public void Activate()
    {
        activate();
    }

    public void Cancel()
    {
        if (cancel != null) cancel();
    }
}