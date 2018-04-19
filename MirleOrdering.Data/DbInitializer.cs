using MirleOrdering.Data.Models;
using System;
using System.Linq;

namespace MirleOrdering.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MirleOrderingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Categories.Any() || context.Products.Any() || context.Users.Any())
            {
                return;   // DB has been seeded
            }
            var addOn = DateTime.Now;
            // Add categories
            var category1 = new Category { Name = "蘋果便當", Seq = 1, AddedOn = addOn };
            var category2 = new Category { Name = "曾師傅便幫", Seq = 2, AddedOn = addOn };
            var categories = new Category[]
            {
                category1,
                category2
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
            // Add products
            var products = new Product[]
            {
                // category1
                new Product {Name = "排骨", Price = 60, CategoryId = category1.Id, AddedOn=addOn, Description = "招牌"},
                new Product {Name = "雞腿", Price = 65, CategoryId = category1.Id, AddedOn=addOn},
                new Product {Name = "煎魚", Price = 55, CategoryId = category1.Id, AddedOn=addOn},
                new Product {Name = "五穀", Price = 50, CategoryId = category1.Id, AddedOn=addOn, Description="素食"},
                // category2
                new Product {Name = "排骨", Price = 60, CategoryId = category2.Id, AddedOn=addOn},
                new Product {Name = "雞排", Price = 65, CategoryId = category2.Id, AddedOn=addOn},
                new Product {Name = "豬排", Price = 55, CategoryId = category2.Id, AddedOn=addOn},
                new Product {Name = "魚排", Price = 55, CategoryId = category2.Id, AddedOn=addOn},
            };
            context.Products.AddRange(products);
            context.SaveChanges();
            // Add roles and groups
            var roleAdmin = new Role { Name = "Admin", AddedOn = addOn };
            var roleUser = new Role { Name = "User", AddedOn = addOn };
            var roles = new Role[]
            {
                roleAdmin,
                roleUser,
            };
            context.Roles.AddRange(roles);
            var groupManagement = new Group { Name = "Management", AddedOn = addOn };
            var groupIT = new Group { Name = "IT", AddedOn = addOn };
            var groups = new Group[]
            {
                groupManagement,
                groupIT,
            };
            context.Groups.AddRange(groups);
            context.SaveChanges();
            // Add users
            var users = new User[]
            {
                new User{ Name="John",Email="john@mail.com",RoleId=roleAdmin.Id, GroupId=groupManagement.Id,Password="0000", AddedOn=addOn},
                new User{ Name="Bob",Email="bob@mail.com",RoleId=roleAdmin.Id, GroupId=groupIT.Id,Password="0000", AddedOn=addOn},
                new User{ Name="Peter",Email="peter@mail.com",RoleId=roleUser.Id, GroupId=groupIT.Id,Password="0000", AddedOn=addOn}
            };
            context.Users.AddRange(users);
            context.SaveChanges();
            // Add setting
            var setting = new Setting { StopHourOn = 11, Announcement = "測試中", AddedOn = addOn };
            context.Settings.Add(setting);
            context.SaveChanges();
        }
    }
}
