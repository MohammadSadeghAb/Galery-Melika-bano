using Domain.ProductAgg;
using Domain.ProductPicAgg;
using Framework.OperationResult;
using Resources.Messages;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.ProductPics;

namespace Application.ProductPicApp
{
    public class ProductPicApplication : IProductPicApplication
    {
        private readonly IProductPicRepository _repository;
        public ProductPicApplication(IProductPicRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddProductPic(CommonViewModel productpic)
        {
            var res = new OperationResult();

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            if (_repository.Exist(x => x.Product_Id == productpic.Product_Id))
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.ProductPicture));
                res.Succeeded = false;
                return res;
            }

            var _productpic = new ProductPic()
            {
                Product_Id = productpic.Product_Id,
                PicAddress1 = productpic.PicAddress1,
                PicAddress2 = productpic.PicAddress2,
                PicAddress3 = productpic.PicAddress3,
                PicAddress4 = productpic.PicAddress4,
                PicAddress5 = productpic.PicAddress5,
            };

            await _repository.CreateAsync(_productpic);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteProductPic(Guid Id)
        {
            var res = new OperationResult();
            var productpicForDelete = await _repository.GetAsync(Id);

            if (productpicForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(productpicForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<CommonViewModel>>> GetAllProductPic()
        {
            var res = new OperationResultWithData<IList<CommonViewModel>>();

            var productpics = await _repository.GetAllAsync();

            var _data = new List<CommonViewModel>();

            foreach (var productpic in productpics)
            {
                _data.Add(new CommonViewModel
                {
                    Id = productpic.Id,
                    PicAddress1 = productpic.PicAddress1,
                    PicAddress2 = productpic.PicAddress2,
                    PicAddress3 = productpic.PicAddress3,
                    PicAddress4 = productpic.PicAddress4,
                    PicAddress5 = productpic.PicAddress5,
                    Product_Id = productpic.Product_Id,
                    UpdateDateTime = productpic.UpdateDateTime,
                    InsertDateTime = productpic.InsertDateTime,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<CommonViewModel>> GetProductPic(Guid Id)
        {
            var res = new OperationResultWithData<CommonViewModel>();

            var productpic = await _repository.GetAsync(Id);

            var _productpic = new CommonViewModel
            {
                Id = productpic.Id,
                PicAddress1 = productpic.PicAddress1,
                PicAddress2 = productpic.PicAddress2,
                PicAddress3 = productpic.PicAddress3,
                PicAddress4 = productpic.PicAddress4,
                PicAddress5 = productpic.PicAddress5,
                Product_Id = productpic.Product_Id,
                UpdateDateTime = productpic.UpdateDateTime,
                InsertDateTime = productpic.InsertDateTime,
            };

            res.Data = _productpic;

            return res;
        }

        public async Task<OperationResult> UpdateProductPic(CommonViewModel productpic)
        {
            var res = new OperationResult();

            var productpicForUpdate = await _repository.GetAsync(productpic.Id.Value);

            if (productpicForUpdate == null)
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

            productpicForUpdate.SetUpdateDateTime();

            productpicForUpdate.PicAddress1 = productpic.PicAddress1;
            productpicForUpdate.PicAddress2 = productpic.PicAddress2;
            productpicForUpdate.PicAddress3 = productpic.PicAddress3;
            productpicForUpdate.PicAddress4 = productpic.PicAddress4;
            productpicForUpdate.PicAddress5 = productpic.PicAddress5;
            productpicForUpdate.Product_Id = productpic.Product_Id;

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
