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
                    Name = model.Name,
                    Description = model.Description,
                    PackSize = model.PackSize
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
                          PackSize = e.PackSize
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
                        PackSize = entity.PackSize
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
                entity.PackSize = model.PackSize;

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
                    .Single(e => e.ItemNumber == itemNumber);
                ctx.Items.Remove(entity);


                return ctx.SaveChanges() == 1;
            }
        }
    }
}
