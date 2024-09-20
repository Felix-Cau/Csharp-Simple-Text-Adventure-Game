namespace Examinationsuppgift3.Classes;

public abstract class Entity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Entity(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static (List<T>, List<T>) MoveObjectBetweenList<T>(List<T> listToMoveFrom, List<T> listToMoveTo, T objToMove)
    {
        listToMoveFrom.RemoveAll(item => listToMoveFrom.Contains(item));
        listToMoveTo.Add(objToMove);
        return (listToMoveFrom, listToMoveTo);
    }
}