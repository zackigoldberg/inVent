﻿using inVent.Contracts;
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
    public class ItemService : IItem
    {
        private readonly Guid _userId;
        private readonly bool _admin;
        private readonly bool _manager;
        public ItemService(Guid userId, bool admin, bool manager)
        {
            _userId = userId;
            _admin = admin;
            _manager = manager;
        }
        public bool CreateItem(ItemCreate model)
        {
            if (_manager || _admin)
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
            return false;
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
                    .Single(e => e.ItemNumber == model.ItemNumber && (_manager || _admin));
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
                    .Single(e => e.ItemNumber == itemNumber && (_manager || _admin));
                ctx.Items.Remove(entity);


                return ctx.SaveChanges() == 1;
            }
        }
    }
}
