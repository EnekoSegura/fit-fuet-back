using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace fitfuet.back.IServices
{
    public interface IChatService
    {
        Task HandleWebSocket(HttpContext context, int userId);
    }
}
