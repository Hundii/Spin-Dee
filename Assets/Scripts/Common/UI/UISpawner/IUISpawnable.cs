namespace Common
{
    public interface IUISpawnable<T1>
    {
        public void Init(T1 item);
        public void Refresh(T1 item) { Init(item); }
    }

    public interface IUISpawnable<T1, T2> : IUISpawnable<T1>
    {
        void IUISpawnable<T1>.Init(T1 item) { Init(item); }
        public void Init(T1 item, T2 additionalData);
        public void Init((T1,T2) itemWithData) { Init(itemWithData.Item1, itemWithData.Item2); }
        public void Refresh(T1 item, T2 additionalData) { Init(item, additionalData); }
        public void Refresh((T1,T2) itemWithData) { Refresh(itemWithData.Item1, itemWithData.Item2); }

    }
}
