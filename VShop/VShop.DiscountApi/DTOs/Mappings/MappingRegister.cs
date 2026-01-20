using Mapster;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.DTOs.Mappings;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CouponDTO,Coupon>().TwoWays();
    }
}
