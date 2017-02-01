using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Data.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> getElementList();
        T getElement(int id);
        void createElement(T item);
        void updateElement(T item);
        void deleteElement(int id);
        Task save();
        bool elementExists(int id);

    }
}
