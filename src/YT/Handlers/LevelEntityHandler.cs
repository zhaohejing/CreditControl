using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using YT.Handlers;

namespace YT.Handlers
{
   public class LevelEntityHandler<T> :ILevelEntityHandler<T> where T:class ,ITreeLevel
   {
       protected IRepository<T> Repository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        public LevelEntityHandler(IRepository<T> repository)
       {
            Repository = repository;
       }
        public virtual void Create(T t)
        {
            if (t.Id == t.ParentId)
                throw new AbpException("树形结构实体，无法指向自己本身，操作失败。");
            var level = GenderLevel();
            if (t.ParentId.HasValue)
            {
                T parent = Repository.Get(t.ParentId.Value);
                t.LevelCode = $"{parent.LevelCode}.{level}";
            }
            else
            {
                t.LevelCode = level;
            }
            var childs = GetParentChilds(t).ToList();
            t.Sort = childs.Any() ? childs.Max(o => o.Sort) + 1 : 1;
            t.Id = Repository.InsertAndGetId(t);
        }
        public virtual void UpdateParent(T t)
        {
            if (t.Id == t.ParentId)
                throw new AbpException("树形结构实体，无法指向自己本身，操作失败。");
            var level = GenderLevel();
            if (t.ParentId == null)
            {
                t.LevelCode = level;
            }
            else
            {
              var  parent = Repository.Get(t.ParentId.Value);
             t.LevelCode = t.LevelCode = $"{parent.LevelCode}.{level}";
            }
            IEnumerable<T> allChilds = GetAllChilds(t);
            foreach (T item in allChilds)
            {
                item.LevelCode = $"{t.LevelCode}.{level}";
                Repository.Update(item);
            }
           // t.ParentIds = parent != null ? $"{parent.ParentIds}{parent.Id}," : ",";
            List<T> childs = GetParentChilds(t).Where(o => o.Id != t.Id).ToList();
            t.Sort = childs.Any() ? childs.Max(o => o.Sort) + 1 : 1;
            Repository.Update(t);
        }
        /// <summary>
        /// 渲染节点
        /// </summary>
        private string GenderLevel()
        {
            return Guid.NewGuid().ToString("D").Split('-')[0];
        }
    
        public virtual  void UpdateAsync(T t)
        {
            Repository.Update(t);
        }

        public virtual async Task DeleteAsync(T t)
        {
          await  Repository.DeleteAsync(c => c.LevelCode.Contains(t.LevelCode));
        }

        public async Task<IEnumerable<T>> GetAllRoot()
        {
            return await Repository.GetAllListAsync(t => t.ParentId == null);
        }

        public IEnumerable<T> GetChilds(T t)
        {
            return Repository.GetAllList(o => o.ParentId == t.Id);
        }

        public IEnumerable<T> GetParentChilds(T t)
        {
            return Repository.GetAllList(o => o.ParentId == t.ParentId);
        }

        public IEnumerable<T> GetAllChilds(T t)
        {
            return Repository.GetAllList(o => o.LevelCode.Contains(t.LevelCode));
        }
     
    }
}
