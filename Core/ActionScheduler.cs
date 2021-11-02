using UnityEngine;

namespace RPG.Core
{
    public interface IAction
    {
        void Cancel();
    }
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }

        public void CancelCurrentAction()
        {

            StartAction(null);
        }
    }
}