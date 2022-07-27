namespace HovyFridge
{
    public class Pagination
    {
        public int ItemsPerPage { get; private set; }

        public Pagination(int itemsPerPage)
        {
            ItemsPerPage = itemsPerPage;
        }

        public int GetOffset(int totalCount) 
            => totalCount * ItemsPerPage;
    }
}
