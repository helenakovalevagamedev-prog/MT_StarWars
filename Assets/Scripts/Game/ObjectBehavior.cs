public interface IMovable
{
    public void Move();
}

public interface IDestroyable
{
    public void DestroySelf();
}

public interface IOutOfBoundsHandler
{
    public bool IsOutOfBounds();
}
