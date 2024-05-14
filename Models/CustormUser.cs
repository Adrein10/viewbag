namespace viewbag.Models
{
    public class CustormUser
    {
        public User? CUser{ get; set; }
        public IEnumerable<Role>? CRole { get; set; }
    }
}
