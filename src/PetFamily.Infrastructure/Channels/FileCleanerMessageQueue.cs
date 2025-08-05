using PetFamily.Application.Channels;
using System.Threading.Channels;

namespace PetFamily.Infrastructure.Channels;

public class InMemoryMessageQueue<T>: IMessageQueue<T>
{
    private readonly Channel<T> _channel = Channel.CreateUnbounded<T>();

    public async Task WriteAsync(T message, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(message, cancellationToken);
    }

    public async Task<T> ReadAsync(CancellationToken cancellationToken)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}
