using Models.DTO;
using System.Collections.Generic;

namespace BusinessLayer.URLOperations
{
    public interface IURLOperators
    {
        URLDTO Add(URLDTO model);
        List<URLDTO> Get();
        URLDTO GetById(int id);
        URLDTO GetDataByParms(string param, string data, string type);
        bool Remove(URLDTO model);
        bool Update(URLDTO urldto);
    }
}