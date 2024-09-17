namespace domain.shared;

public interface IHandler<T>
{
    void Handle(T request);
}
