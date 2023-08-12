using DefaultEcs;
using Raylib_cs;
using System.Numerics;
using Template.Components;
using Template.Core;

namespace Template.Systems
{
    class RenderingSystem : ISystem
    {
        private World _world;
        private EntitySet _entities;

        public RenderingSystem(World world)
        {
            _world = world;
            _entities = _world.GetEntities().With<TransformComponent>().With<SpriteComponent>().Without<AnimationComponent>().AsSet();
        }

        public void Update(float dt)
        {

        }

        public void Draw()
        {
            foreach (var entity in _entities.GetEntities())
            {
                var transform = entity.Get<TransformComponent>();
                var sprite = entity.Get<SpriteComponent>();

                Raylib.DrawTexturePro(sprite.Texture, new Rectangle(0, 0, sprite.Texture.width, sprite.Texture.height), new Rectangle(transform.Position.X, transform.Position.Y, sprite.Texture.width, sprite.Texture.height), new Vector2(0, 0), 0.0f, Color.WHITE);
            }
        }
    }
}
