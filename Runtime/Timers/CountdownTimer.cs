using UnityEngine;
using System;

namespace ImprovedTimers {
    /// <summary>
    /// Timer that counts down from a specific value to zero.
    /// </summary>
    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0 && !reversing)
            {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0 && !reversing)
            {
                CurrentTime = 0;
                OnTimerReachedEnd?.Invoke();
                Stop();
            }

            if (IsRunning && CurrentTime < initialTime && reversing)
            {
                CurrentTime += Time.deltaTime;
            }

            if (IsRunning && CurrentTime >= initialTime && reversing)
            {
                CurrentTime = initialTime;
                OnTimerReachedStart?.Invoke();
                Stop();
            }
        }

        /// <summary>
        /// Allows reversing the CountdownTimer.
        /// Helpful in case where you require holding a button and want to reset something by reversing the timer.
        /// E.g. hold to interact, skip cutscene, etc.
        /// </summary>
        /// 
        public Action OnTimerReachedEnd = delegate { };
        public Action OnTimerReachedStart = delegate { };
        public Action OnTimerReverseStart = delegate { };
        bool reversing = false;
        public void Forward() => reversing = false;
        public void Reverse() => reversing = true;
        
        public void StartReverse() {
            CurrentTime = 0f;
            Reverse();
            if (!IsRunning)
            {
                Resume();
                TimerManager.RegisterTimer(this);
                OnTimerReverseStart.Invoke();
            }
        }

        public override bool IsFinished => CurrentTime <= 0;
        public bool HasReachedStart => CurrentTime >= initialTime;
    }
}