namespace HovyFridge.Api
{
    public class CountedGroupBy<T>
    {
        public T Group { get; set; }
        public long Count { get; set; }

        public CountedGroupBy(T group, long count)
        {
            Group = group;
            Count = count;
        }
    }
}
