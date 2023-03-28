
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SceneState
    {
        public SceneState()
        {
            this.TimeAtPause = TimerEventMan.GetCurrTime();
        }
        public abstract int GetSceneId();
        public abstract void Initialize();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        public abstract void Entering();
        public abstract void Leaving();

        public float TimeAtPause;

    }
}