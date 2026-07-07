using AutoMapper;
using Azure;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI
{
    ///<summary>
    ///  What is Profile?
    ///  Profile is a class from AutoMapper.It holds mapping rules between different object types.
    ///  What does CreateMap<TSource, TDestination>() do?
    ///  This tells AutoMapper:
    ///  "When needed, convert a CartHeader object into a CartHeaderDto object."
    ///  CartHeader header = new CartHeader();
    ///  CartHeaderDto dto = mapper.Map<CartHeaderDto>(header);
    ///  AutoMapper copies matching properties automatically.
    ///  What does ReverseMap() do?
    ///  Without ReverseMap():
    ///  CreateMap<CartHeader, CartHeaderDto>();
    ///  you can only map:
    ///  CartHeader -> CartHeaderDto
    ///  With ReverseMap():
    ///  CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
    ///  CartHeader -> CartHeaderDto
    ///  CartHeaderDto -> CartHeader
    ///  So these two lines create mappings for:
    ///  CartHeader ↔ CartHeaderDto
    ///  CartDetails ↔ CartDetailsDto
    ///  Simple analogy
    ///  Think of CartHeader as your database/entity model and CartHeaderDto as a package used for API requests/responses.
    ///  AutoMapper acts like a translator that automatically copies data between them so you don't have to write:
    ///  dto.Id = entity.Id;
    ///  dto.Name = entity.Name;
    ///  dto.Total = entity.Total;
    ///  </summary>
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
