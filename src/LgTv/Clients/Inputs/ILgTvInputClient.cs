using System.Collections.Generic;
using System.Threading.Tasks;

namespace LgTv.Inputs
{
    public interface ILgTvInputClient
    {
        Task<IEnumerable<Input>> GetInputs();

        Task SetInput(string id);
    }
}
