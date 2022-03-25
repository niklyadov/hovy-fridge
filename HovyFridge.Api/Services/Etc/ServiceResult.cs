namespace HovyFridge.Api.Services.Etc
{
    public class ServiceResult<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; }

    }
}
