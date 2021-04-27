using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        int currentPriority = 0;
        IAction nextAction;
        int nextPriority = 0;

        public void StartAction(IAction action, int priority, int cancelAllBelow)
        {
            if (currentAction == action) return;
            if (cancelAllBelow > currentPriority)
            {
                if (currentAction != null) currentAction.Cancel();
                currentAction = action;
                currentPriority = priority;
                if (currentAction != null) currentAction.Activated();
            }
            else
            {
                nextAction = action;
                nextPriority = priority;
            }
        }

        public void SuspendAndStartAction(IAction action, int priority, int suspendAllBelow)
        {
            if (suspendAllBelow > currentPriority)
            {
                nextAction = currentAction;
                nextPriority = currentPriority;
            }
            StartAction(action, priority, suspendAllBelow);
        }

        public void FinishAction(IAction action)
        {
            if (!isCurrentAction(action)) return;

            currentAction = nextAction;
            nextAction = null;
            currentPriority = nextPriority;
            nextPriority = 0;
            if (currentAction != null) currentAction.Activated();
        }

        public bool isCurrentAction(IAction action)
        {
            return currentAction == action;
        }

        public void CancelCurrentAction(int cancelAllBelow)
        {
            StartAction(null, 0, cancelAllBelow);
        }
    }
}