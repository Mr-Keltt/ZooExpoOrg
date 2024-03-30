using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ZooExpoOrg.Services.Users;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAll();
    Task<UserModel> GetById(Guid id);
    Task<UserModel> Create(CreateUserModel model);
    Task Update(Guid id, UpdateUserModel model);
    Task Delete(Guid id);
}
