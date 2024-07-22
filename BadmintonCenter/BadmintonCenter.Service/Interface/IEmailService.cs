using BadmintonCenter.Common.DTO.User;

namespace BadmintonCenter.Service.Interface
{
    public interface IEmailService
    {
        void SendInfomationModeratorEmail(string userEmail, string subject, CreateUserDTO info);
    }
}
