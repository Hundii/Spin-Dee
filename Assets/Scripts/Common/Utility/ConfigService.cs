using Newtonsoft.Json.Linq;
using System.IO;

namespace Common
{
    public class ConfigService
    {
        private const string configPath = "Assets/Scripts/Common/config.json";

        private static ConfigService _instance;
        public static ConfigService Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }

        private JObject root;
        private ConfigService()
        {
            Init();
        }
        private void Init()
        {
            try
            {
                var path = Utility.NormalizeFilePath(configPath);
                var configContent = File.ReadAllText(path);
                root = JObject.Parse(configContent);
            }
            catch
            {
                throw new System.Exception($"config.json is missing from {configPath}");
            }
        }
        public string GetValue(string key)
        {
            var token = GetToken(key);
            return token.ToString();
        }

        public T GetValue<T>(string key)
        {
            var token = GetToken(key);
            return token != null ? token.ToObject<T>() : default;
        }

        private JToken GetToken(string key)
        {
            string[] parts = key.Split(':');
            JToken current = root;
            foreach (var part in parts)
            {
                if (current == null) return null;
                current = current[part];
            }
            return current;
        }

    }
}
