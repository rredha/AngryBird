using Project.Scripts.Runtime.Angrybird.Presenter.Pointer;
using UnityEngine;

namespace Project.Scripts.Runtime.Core.SessionManager
{
    public class TaskDataRecord
    {
        private BaseTask _task;
        private Time _beginTime;
        private Time _endTime;
        private Time _elapsedTime;

        public TaskDataRecord(BaseTask task)
        {
            _task = task;
        }

        public void CalculateElapsedTime(Time begin, Time end)
        {
            //_elapsedTime = end - begin;
        }

        public void SetBeginTime(Time time)
        {
            
        }
        public void SetEndTime(Time time)
        {
            
        }
        public void ClearTimers()
        {
            /*
            _beginTime = 0;
            _endTime = 0;
            */
        }
    }
}