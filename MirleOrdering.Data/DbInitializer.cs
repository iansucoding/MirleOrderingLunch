using MirleOrdering.Data.Models;
using System;
using System.Linq;

namespace MirleOrdering.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MirleOrderingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            var addOn = DateTime.Now;
            // Add categories
            var categories = new Category[]
            {
                new Category { Name = "一般", Seq = 1, AddedOn=addOn},
                new Category { Name = "素食", Seq = 2, AddedOn=addOn}
            };
            foreach (var item in categories)
            {
                if (!context.Categories.Where(x => x.Name == item.Name).Any())
                {
                    context.Categories.Add(item);
                }
            }
            context.SaveChanges();
            // Add products
            var generalProductId = context.Categories.Where(x => x.Name == "一般").First().Id;
            var VegetarianProductId = context.Categories.Where(x => x.Name == "素食").First().Id;
            var products = new Product[]
            {
                new Product {Name = "排骨", Price = 60, CategoryId = generalProductId, AddedOn=addOn},
                new Product {Name = "雞腿", Price = 65, CategoryId = generalProductId, AddedOn=addOn},
                new Product {Name = "煎魚", Price = 55, CategoryId = generalProductId, AddedOn=addOn},
                new Product {Name = "五穀", Price = 50, CategoryId = VegetarianProductId, AddedOn=addOn}
            };
            foreach (var item in products)
            {
                context.Products.Add(item);
            }
            context.SaveChanges();
            // Add Role
            var roles = new Role[]
            {
                new Role { Name="Admin", AddedOn=addOn },
                new Role { Name="User", AddedOn=addOn },
            };
            foreach (var item in roles)
            {
                if (!context.Roles.Where(x => x.Name == item.Name).Any())
                {
                    context.Roles.Add(item);
                }
            }
            context.SaveChanges();
            // Add Group 
            var groups = new Group[]
            {
                new Group { Name="Management", AddedOn=addOn },
                new Group { Name="IT", AddedOn=addOn },
            };
            foreach (var item in groups)
            {
                if (!context.Groups.Where(x => x.Name == item.Name).Any())
                {
                    context.Groups.Add(item);
                }
            }
            context.SaveChanges();
            // Add User
            var roleAdminId = context.Roles.Where(x => x.Name == "Admin").First().Id;
            var roleUserId = context.Roles.Where(x => x.Name == "User").First().Id;
            var groupManagementId = context.Groups.Where(x => x.Name == "Management").First().Id;
            var groupItId = context.Groups.Where(x => x.Name == "IT").First().Id;

            var users = new User[]
            {
                new User{ Name="John",Email="john@mail.com",RoleId=roleAdminId, GroupId=groupManagementId,Password="0000", AddedOn=addOn},
                new User{ Name="Bob",Email="bob@mail.com",RoleId=roleAdminId, GroupId=groupItId,Password="0000", AddedOn=addOn},
                new User{ Name="Peter",Email="peter@mail.com",RoleId=roleUserId, GroupId=groupItId,Password="0000", AddedOn=addOn}
            };
            foreach (var item in users)
            {
                if (!context.Users.Where(x => x.Name == item.Name).Any())
                {
                    context.Users.Add(item);
                }
            }
            context.SaveChanges();
            //
        }
    }
}
