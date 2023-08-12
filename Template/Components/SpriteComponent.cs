using Raylib_cs;
using System.Numerics;
using Template.Core;

namespace Template.Components
{
    class SpriteComponent
    {
        public Texture2D Texture;
        public string TextureName;
        public Vector2 Offset = Vector2.Zero;
        public Color Tint = Color.WHITE;

        public SpriteComponent(string filename)
        {
            Texture = AssetManager.Instance.GetTexture(filename);
            TextureName = filename;
        }
    }
}
