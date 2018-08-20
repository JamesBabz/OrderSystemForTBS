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
        Visit Get(int id);
        IEnumerable<Visit> GetAll();
        IEnumerable<Visit> GetAllById(int id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Visit Delete(int id);
       
    }
}
