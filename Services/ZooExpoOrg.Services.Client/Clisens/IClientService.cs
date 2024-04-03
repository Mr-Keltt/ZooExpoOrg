using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ZooExpoOrg.Services.Clients;

public interface IClientService
{
    Task<IEnumerable<ClientModel>> GetAll();
    Task<ClientModel> GetById(Guid id);
    Task<ClientModel> Create(CreateClientModel model);
    Task Update(Guid id, UpdateClientModel model);
    Task Delete(Guid id);
}
