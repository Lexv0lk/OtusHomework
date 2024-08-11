using System.IO;

namespace SaveSystem.DataSaving
{
    public class FileSaver : IDataSaver
    {
        private const string FILENAME = "Save.json";
        
        public void SaveData(string value)
        {
            using (var stream = new StreamWriter(File.OpenWrite(FILENAME)))
                stream.Write(value);
        }

        public bool TryGetData(out string result)
        {
            result = null;
            
            if (File.Exists(FILENAME) == false)
                return false;

            result = File.ReadAllText(FILENAME);
            return true;
        }
    }
}