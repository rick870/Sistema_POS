using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class SaleApplication : ISaleApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SaleApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        public async Task<BaseResponse<BaseEntityResponse<SaleResponseDto>>> ListSales(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<SaleResponseDto>>();

            try
            {
                // 1. Delegamos la lógica de filtrado y paginación al Repositorio (vía UoW)
                // Nota: Asegúrate de que tu interfaz ISaleRepository tenga definido el método ListSales
                var sales = await _unitOfWork.Sale.ListSales(filters);

                if (sales is not null)
                {
                    response.IsSuccess = true;
                    // 2. Mapeamos el BaseEntityResponse<Sale> al BaseEntityResponse<SaleResponseDto>
                    response.Data = _mapper.Map<BaseEntityResponse<SaleResponseDto>>(sales);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                // Cambia la línea de abajo para ver el error real en Swagger
                response.Message = ex.Message + (ex.InnerException != null ? " | " + ex.InnerException.Message : "");
                
            }

            return response;
        }


        public async Task<BaseResponse<SaleByIdResponseDto>> SaleById(int saleId)
        {
            var response = new BaseResponse<SaleByIdResponseDto>();

            try
            {
                // 1. Obtenemos la cabecera
                var sale = await _unitOfWork.Sale.GetByIdAsync(saleId);

                if (sale is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                // 2. Obtenemos los detalles explícitamente
                var saleDetails = await _unitOfWork.SaleDetail.GetSaleDetailBySaleId(saleId);

                if (saleDetails != null && saleDetails.Items != null)
                {
                    // IMPORTANTÍSIMO: Asignamos la lista a la entidad ANTES del mapeo
                    sale.SaleDetails = saleDetails.Items.ToList();
                }

                // 3. Mapeamos la entidad (que ya tiene los detalles cargados) al DTO
                response.Data = _mapper.Map<SaleByIdResponseDto>(sale);

                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "ERROR: " + ex.Message;
            }

            return response;
        }



        public async Task<BaseResponse<bool>> RegisterSale(SaleRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                // 1. Mapeo del DTO de la petición a la entidad de dominio Sale
                var sale = _mapper.Map<Sale>(requestDto);

                // Asignación del estado activo
                sale.State = (int)StateTypes.Active;

                // 2. Registro de la venta (Cabecera y Detalles)
                // Se guarda el resultado booleano en response.Data siguiendo el modelo de RegisterProduct
                response.Data = await _unitOfWork.Sale.RegisterAsync(sale);

                // 3. Validación del resultado del registro
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE; //
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y registro de errores
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION; //
              
            }

            return response;
        }

        public async Task<BaseResponse<bool>> CancelSale(int saleId)
        {
            var response = new BaseResponse<bool>();

            // 1. Validar si la venta existe antes de intentar eliminarla
            var saleById = await SaleById(saleId);

            if (saleById.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            try
            {
                // 2. Ejecutar la eliminación (o anulación lógica)
                // Se guarda el resultado en Data siguiendo tu modelo de RemoveProduct
                response.Data = await _unitOfWork.Sale.RemoveAsync(saleId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE; // O MESSAGE_CANCEL según tus constantes
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                // 3. Manejo de excepciones
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
               
            }

            return response;
        }

        
    }

    
}
