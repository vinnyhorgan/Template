using Raylib_cs;
using System.IO;
using System.Reflection;

namespace Template.Core
{
    class AssetManager
    {
        private static AssetManager _instance;

        private AssetManager()
        {

        }

        public static AssetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetManager();
                }

                return _instance;
            }
        }

        public Texture2D LoadTexture(string name)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Template.Assets.Textures.{name}"))
            {
                if (stream == null)
                {
                    Logger.Error($"Could not find texture {name}");
                    return new Texture2D();
                }

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    var image = Raylib.LoadImageFromMemory(".png", ms.ToArray());
                    var texture = Raylib.LoadTextureFromImage(image);
                    Raylib.UnloadImage(image);

                    return texture;
                }
            }
        }

        public Font LoadFont(string name, int size)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Template.Assets.Fonts.{name}"))
            {
                if (stream == null)
                {
                    Logger.Error($"Could not find font {name}");
                    return new Font();
                }

                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    var font = Raylib.LoadFontFromMemory(".ttf", ms.ToArray(), size, null, 0);
                    return font;
                }
            }
        }
    }
}
