
using E_Commerce.Core.DTOs;

namespace E_Commerce.Core.Services
{
    public interface IEmailService
    {
        public Task SendEmail(EmailDTO emailDTO);
    }
}
