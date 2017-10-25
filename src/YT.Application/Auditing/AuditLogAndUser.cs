using Abp.Auditing;
using YT.Authorization.Users;
using YT.Managers.Users;

namespace YT.Auditing
{
    /// <summary>
    /// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
    /// </summary>
    public class AuditLogAndUser
    {/// <summary>
    /// 
    /// </summary>
        public AuditLog AuditLog { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }
    }
}