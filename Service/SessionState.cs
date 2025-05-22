namespace CrmService.Service
{
    public class SessionState
    {
        public Guid? UserId { get; private set; }
        public string? Email { get; private set; }

        public bool IsLoggedIn => UserId != null;

        public void SignIn(Guid id, string email)
        {
            UserId = id;
            Email = email;
        }

        public void SignOut()
        {
            UserId = null;
            Email = null;
        }
    }
}