using Raylib_cs;
using System;
using Template.Core;

namespace Template.Components
{
    class AnimationComponent
    {
        public int Columns;
        public int Rows;
        public int CurrentAnimation;
        public Rectangle Frame;
        public int FPS = 10;
        public bool Playing = true;
        public int CurrentFrame = 0;
        public int FrameCounter = 0;

        public AnimationComponent(string filename, int columns, int rows, int currentAnimation)
        {
            var texture = AssetManager.Instance.GetTexture(filename);

            Columns = columns;
            Rows = rows;
            CurrentAnimation = currentAnimation;
            Frame = new Rectangle(0, CurrentAnimation * texture.height / Rows, texture.width / Columns, texture.height / Rows);
        }

        public void SetAnimation(int animation)
        {
            CurrentAnimation = animation;
            Frame.y = CurrentAnimation * Math.Abs(Frame.height);
        }

        public void GoToFrame(int frame)
        {
            CurrentFrame = frame;
            Frame.x = CurrentFrame * Math.Abs(Frame.width);
        }

        public void Pause()
        {
            Playing = false;
        }

        public void Resume()
        {
            Playing = true;
        }

        public void FlipHorizontally()
        {
            Frame.width *= -1;
        }

        public void FlipVertically()
        {
            Frame.height *= -1;
        }
    }
}
