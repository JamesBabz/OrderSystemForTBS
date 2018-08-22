using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.IRepositories
{
    public interface IReceiptRepository
    {
        //C
        Receipt Create(Receipt ent);
        //R
        Receipt Get(int Id);

        IEnumerable<Receipt> GetNotificationList(int Id);
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Receipt Delete(int Id);
        IEnumerable<Receipt> GetAll(int id);
    }
}
