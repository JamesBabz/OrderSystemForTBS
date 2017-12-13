using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface IPropositionRepository
    { 
        //C
        Proposition Create(Proposition ent);
        //R
        Proposition Get(int Id);

        List<int> getFileIds();
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Proposition Delete(int Id);
        IEnumerable<Proposition> GetAll(int id);
    }
}
