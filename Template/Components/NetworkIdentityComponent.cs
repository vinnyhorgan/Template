using Raylib_cs;

namespace Template.Components
{
    class NetworkIdentityComponent
    {
        public int Id;
        public bool Dirty = true;

        public NetworkIdentityComponent()
        {
            Id = Raylib.GetRandomValue(0, 25000);
        }
    }
}
