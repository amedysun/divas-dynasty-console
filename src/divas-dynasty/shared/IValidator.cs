namespace divas_dynasty.shared;

public interface IValidator<T>
{
    void Validate(T value);
}
