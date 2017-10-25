namespace YT.Authorization.Permissions.Dto
{
    /// <summary>
    /// 权限dto
    /// </summary>
    public class FlatPermissionWithLevelDto : FlatPermissionDto
    {
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }
}