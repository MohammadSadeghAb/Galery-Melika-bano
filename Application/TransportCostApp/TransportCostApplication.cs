using Domain.ProductAgg;
using Domain.TransportCostAgg;
using Framework.OperationResult;
using Resources.Messages;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.TransportCosts;

namespace Application.TransportCostApp
{
    public class TransportCostApplication : ITransportCostApplication
    {
        private readonly ITransportCostRepository _repository;

        public TransportCostApplication(ITransportCostRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddTransportCost(CommonViewModel transportcost)
        {
            var res = new OperationResult();

            if (_repository.Exist(x => x.Maximum_Weight == transportcost.Maximum_Weight))
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.Weight));
            }

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            var _transportcost = new TransportCost()
            {
                Price = transportcost.Price,
                IsActive = transportcost.IsActive,
                Maximum_Weight = transportcost.Maximum_Weight,
            };

            await _repository.CreateAsync(_transportcost);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteTransportCost(Guid Id)
        {
            var res = new OperationResult();
            var transportcostForDelete = await _repository.GetAsync(Id);

            if (transportcostForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(transportcostForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllTransportCost()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var transportcosts = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var transportcost in transportcosts)
            {
                _data.Add(new CommonViewModel
                {
                    Id = transportcost.Id,
                    Price = transportcost.Price,
                    IsActive = transportcost.IsActive,
                    Maximum_Weight = transportcost.Maximum_Weight,
                    UpdateDateTime = transportcost.UpdateDateTime,
                    InsertDateTime = transportcost.InsertDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<TransportCost>> GetByWeight(int weight)
        {
            var res = new OperationResultWithData<TransportCost>();

            var transportcost = await _repository.GetByWeight(weight);

            res.Data = transportcost;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetTransportCost(Guid? Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var transportcost = await _repository.GetAsync(Id);

            var _transportcost = new CommonViewModel
            {
                Id = transportcost.Id,
                Price = transportcost.Price,
                IsActive = transportcost.IsActive,
                Maximum_Weight = transportcost.Maximum_Weight,
                UpdateDateTime = transportcost.UpdateDateTime,
                InsertDateTime = transportcost.InsertDateTime,
            };

            res.Data = _transportcost;

            return res;
        }

        public async Task<OperationResult> UpdateTransportCost(CommonViewModel transportcost)
        {
            var res = new OperationResult();

            var transportcostForUpdate = await _repository.GetAsync(transportcost.Id.Value);

            if (transportcostForUpdate == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
                res.Succeeded = false;
                return res;
            }

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            transportcostForUpdate.SetUpdateDateTime();

            transportcostForUpdate.Price = transportcost.Price;
            transportcostForUpdate.IsActive = transportcost.IsActive;
            transportcostForUpdate.Maximum_Weight = transportcost.Maximum_Weight;


            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
