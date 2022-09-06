using KF.Core.Data;
using System;
using System.Collections.Generic;

namespace KF.Core.DomainModels
{
    public partial class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
