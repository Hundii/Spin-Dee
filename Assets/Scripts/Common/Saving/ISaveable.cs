namespace Common.Saving
{
    public interface ISaveable
    {
        public object GetContent();
        public string GetFileName();
    }
}
