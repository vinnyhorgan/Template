using DefaultEcs;
using Raylib_cs;
using System;
using Template.Components;
using Template.Core;

namespace Template.Systems
{
    class AnimationSystem : ISystem
    {
        private World _world;
        private EntitySet _entities;

        public AnimationSystem(World world)
        {
            _world = world;
            _entities = _world.GetEntities().With<TransformComponent>().With<SpriteComponent>().With<AnimationComponent>().AsSet();
        }

        public void Update(float dt)
        {
            foreach (var entity in _entities.GetEntities())
            {
                var animation = entity.Get<AnimationComponent>();

                if (animation.Playing)
                {
                    animation.FrameCounter++;

                    if (animation.FrameCounter >= Raylib.GetFPS() / animation.FPS)
                    {
                        animation.FrameCounter = 0;
                        animation.CurrentFrame++;

                        if (animation.CurrentFrame >= animation.Columns)
                        {
                            animation.CurrentFrame = 0;
                        }
                    }
                }

                animation.Frame.x = animation.CurrentFrame * Math.Abs(animation.Frame.width);
            }
        }

        public void Draw()
        {
            foreach (var entity in _entities.GetEntities())
            {
                var transform = entity.Get<TransformComponent>();
                var sprite = entity.Get<SpriteComponent>();
                var animation = entity.Get<AnimationComponent>();

                Raylib.DrawTexturePro(sprite.Texture, animation.Frame, new Rectangle(transform.Position.X, transform.Position.Y, Math.Abs(animation.Frame.width) * transform.Scale.X, Math.Abs(animation.Frame.height) * transform.Scale.Y), sprite.Offset, transform.Rotation, sprite.Tint);
            }
        }
    }
}
