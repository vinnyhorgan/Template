using DefaultEcs;
using Raylib_cs;
using System.Numerics;
using Template.Core;
using Template.Core.Transitions;
using Template.Components;

namespace Template.Screens
{
    class GameScreen : Screen
    {
        private Entity _player;

        public override void Load()
        {
            base.Load();

            _player = World.CreateEntity();

            Texture2D texture = AssetManager.LoadTexture("eevee.png");

            _player.Set(new TransformComponent { Position = new Vector2(100, 100) });
            _player.Set(new SpriteComponent { Texture = texture });
            _player.Set(new AnimationComponent(texture, 4, 4, 0));
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                ScreenManager.LoadScreen(new ServerScreen(), new FadeTransition(Color.BLACK));
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
