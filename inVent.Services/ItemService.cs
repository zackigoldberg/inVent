using inVent.Data;
using inVent.Models.ItemModels;
using inVent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Services
{
    public class ItemService
    {
        private readonly Guid _userId;

        public ItemService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateItem(ItemCreate model)
        {
            var entity =
                new Item()
                {
                    UserId = _userId,
                    Name = model.Name,
                    Stock = model.Stock,
                    Description = model.Description,
                    Price = model.Price
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItemListItem> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {

                var query =
                       ctx
                       .Items
                       .Select(
                        e =>
                      new ItemListItem
                      {
                          ItemNumber = e.ItemNumber,
                          Name = e.Name,
                          Description = e.Description,
                          Price = e.Price,
                          Stock = e.Stock

                      }
                      );
                return query.ToArray();
            }
        }

        public ItemDetail GetItemByNumber(int itemNumber)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemNumber == itemNumber);
                return
                    new ItemDetail
                    {
                        ItemNumber = entity.ItemNumber,
                        Name = entity.Name,
                        Description = entity.Description,
                        Price = entity.Price,
                        Stock = entity.Stock
                    };
            }
        }

        public bool UpdateItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemNumber == model.ItemNumber);
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Stock = model.Stock;
                entity.Price = model.Price;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int itemNumber)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Items
                    .Single(e => e.ItemNumber == itemNumber && e.UserId == _userId);
                ctx.Items.Remove(entity);


                return ctx.SaveChanges() == 1;
            }
        }
    }
}
