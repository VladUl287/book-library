namespace DataAccess.Abstractions
{
    public abstract class EntityWithId<T>
    {
        public T Id { get; set; }
    }
}