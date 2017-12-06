using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    using BLL.BusinessObjects;

    public interface IVisitService
    {
        //C
        VisitBO Create(VisitBO visit);
        //R
        List<VisitBO> GetAll();

        VisitBO Get(int id);

        //U
        VisitBO Update(VisitBO visit);
        //D
        VisitBO Delete(int id);
    }
}
