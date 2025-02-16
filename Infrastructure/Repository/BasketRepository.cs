using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        /*private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }*/
        private readonly Context _context;
        public BasketRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            var basket= await _context.CustomerBaskets.Include(e=>e.Items).FirstOrDefaultAsync(e=>e.Id==BasketId);
            if (basket != null)
            {
                 _context.CustomerBaskets.Remove(basket);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var data = await _context.CustomerBaskets.Include(e=>e.Items).FirstOrDefaultAsync(e=>e.Id==BasketId);
            //  return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
            return data;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            /* var Created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
             if (!Created)
                 return null;
             return await GetBasketAsync(basket.Id);*/
            if (_context.CustomerBaskets.Any(e => e.Id == basket.Id))
            {
                 _context.CustomerBaskets.Update(basket);
            }
            else
            {
                _context.CustomerBaskets.Add(basket);
            }
            await _context.SaveChangesAsync();
            return await GetBasketAsync(basket.Id);
        }
    }
}
