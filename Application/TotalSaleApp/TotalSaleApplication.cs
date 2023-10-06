using Domain.ProductAgg;
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
                Color = totalsale.Color,
                UserId = totalsale.UserId,
                Number = totalsale.Number,
                Products = totalsale.Products,
                PicAddress = totalsale.PicAddress,
                TotalPrice = totalsale.TotalPrice,
                FactorNumber = totalsale.FactorNumber,
                TrackingCode = totalsale.TrackingCode,
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

        public async Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllTotalSale()
        {
            var res = new OperationResultWithData<IList<DetailsViewModel>>();

            var totalsales = await _repository.GetAllAsync();

            var _data = new List<DetailsViewModel>();

            foreach (var totalsale in totalsales)
            {
                _data.Add(new DetailsViewModel
                {
                    Id = totalsale.Id,
                    Color = totalsale.Color,
                    UserId = totalsale.UserId,
                    Number = totalsale.Number,
                    Posted = totalsale.Posted,
                    Packing = totalsale.Packing,
                    Accepted = totalsale.Accepted,
                    Delivery = totalsale.Delivery,
                    Products = totalsale.Products,
                    TotalPrice = totalsale.TotalPrice,
                    FactorNumber = totalsale.FactorNumber,
                    TrackingCode = totalsale.TrackingCode,
                    InsertDateTime = totalsale.InsertDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<DetailsViewModel>> GetTotalSale(Guid Id)
        {
            var res = new OperationResultWithData<DetailsViewModel>();

            var totalsale = await _repository.GetAsync(Id);

            var _totalsale = new DetailsViewModel
            {
                Id = totalsale.Id,
                Color = totalsale.Color,
                Number = totalsale.Number,
                UserId = totalsale.UserId,
                Posted = totalsale.Posted,
                Packing = totalsale.Packing,
                Accepted = totalsale.Accepted,
                Delivery = totalsale.Delivery,
                Products = totalsale.Products,
                PicAddress = totalsale.PicAddress,
                TotalPrice = totalsale.TotalPrice,
                FactorNumber = totalsale.FactorNumber,
                TrackingCode = totalsale.TrackingCode,
                InsertDateTime = totalsale.InsertDateTime,
            };

            res.Data = _totalsale;

            return res;
        }

        public async Task<OperationResult> UpdateTotalSale(UpdateViewModel totalsale)
        {
            var res = new OperationResult();

            var totalsaleForUpdate = await _repository.GetAsync(totalsale.Id.Value);

            if (totalsaleForUpdate == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
                res.Succeeded = false;
                return res;
            }

            totalsaleForUpdate.Posted = totalsale.Posted;
            totalsaleForUpdate.Packing = totalsale.Packing;
            totalsaleForUpdate.Accepted = totalsale.Accepted;
            totalsaleForUpdate.Delivery = totalsale.Delivery;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
