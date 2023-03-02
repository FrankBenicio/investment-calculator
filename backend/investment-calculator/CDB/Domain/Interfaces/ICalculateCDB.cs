using Cdb.Domain.Dto;
using Cdb.Domain.Models;

namespace Cdb.Domain.Interfaces
{
    public interface ICalculateCDB
    {
        Task<CdbResponse> Execute(CdbRequest cdbRequest);
    }
}
