using lensLook.Dal.Context;
using lensLook.Dal.Models;
using lensLook.Pl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace lensLook.Pl
{
    public static class SeedData
    {
        #region Seed Data ( Brands)
        public static async Task Seed(UserManager<user> _userManager, RoleManager<IdentityRole> _RoleManager, LensLookDbContext _context)
        {

            if (!_RoleManager.Roles.Any())
            {



                await _RoleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _RoleManager.CreateAsync(new IdentityRole() { Name = "Doctor" });
                await _RoleManager.CreateAsync(new IdentityRole() { Name = "Patient" });

            }

            if (!_userManager.Users.Any())
            {
                try
                {

                    user model = new user()
                    {
                        FirstName = "Kariman",
                        LastName = "Samir",
                        Email = "test@mail.com",
                        UserName = "HambozoCom",
                        DisplayName = "Hambozo11",
                        DateOfBirth= DateTime.Now,
                        IsActive = true,
                        RoleName= "Admin"
                    };
                    var resulte = await _userManager.CreateAsync(model, "Hambozo123@@##");

                    if (resulte.Succeeded)
                    {
                        var RoleName = _RoleManager.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
                       await _userManager.AddToRoleAsync(model, RoleName.Name.ToString());
                        var basketCustomer = new BasketCustomer
                        {
                            UserId = model.Id // Set the UserId property
                                              // other properties initialization if any
                        };

                        // Add the basketCustomer to the user's basket
                        model.BasketCustomers = basketCustomer;
                        _context.BasketCustomers.Add(basketCustomer);
                        _context.SaveChanges();
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);

                    throw;
                }



                try
                {

                    user model = new user()
                    {
                        FirstName = "Kariman",
                        LastName = "Samir",
                        Email = "test1@mail.com",
                        UserName = "HambozoCom2",
                        DisplayName = "Hambozo11",
                        RoleName = "Patient"
                    };
                    var resulte = await _userManager.CreateAsync(model, "Hambozo123@@##");

                    if (resulte.Succeeded)
                    {
                        var RoleName = _RoleManager.Roles.Where(x => x.Name == "Patient").FirstOrDefault();
                        await _userManager.AddToRoleAsync(model, RoleName.Name.ToString());
                        var basketCustomer = new BasketCustomer
                        {
                            UserId = model.Id // Set the UserId property
                                              // other properties initialization if any
                        };

                        // Add the basketCustomer to the user's basket
                        model.BasketCustomers = basketCustomer;
                        _context.BasketCustomers.Add(basketCustomer);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);

                    throw;
                }











                try
                {

                    user model = new user()
                    {
                        FirstName = "Kariman",
                        LastName = "Samir",
                        Email = "test4@mail.com",
                        UserName = "HambozoCom1",
                        DisplayName = "Hambozo11",
                        RoleName = "Doctor"
                    };
                    var resulte = await _userManager.CreateAsync(model, "Hambozo123@@##");

                    if (resulte.Succeeded)
                    {
                        var RoleName = _RoleManager.Roles.Where(x => x.Name == "Doctor").FirstOrDefault();
                       await _userManager.AddToRoleAsync(model, RoleName.Name.ToString());
                        var basketCustomer = new BasketCustomer
                        {
                            UserId = model.Id // Set the UserId property
                                              // other properties initialization if any
                        };

                        // Add the basketCustomer to the user's basket
                        model.BasketCustomers = basketCustomer;
                        _context.BasketCustomers.Add(basketCustomer);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);

                    throw;
                }
















































            }












            if (!_context.Services.Any())
            {
                List<Services> x = new List<Services> { };
                foreach (var item in Enum.GetValues(typeof(BookingType)))
                {
                    BookingType bookingType = (BookingType)item;
                    x.Add(new Services() { BookingType = bookingType });

                }

                _context.Services.AddRange(x);
                _context.SaveChanges();

            }

        }
        #endregion

    }
}
