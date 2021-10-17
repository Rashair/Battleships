namespace Battleships.Settings
{
    public interface IIOManager
    {
        bool GetBooleanInput(string question);
        int? GetIntegerInput(string msg);
        string? ReadLine();
        int? ReadNumericInput();
        void Write(string msg);
        void WriteLine(string msg = "");
    }
}