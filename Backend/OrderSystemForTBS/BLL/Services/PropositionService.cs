using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class PropositionService : IPropositionService
    {
        private IDALFacade facade;
        private PropositionConverter propConv;
        private Proposition newProp;

        public PropositionService(IDALFacade facade)
        {
            this.facade = facade;
            this.propConv = new PropositionConverter();
            
        }

        public PropositionBO Create(PropositionBO bo)
        {
            using (var uow = facade.UnitOfWork)
            {
                newProp = uow.PropositionRepository.Create(propConv.Convert(bo));
                uow.Complete();
                return propConv.Convert(newProp);
            }
        }

        public PropositionBO Get(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newProp = uow.PropositionRepository.Get(Id);
                uow.Complete();
                return propConv.Convert(newProp);
            }
        }

        public PropositionBO Update(PropositionBO bo)
        {
            using (var uow = facade.UnitOfWork)
            {
                // gets prop from DB that matches the id
                var propFromDb = uow.PropositionRepository.Get(bo.Id);
                propFromDb.Title = bo.Title;
                propFromDb.Description = bo.Description;
                propFromDb.FileId = bo.FileId;
                uow.Complete();
                return propConv.Convert(propFromDb);
            }
        }

        public PropositionBO Delete(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newProp = uow.PropositionRepository.Get(Id);
                uow.PropositionRepository.Delete(newProp.Id);
                uow.Complete();
                return propConv.Convert(newProp);
            }
        }

        public List<PropositionBO> GetAllById(int customerId)
        {
            using (var uow = facade.UnitOfWork)
            {
                List<PropositionBO> returnList = new List<PropositionBO>();
               var fullList = uow.PropositionRepository.GetAll(customerId);
                foreach (var prop in fullList)
                {
                        returnList.Add(propConv.Convert(prop));
                }
                Console.Write(returnList.ToString());
                
                return returnList;
            }
        }
    }
}
