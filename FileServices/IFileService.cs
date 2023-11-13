namespace FileServices
{
    public interface IFileService<T>
    {
        Task<IEnumerable<T>> LoadAsync();
        Task SaveAsync(IEnumerable<T> items);
    }

}