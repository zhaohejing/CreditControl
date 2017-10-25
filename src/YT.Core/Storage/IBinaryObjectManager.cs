using System;
using System.Threading.Tasks;

namespace YT.Storage
{
    /// <summary>
    /// ¥¢¥Ê∂‘œÛinterface
    /// </summary>
    public interface IBinaryObjectManager
    {
        Task<BinaryObject> GetOrNullAsync(Guid id);
        
        Task SaveAsync(BinaryObject file);
        
        Task DeleteAsync(Guid id);
    }
}