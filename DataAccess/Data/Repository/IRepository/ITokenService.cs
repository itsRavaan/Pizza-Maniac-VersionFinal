using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Data.Repository.IRepository
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
