using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Clients.Inputs
{
    public interface ILgTvInputClient
    {
        Task<Input> GetInput(string id);

        Task<IEnumerable<Input>> GetInputs();

        Task SetInput(string id);
    }
}
