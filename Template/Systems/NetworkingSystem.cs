using DefaultEcs;
using Raylib_cs;
using Riptide;
using System.Numerics;
using Template.Components;
using Template.Core;

namespace Template.Systems
{
    class NetworkingSystem : ISystem
    {
        private World _world;
        private EntitySet _entities;
        private Client _client;

        public NetworkingSystem(World world)
        {
            _client = new Client();

            _client.ClientConnected += ClientConnected;
            _client.ClientDisconnected += ClientDisconnected;
            _client.MessageReceived += MessageReceived;

            _client.Connect("127.0.0.1:7777");

            _world = world;
            _entities = _world.GetEntities().With<NetworkIdentityComponent>().With<TransformComponent>().AsSet();
        }

        public void Update(float dt)
        {
            if (!_client.IsConnected)
            {
                return;
            }

            _client.Update();

            foreach (var entity in _entities.GetEntities())
            {
                var networkIdentity = entity.Get<NetworkIdentityComponent>();

                if (networkIdentity.Dirty)
                {
                    var message = Message.Create(MessageSendMode.Reliable, 0);

                    message.AddInt(entity.Get<NetworkIdentityComponent>().Id);

                    message.AddFloat(entity.Get<TransformComponent>().Position.X);
                    message.AddFloat(entity.Get<TransformComponent>().Position.Y);

                    _client.Send(message);

                    networkIdentity.Dirty = false;
                }
            }
        }

        public void Draw()
        {
            if (!_client.IsConnected)
            {
                Raylib.DrawText("Not connected to server", 10, 10, 20, Color.WHITE);

                return;
            }

            for (int i = 0; i < _entities.Count; i++)
            {
                var entity = _entities.GetEntities()[i];

                Raylib.DrawText($"{entity.Get<NetworkIdentityComponent>().Id} - <{(int)entity.Get<TransformComponent>().Position.X}, {(int)entity.Get<TransformComponent>().Position.Y}>", 10, 10 + i * 20, 20, Color.WHITE);
            }
        }

        [MessageHandler(0)]
        private static void RemoveWarning(Message message)
        {
        }

        private void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            foreach (var entity in _entities.GetEntities())
            {
                entity.Get<NetworkIdentityComponent>().Dirty = true;
            }
        }

        private void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {

        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var message = e.Message;

            var id = message.GetInt();
            var x = message.GetFloat();
            var y = message.GetFloat();

            bool found = false;

            foreach (var entity in _entities.GetEntities())
            {
                var networkIdentity = entity.Get<NetworkIdentityComponent>();

                if (networkIdentity.Id == id)
                {
                    entity.Get<TransformComponent>().Position = new Vector2(x, y);

                    found = true;
                }
            }

            if (!found)
            {
                var entity = _world.CreateEntity();

                entity.Set(new TransformComponent { Position = new Vector2(x, y) });
                entity.Set(new SpriteComponent("eevee.png"));
                entity.Set(new AnimationComponent("eevee.png", 4, 4, 0));
                entity.Set(new NetworkIdentityComponent { Id = id });
            }
        }
    }
}
