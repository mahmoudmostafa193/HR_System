using DevDash.DTO.Account;
using HRSystem.DTO.Account;

namespace HRSystem.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<TokenDTO> Login(LoginDTO loginDTO);
        
    }
}
