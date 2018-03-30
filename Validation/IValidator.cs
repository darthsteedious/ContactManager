namespace ContactManager.Validation
{
    public interface IValidator<in T>
    {
        string[] Errors { get; }
        bool Validate(T entity);
    }
}