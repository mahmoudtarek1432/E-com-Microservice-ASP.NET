using Dapper;
using Discount.API.Entites;
using Npgsql;

namespace Discount.API.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string? _ConnectionString;

        public DiscountRepository(IConfiguration configuration)
        {
            var cofig = configuration?? throw new ArgumentNullException(nameof(configuration)); 
            _ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }
        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_ConnectionString);
            var ResultCoupon = await connection.ExecuteAsync
                ($"INSERT INTO Coupon (ProductName, Description, Amount) VALUE (@ProductName, @Description, @Amount)",
                new { ProductName=coupon.ProductName, Descrption=coupon.Description, Amount=coupon.Amount });

            //number of affected rows
            if (ResultCoupon == 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteCoupon(string CouponName)
        {
            using var connection = new NpgsqlConnection(_ConnectionString);
            var results = await connection.ExecuteAsync
                ($"DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = CouponName });

            if (results == 0)
                return false;
            return true;
        }

        public async Task<Coupon> GetCoupon(string CouponName)
        {
            using var connection = new NpgsqlConnection(_ConnectionString);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ($"SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = CouponName });

            if (coupon == null)
                return new Coupon
                { Amount = 0, Description = "Coupon Code not valid", ProductName = "No Discount" };
            return coupon;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_ConnectionString);
            var Result = await connection.ExecuteAsync
                ($"UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { ProductName = coupon.ProductName, Descrption = coupon.Description, Amount = coupon.Amount });

            //number of affected rows
            if (Result == 0)
                return false;
            return true;
        }
    }
}
