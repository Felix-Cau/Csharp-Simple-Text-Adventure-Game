namespace Examinationsuppgift3.Classes;

public abstract class Entity
{
    string Name { get; set; }
    string Description { get; set; }
    
    public static (List<T>, List<T>) MoveObjectBetweenList<T>(List<T> listToMoveFrom, List<T> listToMoveTo, T objToMove)
    {
        listToMoveFrom.RemoveAll(item => listToMoveFrom.Contains(item));
        listToMoveTo.Add(objToMove);
        return (listToMoveFrom, listToMoveTo);
    }
}