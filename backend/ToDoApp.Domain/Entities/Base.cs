namespace ToDoApp.Domain.Entities;
public class Base
{
    public Base()
    {
        Id = Ulid.NewUlid();
    }

    public Ulid Id { get; set; }
}
