using Domain.ProductAgg;
using Domain.SaleAgg;
using Framework.OperationResult;
using Resources.Messages;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Sales;
using Domain.Users;

namespace Application.SaleApp
{
    public class SaleApplication : ISaleApplication
    {
        private readonly ISaleRepository _repository;

        public SaleApplication(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddSale(CommonViewModel sale)
        {
            var res = new OperationResult();

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            var _sale = new Sale()
            {
                Price = sale.Price,
                Number = sale.Number,
                UserId = sale.UserId,
                ProductId = sale.ProductId,
            };

            await _repository.CreateAsync(_sale);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteSale(Guid Id)
        {
            var res = new OperationResult();
            var saleForDelete = await _repository.GetAsync(Id);

            if (saleForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(saleForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllSale()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var sales = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var sale in sales)
            {
                _data.Add(new CommonViewModel
                {
                    Id = sale.Id,
                    Price = sale.Price,
                    UserId = sale.UserId,
                    Number = sale.Number,
                    ProductId = sale.ProductId,
                    InsertDateTime = sale.InsertDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetSale(Guid Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var sale = await _repository.GetAsync(Id);

            var _sale = new CommonViewModel
            {
                Id = sale?.Id,
                Price = sale.Price,
                UserId = sale.UserId,
                Number = sale.Number,
                ProductId = sale.ProductId,
                InsertDateTime = sale?.InsertDateTime,
            };

            res.Data = _sale;

            return res;
        }

        public async Task<OperationResult> UpdateSale(CommonViewModel product)
        {
            var res = new OperationResult();

            var saleForUpdate = await _repository.GetAsync(product.Id.Value);

            if (saleForUpdate == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
                res.Succeeded = false;
                return res;
            }

            saleForUpdate.Number = product.Number;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
