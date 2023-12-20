namespace LgTv.Interaction;

public interface IKeyboard
{
    Task InsertText(string text, int replace = 0);

    Task DeleteCharacters(int count = 1);

    Task SendEnterKey();
}
