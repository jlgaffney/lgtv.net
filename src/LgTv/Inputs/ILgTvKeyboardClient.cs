using System.Threading.Tasks;

namespace LgTv.Inputs
{
    public interface ILgTvKeyboardClient
    {
        Task InsertText(string text, int replace = 0);

        Task DeleteCharacters(int count = 1);

        Task SendEnterKey();
    }
}
