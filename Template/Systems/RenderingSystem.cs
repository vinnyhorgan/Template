using DefaultEcs;
using Raylib_cs;
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
            _entities = _world.GetEntities().With<TransformComponent>().AsSet();
        }

        public void Update(float dt)
        {
            foreach (var entity in _entities.GetEntities())
            {
                var transform = entity.Get<TransformComponent>();

                transform.Position.X += 1;
            }
        }

        public void Draw()
        {
            foreach (var entity in _entities.GetEntities())
            {
                var transform = entity.Get<TransformComponent>();

                Raylib.DrawRectangle((int)transform.Position.X, (int)transform.Position.Y, 32, 32, Color.RED);
            }
        }
    }
}
