namespace LgTv.Features.Inputs;

public interface IInputClient
{
    Task<Input> GetInput(string id);

    Task<IEnumerable<Input>> GetInputs();

    Task SetInput(string id);
}
