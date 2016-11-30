using System.Collections.Generic;

namespace MvcCms.Data
{
    public interface ITagRepository
    {
        IEnumerable<string> GetAll();
        bool IsExists(string tag);
        void Edit(string existingtag, string newTag);
        void Delete(string tag);
    }
}