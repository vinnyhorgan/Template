using Raylib_cs;
using Riptide;
using System.Numerics;
using Template.Core;

namespace Template.Screens
{
    class ServerScreen : Screen
    {
        private Server _server;

        public override void Load()
        {
            _server = new Server();
            _server.MessageReceived += MessageReceived;

            _server.Start(7777, 10);
        }

        public override void Update(float dt)
        {
            _server.Update();
        }

        public override void Draw()
        {
            Raylib.DrawTextEx(Font, "Server", new Vector2(10, 10), Font.baseSize, 0, Color.WHITE);

            for (int i = 0; i < _server.ClientCount; i++)
            {
                Raylib.DrawTextEx(Font, $"Client {_server.Clients[i].Id}", new Vector2(10, 50 + (i * 20)), Font.baseSize, 0, Color.WHITE);

                if (_server.Clients[i].IsConnected)
                {
                    Raylib.DrawTextEx(Font, "Connected", new Vector2(200, 50 + (i * 20)), Font.baseSize, 0, Color.GREEN);
                }
                else
                {
                    Raylib.DrawTextEx(Font, "Disonnected", new Vector2(200, 50 + (i * 20)), Font.baseSize, 0, Color.RED);
                }

                Raylib.DrawTextEx(Font, $"Ping: {_server.Clients[i].SmoothRTT}", new Vector2(400, 50 + (i * 20)), Font.baseSize, 0, Color.WHITE);
            }
        }

        public override void Unload()
        {
            _server.Stop();
        }

        [MessageHandler(0)]
        private static void RemoveWarning(ushort clientId, Message message)
        {
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            _server.SendToAll(e.Message, e.FromConnection.Id);
        }
    }
}
