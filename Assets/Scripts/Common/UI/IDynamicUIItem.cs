namespace Common
{
    public interface IDynamicUIItem<T1>
    {
        public void UpdateItem(T1 data);
    }

    public interface IDynamicUIItem<T1, T2> : IDynamicUIItem<T1>
    {
        void IDynamicUIItem<T1>.UpdateItem(T1 data) => UpdateItem(data);
        public void UpdateItem(T1 data, T2 additionalData);
    }
}
