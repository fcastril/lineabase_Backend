using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.Port
{
    public interface IMessageSender<T> where T : class
    {
        Task SendAsync(T message, CancellationToken cancellationToken = default);
        Task SendCommAsync(T message, CancellationToken cancellationToken = default);
    }
}
