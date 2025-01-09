public class UserService : CrudService<User>, IUserService
{
    public UserService(BookManagerContext context) : base(context)
    {
    }

    // Add or override custom methods specific to User here
}
