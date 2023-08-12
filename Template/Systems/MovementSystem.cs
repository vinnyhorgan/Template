using DefaultEcs;
using Raylib_cs;
using System.Numerics;
using Template.Components;
using Template.Core;

namespace Template.Systems
{
    class MovementSystem : ISystem
    {
        private World _world;
        private EntitySet _entities;

        public MovementSystem(World world)
        {
            _world = world;
            _entities = _world.GetEntities().With<TransformComponent>().With<CharacterControllerComponent>().AsSet();
        }

        public void Update(float dt)
        {
            foreach (var entity in _entities.GetEntities())
            {
                var transform = entity.Get<TransformComponent>();
                var controller = entity.Get<CharacterControllerComponent>();

                var velocity = Vector2.Zero;

                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                {
                    velocity.Y -= 1;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                {
                    velocity.Y += 1;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    velocity.X -= 1;
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    velocity.X += 1;
                }

                if (velocity != Vector2.Zero)
                {
                    velocity = Vector2.Normalize(velocity);

                    transform.Position += velocity * controller.Speed * dt;

                    if (entity.Has<NetworkIdentityComponent>())
                    {
                        entity.Get<NetworkIdentityComponent>().Dirty = true;
                    }
                }
            }
        }

        public void Draw()
        {

        }
    }
}
