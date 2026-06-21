using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Common.Saving.Flexible
{
    public class SaveFile : ISaveable
    {
        private string fileName;
        public Dictionary<string, object> saveContent = new();

        public SaveFile(string fileName)
        {
            this.fileName = fileName;
        }
        public T Load<T>(string key, T defaultValue = default)
        {
            if (saveContent.TryGetValue(key, out var content))
            {
                if (content.GetType() == typeof(JObject) || content.GetType() == typeof(JArray))
                {
                    return JsonConvert.DeserializeObject<T>(content.ToString());
                }
                return (T)content;
            }

            LoadContent();

            if (saveContent.TryGetValue(key, out var loadedContent))
            {
                return JsonConvert.DeserializeObject<T>(loadedContent.ToString());
            }

            return defaultValue;
        }

        public void LoadContent()
        {
            FileSaveManager.LoadFile(fileName, ref saveContent);
        }

        public void Save<T>(string key, T value)
        {
            saveContent[key] = value;
        }

        public void ForceSave<T>(string key, T value)
        {
            Save(key, value);
            SaveContent();
        }

        public void SaveContent()
        {
            FileSaveManager.SaveFile(fileName, saveContent);
        }

        public void Clear()
        {
            saveContent = new();
        }

        public string GetFileName()
        {
            return fileName;
        }

        public object GetContent()
        {
            return saveContent;
        }
    }
}
