namespace SaveSystem.DataSaving
{
    public interface IDataSaver
    {
        void SaveData(string value);
        bool TryGetData(out string result);
    }
}