using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
               

        public async Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();

            // 1. Llamamos al repositorio de productos a través del Unit of Work
            var products = await _unitOfWork.Product.ListProducts(filters);

            if (products is not null)
            {
                response.IsSuccess = true;
                // 2. Mapeamos de la entidad (o base de respuesta de entidad) al DTO de respuesta
                response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<ProductResponseDto>> ProductById(int productId)
        {
            var response = new BaseResponse<ProductResponseDto>();

            // Usamos GetEntityQuery para poder meter los Include sin tocar el Repositorio
            var product = await _unitOfWork.Product
                .GetEntityQuery(x => x.Id == productId && x.AuditDeleteDate == null)
                .AsNoTracking()
                .Include(x => x.Category) // <--- Esto llena el objeto Category
                .Include(x => x.Provider) // <--- Esto llena el objeto Provider
                .FirstOrDefaultAsync();

            if (product is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductResponseDto>(product);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var product = _mapper.Map<Product>(requestDto);

            response.Data = await _unitOfWork.Product.RegisterAsync(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productById = await ProductById(productId);

            if (productById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var product = _mapper.Map<Product>(requestDto);
            product.Id = productId;
            response.Data = await _unitOfWork.Product.EditAsync(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveProduct(int productId)
        {
            var response = new BaseResponse<bool>();
            var productById = await ProductById(productId);

            if (productById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.Data = await _unitOfWork.Product.RemoveAsync(productId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}