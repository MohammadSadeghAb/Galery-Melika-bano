using Domain.ProductAgg;
using Framework.OperationResult;
using Resources;
using Resources.Messages;
using ViewModels.Pages.Admin.Products;

namespace Application.ProductApp
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;
        public ProductApplication(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> AddProduct(CommonViewModel product)
        {
            var res = new OperationResult();

            if (_repository.Exist(x => x.Name_Product == product.Name_Product))
            {
                res.AddErrorMessage(string.Format(Errors.AlreadyExists, DataDictionary.ProductName));
            }

            if (0 < res.ErrorMessages.Count)
            {
                res.Succeeded = false;
                return res;
            }

            var _product = new Product()
            {
                Type = product.Type,
                Price = product.Price,
                Gender = product.Gender,
                Number = product.Number,
                Color1_rgb = product.Color1_rgb,
                Color1_text = product.Color1_text,
                Name_Product = product.Name_Product,
                Discount_Major = product.Discount_Major,
                Discount_Single = product.Discount_Single,
                CategoryChild_Id = product.CategoryChild_Id,
                CategoryParent_Id = product.CategoryParent_Id,
            };

            await _repository.CreateAsync(_product);
            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResult> DeleteProduct(Guid Id)
        {
            var res = new OperationResult();
            var productForDelete = await _repository.GetAsync(Id);

            if (productForDelete == null)
            {
                res.AddErrorMessage(Errors.ThereIsNotAnyDataWithThisId);
            }

            if (res.ErrorMessages.Count > 0)
            {
                res.Succeeded = false;
                return res;
            }

            _repository.Remove(productForDelete);

            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }

        public async Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllProduct()
        {
            var res = new OperationResultWithData<IList<DetailsViewModel>>();

            var products = await _repository.GetAllAsync();

            var _data = new List<DetailsViewModel>();

            foreach (var product in products)
            {
                _data.Add(new DetailsViewModel
                {
                    Id = product.Id,
                    Type = product.Type,
                    Price = product.Price,
                    Gender = product.Gender,
                    Number = product.Number,
                    Dimensions = product.Dimensions,
                    Color1_rgb = product.Color1_rgb,
                    Color2_rgb = product.Color2_rgb,
                    Color3_rgb = product.Color3_rgb,
                    Color4_rgb = product.Color4_rgb,
                    Color1_text = product.Color1_text,
                    Color2_text = product.Color2_text,
                    Color3_text = product.Color3_text,
                    Color4_text = product.Color4_text,
                    Name_Product = product.Name_Product,
                    UpdateDateTime = product.UpdateDateTime,
                    Discount_Major = product.Discount_Major,
                    InsertDateTime = product?.InsertDateTime,
                    Discount_Single = product.Discount_Single,
                    CategoryChild_Id = product.CategoryChild_Id,
                    CategoryParent_Id = product.CategoryParent_Id,
                });
            }

            res.Data = _data;

            return res;
        }

        public async Task<OperationResultWithData<DetailsViewModel>> GetProduct(Guid? Id)
        {
            var res = new OperationResultWithData<DetailsViewModel>();

            var product = await _repository.GetAsync(Id);

            var _product = new DetailsViewModel
            {
                Id = product.Id,
                Type = product.Type,
                Price = product.Price,
                Gender = product.Gender,
                Number = product.Number,
                Dimensions = product.Dimensions,
                Color1_rgb = product.Color1_rgb,
                Color2_rgb = product.Color2_rgb,
                Color3_rgb = product.Color3_rgb,
                Color4_rgb = product.Color4_rgb,
                Color1_text = product.Color1_text,
                Color2_text = product.Color2_text,
                Color3_text = product.Color3_text,
                Color4_text = product.Color4_text,
                Name_Product = product.Name_Product,
                UpdateDateTime = product.UpdateDateTime,
                Discount_Major = product.Discount_Major,
                InsertDateTime = product?.InsertDateTime,
                Discount_Single = product.Discount_Single,
                CategoryChild_Id = product.CategoryChild_Id,
                CategoryParent_Id = product.CategoryParent_Id,
            };

            res.Data = _product;

            return res;
        }

        public async Task<OperationResultWithData<Product>> GetProductByProductName(string productname)
        {
            var res = new OperationResultWithData<Product>();

            var product = await _repository.GetByName(productname);

            res.Data = product;

            return res;
        }

        public async Task<OperationResult> UpdateProduct(UpdateViewModel product)
        {
            var res = new OperationResult();

            var productForUpdate = await _repository.GetAsync(product.Id.Value);

            if (productForUpdate == null)
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

            productForUpdate.SetUpdateDateTime();

            productForUpdate.Type = product.Type;
            productForUpdate.Price = product.Price;
            productForUpdate.Gender = product.Gender;
            productForUpdate.Number = product.Number;
            productForUpdate.Dimensions = product.Dimensions;
            productForUpdate.Color1_rgb = product.Color1_rgb;
            productForUpdate.Color2_rgb = product.Color2_rgb;
            productForUpdate.Color3_rgb = product.Color3_rgb;
            productForUpdate.Color4_rgb = product.Color4_rgb;
            productForUpdate.Color1_text = product.Color1_text;
            productForUpdate.Color2_text = product.Color2_text;
            productForUpdate.Color3_text = product.Color3_text;
            productForUpdate.Color4_text = product.Color4_text;
            productForUpdate.Name_Product = product.Name_Product;
            productForUpdate.Discount_Major = product.Discount_Major;
            productForUpdate.Discount_Single = product.Discount_Single;
            productForUpdate.CategoryChild_Id = product.CategoryChild_Id;
            productForUpdate.CategoryParent_Id = product.CategoryParent_Id;


            await _repository.SaveChangesAsync();
            res.Succeeded = true;
            return res;
        }
    }
}
