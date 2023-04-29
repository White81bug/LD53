using static CollectableObject;

public interface ICollectable 
{
    public CollectableType Type { get; set; }

    public enum CollectableType
    {
        Food,
        Scheme
    }
}