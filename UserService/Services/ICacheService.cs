namespace UserService.Services
{
    public interface ICacheService
    {
        void SetValue(string key, string value);

        string GetValue(string key);

        void Delete(string key);
    }
}