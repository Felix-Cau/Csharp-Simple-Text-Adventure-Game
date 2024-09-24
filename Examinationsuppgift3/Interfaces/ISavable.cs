namespace Examinationsuppgift3.Interfaces;

public interface ISavable
{
    public void SaveObjectToFile<T>(T obj);
}