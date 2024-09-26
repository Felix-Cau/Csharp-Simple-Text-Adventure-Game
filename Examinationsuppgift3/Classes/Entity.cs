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
}