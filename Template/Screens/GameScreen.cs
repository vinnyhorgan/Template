using DefaultEcs;
using System.Numerics;
using Template.Core;
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

            _player.Set(new TransformComponent { Position = new Vector2(100, 100) });
            _player.Set(new SpriteComponent("eevee.png"));
            _player.Set(new AnimationComponent("eevee.png", 4, 4, 0));
            _player.Set(new CharacterControllerComponent { Speed = 300 });
            _player.Set(new NetworkIdentityComponent());
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
