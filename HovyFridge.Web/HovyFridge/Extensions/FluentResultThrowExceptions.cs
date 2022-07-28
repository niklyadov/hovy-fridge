using FluentResults;
namespace HovyFridge.Extensions
{
    public static class FluentResultThrowExceptions
    {
        public static void ThrowExceptions(this ResultBase<Result> result)
        {
            if (result.IsFailed)
            {
                throw new Exception("Some error");
            }
        }
    }
}
