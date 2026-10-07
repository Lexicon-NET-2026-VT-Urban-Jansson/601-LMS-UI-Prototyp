using TestClaudeLMS01.Client.Layout;

namespace TestClaudeLMS01.Client.Services
{
    public class AuthState
    {
        public UserRole? CurrentRole { get; set; }

        public bool IsAuthenticated => CurrentRole.HasValue;

        public void Login(UserRole role)
        {
            CurrentRole = role;
        }

        public void Logout()
        {
            CurrentRole = null;
        }
    }
}
