using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    using DAL.Entities;

    public interface IVisitRepository
    {
        //C
        Visit Create(Visit visit);
        //R
        IEnumerable<Visit> GetAll();

        Visit Get(int id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Visit Delete(int id);
        IEnumerable<Visit> GetAll(int id);
    }
}
