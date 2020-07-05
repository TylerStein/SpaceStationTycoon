namespace SpaceStationTycoon.Engine
{
    public interface IUpdateReceiver
    {
        void Update(double deltaTimeMs, InputState inputState);
    }
}
