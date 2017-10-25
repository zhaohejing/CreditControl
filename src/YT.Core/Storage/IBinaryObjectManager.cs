using System;
using System.Threading.Tasks;

namespace YT.Storage
{
    /// <summary>
    /// �������interface
    /// </summary>
    public interface IBinaryObjectManager
    {
        Task<BinaryObject> GetOrNullAsync(Guid id);
        
        Task SaveAsync(BinaryObject file);
        
        Task DeleteAsync(Guid id);
    }
}