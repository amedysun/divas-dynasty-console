namespace domain.shared;

public interface IValidator<T>
{
    void Validate(T value);
}
