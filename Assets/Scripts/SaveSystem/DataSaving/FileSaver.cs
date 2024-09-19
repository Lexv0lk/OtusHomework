using System.IO;

namespace SaveSystem.DataSaving
{
    public class FileSaver : IDataSaver
    {
        private const string FILENAME = "Save.json";
        
        public void SaveData(string value)
        {
            using (FileStream fs = new FileStream(FILENAME, FileMode.Create, FileAccess.Write))
                using (var stream = new StreamWriter(fs))
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