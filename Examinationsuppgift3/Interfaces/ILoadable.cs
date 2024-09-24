namespace Examinationsuppgift3.Interfaces;

public interface ILoadable
{
    public List<T> LoadObject<T>() where T : class;
}