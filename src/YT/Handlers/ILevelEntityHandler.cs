using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace YT.Handlers
{
  public  interface ILevelEntityHandler<T> where T :ITreeLevel
  {
        void Create(T t);


       void UpdateAsync(T t);

          Task DeleteAsync(T t);

        Task<IEnumerable<T>> GetAllRoot();

         IEnumerable<T> GetChilds(T t);

         IEnumerable<T> GetParentChilds(T t);

         IEnumerable<T> GetAllChilds(T t);
        void UpdateParent(T t);
  }
}
