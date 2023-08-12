using System.Numerics;
using System.Reflection;
using DefaultEcs;
using Raylib_cs;
using System.Linq;
using System;

namespace Template.Core
{
    class Screen
    {
        ISystem[] _systems;

        public int GameWidth
        {
            get { return Program.GameWidth; }
        }

        public int GameHeight
        {
            get { return Program.GameHeight; }
        }

        public Font Font
        {
            get { return Program.Font; }
        }

        public Vector2 Mouse
        {
            get { return Raylib.GetMousePosition(); }
        }

        public AssetManager AssetManager
        {
            get { return AssetManager.Instance; }
        }

        public ScreenManager ScreenManager
        {
            get { return ScreenManager.Instance; }
        }

        public World World { get; private set; }

        public virtual void Load()
        {
            World = new World();

            _systems = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(ISystem).IsAssignableFrom(type) && type != typeof(ISystem))
                .Where(type => type.Namespace == "Template.Systems")
                .ToList().Select(type => (ISystem)Activator.CreateInstance(type, World)).ToArray();
        }

        public virtual void Update(float dt)
        {
            foreach (var system in _systems)
            {
                system.Update(dt);
            }
        }

        public virtual void Draw()
        {
            foreach (var system in _systems)
            {
                system.Draw();
            }
        }

        public virtual void Unload()
        {
            World.Dispose();
        }
    }
}
