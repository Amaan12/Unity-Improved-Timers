using UnityEngine;

namespace ImprovedTimers {
    /// <summary>
    /// Timer that counts up from zero to a specific value.
    /// </summary>
    public class CountupTimer : Timer {
        public CountupTimer(float value) : base(value) { }

        public override void Tick() {
            if (IsRunning && CurrentTime < initialTime) {
                CurrentTime += Time.deltaTime;
            }

            if (IsRunning && CurrentTime >= initialTime) {
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime >= initialTime;
    }
}