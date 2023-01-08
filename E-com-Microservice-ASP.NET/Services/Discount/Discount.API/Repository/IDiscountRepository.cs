using Discount.API.Entites;

namespace Discount.API.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetCoupon(string CouponName);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string CouponName);
        Task<bool> CreateCoupon(Coupon coupon);
    }
}
