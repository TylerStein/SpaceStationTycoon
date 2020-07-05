using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStationTycoon
{
    using Engine;

    public interface IScene
    {
        IRenderable View { get; }
        ISceneController Controller { get; }
    }

    public interface ISceneView : IRenderable
    {
        //
    }

    public interface ISceneController : IUpdateReceiver
    {
        void OnEnter();
        void OnLeave();
    }

    public abstract class SceneView : ISceneView
    {
        public abstract string[] Render();
    }

    public abstract class SceneController : ISceneController
    {
        public abstract void Update(double deltaTimeSeconds, InputState inputState);
        public virtual void OnEnter() { }
        public virtual void OnLeave() { }
    }

    public abstract class Scene : IScene {
        public abstract IRenderable View { get; }
        public abstract ISceneController Controller { get; }
    }
}
