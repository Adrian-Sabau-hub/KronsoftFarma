using KF.Core.Data;

namespace KF.CommonModel.Models
{
    public class UserModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
