using Domain.ProductAgg;
using Domain.Users;
using Framework.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pages.Admin.Products;

namespace Application.ProductApp
{
    public interface IProductApplication
    {
        Task<OperationResult> AddProduct(CommonViewModel product);
        Task<OperationResult> DeleteProduct(Guid Id);
        Task<OperationResult> UpdateProduct(UpdateViewModel product);
        Task<OperationResultWithData<DetailsViewModel>> GetProduct(Guid? Id);
        Task<OperationResultWithData<IList<DetailsViewModel>>> Getnewest();
        Task<OperationResultWithData<IList<DetailsViewModel>>> GetAllProduct();
        Task<OperationResultWithData<Product>> GetProductByProductName(string productname);
    }
}
