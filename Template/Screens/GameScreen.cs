using Template.Core;
using Template.Components;
using Raylib_cs;
using DefaultEcs;
using System.Numerics;

namespace Template.Screens
{
    class GameScreen : Screen
    {
        private Entity _player;

        public override void Load()
        {
            base.Load();

            _player = World.CreateEntity();

            _player.Set(new TransformComponent { Position = new Vector2(100, 100) });
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            Raylib.ClearBackground(Color.GREEN);
        }
    }
}
