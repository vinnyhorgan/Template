using Raylib_cs;
using Riptide.Utils;
using System;
using System.Numerics;
using Template.Utility;

namespace Template.Core
{
    class Program
    {
        public static int GameWidth { get; private set; } = 1280;
        public static int GameHeight { get; private set; } = 720;
        public static Font Font { get; private set; }
        public static Vector2 Mouse { get; private set; }

        static void Main(string[] args)
        {
            Logger.Init(true);

            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE | ConfigFlags.FLAG_MSAA_4X_HINT);
            Raylib.InitWindow(GameWidth, GameHeight, "Template");
            Raylib.SetWindowMinSize(GameWidth / 2, GameHeight / 2);
            Raylib.SetExitKey(KeyboardKey.KEY_NULL);
            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));

            Raylib.InitAudioDevice();

            RenderTexture2D target = Raylib.LoadRenderTexture(GameWidth, GameHeight);
            Raylib.SetTextureFilter(target.texture, TextureFilter.TEXTURE_FILTER_POINT);

            Raylib.SetWindowIcon(Raylib.LoadImageFromTexture(AssetManager.Instance.LoadTexture("logo.png")));

            Font = AssetManager.Instance.LoadFont("roboto.ttf", 18);

            ImguiController controller = new ImguiController();
            controller.Load(GameWidth, GameHeight);

            RiptideLogger.Initialize(Logger.Debug, Logger.Info, Logger.Warning, Logger.Error, false);

            ScreenManager.Instance.LoadScreen(new Screens.GameScreen());

            while (!Raylib.WindowShouldClose())
            {
                float scale = MathF.Min(
                    (float)Raylib.GetScreenWidth() / GameWidth,
                    (float)Raylib.GetScreenHeight() / GameHeight
                );

                Vector2 mouse = Raylib.GetMousePosition();
                Vector2 virtualMouse = Vector2.Zero;
                virtualMouse.X = (mouse.X - (Raylib.GetScreenWidth() - (GameWidth * scale)) * 0.5f) / scale;
                virtualMouse.Y = (mouse.Y - (Raylib.GetScreenHeight() - (GameHeight * scale)) * 0.5f) / scale;

                var max = new Vector2(GameWidth, GameHeight);
                virtualMouse = Vector2.Clamp(virtualMouse, Vector2.Zero, max);

                Mouse = virtualMouse;

                float dt = Raylib.GetFrameTime();

                controller.Update(dt);

                ScreenManager.Instance.Update(dt);

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);

                Raylib.BeginTextureMode(target);

                Raylib.ClearBackground(Color.BLACK);

                ScreenManager.Instance.Draw();

                Raylib.EndTextureMode();

                var sourceRec = new Rectangle(
                    0.0f,
                    0.0f,
                    target.texture.width,
                    -target.texture.height
                );

                var destRec = new Rectangle(
                    (Raylib.GetScreenWidth() - (GameWidth * scale)) * 0.5f,
                    (Raylib.GetScreenHeight() - (GameHeight * scale)) * 0.5f,
                    GameWidth * scale,
                    GameHeight * scale
                );

                Raylib.DrawTexturePro(target.texture, sourceRec, destRec, new Vector2(0, 0), 0.0f, Color.WHITE);

                controller.Draw();

                Raylib.EndDrawing();
            }

            controller.Dispose();

            Raylib.UnloadRenderTexture(target);

            Raylib.CloseAudioDevice();

            Raylib.CloseWindow();
        }
    }
}
