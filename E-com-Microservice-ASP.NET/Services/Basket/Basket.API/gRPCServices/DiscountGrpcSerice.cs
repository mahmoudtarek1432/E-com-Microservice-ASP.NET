using Discount.Grpc.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountGrpcSerice
    {
        //public readonly DiscountProtoService.DiscountProtoServiceClient _DiscountService;

        public DiscountGrpcSerice(/*DiscountProtoService.DiscountProtoServiceClient discountProtoService*/)
        {
            //_DiscountService= discountProtoService;
        }

        public async Task<CouponModel> GetDiscountCoupon( string ProductName)
        {
            var request = new GetDiscountRequest { ProductName = ProductName };
            return null;//await _DiscountService.GetDiscountAsync(request);

        }
    }
}
