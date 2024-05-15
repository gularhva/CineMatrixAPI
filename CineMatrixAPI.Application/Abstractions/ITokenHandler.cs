using CineMatrixAPI.Application.DTOs;
using CineMatrixAPI.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMatrixAPI.Application.Abstractions
{
    public interface ITokenHandler
    {
        Task<TokenDTO> CreateAccessToken(AppUser user);
        string CreateRefreshToken();
    }
}
