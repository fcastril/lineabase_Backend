using System.Threading.Tasks;

namespace ServiceApplication.Functions
{
    public interface IGenAI
    {
        Task<bool> SendAsync(string discoveryId, string name);
    }
}
