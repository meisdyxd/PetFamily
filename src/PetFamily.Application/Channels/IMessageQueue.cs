namespace PetFamily.Application.Channels;

public interface IMessageQueue<T>
{
    Task WriteAsync(T message, CancellationToken cancellationToken);
    Task<T> ReadAsync(CancellationToken cancellationToken);
}
