using UnityEngine;

namespace Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;



        public void StartAction(IAction action)
        {
            if (_currentAction == null)
            {
                _currentAction = action;
                return;
            }

            if (_currentAction != action)
            {
                _currentAction.Cancel();
                _currentAction = action;
            }
        }
        
        public void CancelCurrent()
        {
            StartAction(null);
        }
    }
}