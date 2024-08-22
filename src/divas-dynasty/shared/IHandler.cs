namespace divas_dynasty.shared;

public interface IHandler<T>
{
    void Handle(T request);
}
