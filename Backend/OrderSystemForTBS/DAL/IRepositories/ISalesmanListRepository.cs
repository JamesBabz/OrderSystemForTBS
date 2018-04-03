using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    public interface ISalesmanListRepository
    {
        //C
        SalesmanList Create(SalesmanList salesmanList);
        //R
        SalesmanList Get(int Id);
        IEnumerable<SalesmanList> GetAll();
        IEnumerable<SalesmanList> GetAllById(int Id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        SalesmanList Delete(int Id);
    }
}
