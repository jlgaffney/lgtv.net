using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Inputs
{
    public interface ILgTvInputClient
    {
        Task<IEnumerable<ExternalInput>> GetInputs();

        Task SetInput(string id);
    }
}
