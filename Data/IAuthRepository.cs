using dotnet_rpg.Models;

namespace dotnet_rpg.Data {
    public interface IAuthRepository {

        Task<ServiceResponse<Guid>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
    
}