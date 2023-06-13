using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Framework.OperationResult;
using Resources.Messages;
using ViewModels.Pages.Admin.TotalSales;

namespace Application.TotalSaleApp
{
    public class TotalSaleApplication : ITotalSaleApplication
    {
        private readonly ITotalSaleRepository _repository;

        public TotalSaleApplication(ITotalSaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddTotalSale(CommonViewModel totalsale)
        {
            var res = new OperationResult();

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            var _totalsale = new TotalSale()
            {
                UserId = totalsale.UserId,
                Number = totalsale.Number,
                Products = totalsale.Products,
                TotalPrice = totalsale.TotalPrice,
                FactorNumber = totalsale.FactorNumber,
            };

            await _repository.CreateAsync(_totalsale);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteTotalSale(Guid Id)
        {
            var res = new OperationResult();
            var totalsaleForDelete = await _repository.GetAsync(Id);

            if (totalsaleForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(totalsaleForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllTotalSale()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var totalsales = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var totalsale in totalsales)
            {
                _data.Add(new CommonViewModel
                {
                    Id = totalsale.Id,
                    UserId = totalsale.UserId,
                    Number = totalsale.Number,
                    Products = totalsale.Products,
                    TotalPrice = totalsale.TotalPrice,
                    FactorNumber = totalsale.FactorNumber,
                    InsertDateTime = totalsale.InsertDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetTotalSale(Guid Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var totalsale = await _repository.GetAsync(Id);

            var _totalsale = new CommonViewModel
            {
                Id = totalsale.Id,
                Number = totalsale.Number,
                UserId = totalsale.UserId,
                Products = totalsale.Products,
                TotalPrice = totalsale.TotalPrice,
                FactorNumber = totalsale.FactorNumber,
                InsertDateTime = totalsale.InsertDateTime,
            };

            res.Data = _totalsale;

            return res;
        }
    }
}
