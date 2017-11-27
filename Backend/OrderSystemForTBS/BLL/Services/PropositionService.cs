using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class PropositionService : IService<PropositionBO>
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

        public List<PropositionBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.PropositionRepository.GetAll().Select(propConv.Convert).ToList(); ;
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
            throw new NotImplementedException();
        }

        public PropositionBO Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
