using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            BonusTwo();
            //BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            int allUsers = _context.Users.ToList().Count;
            Console.WriteLine(allUsers);
            
        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Price > 150);

            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Name.Contains("s"));
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users.Where(u => u.RegistrationDate.Value.Year < 2016);
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Email} | {user.RegistrationDate}");
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users.Where(u => u.RegistrationDate.Value.Year > 2016 && u.RegistrationDate.Value.Year < 2018);
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Email} | {user.RegistrationDate}");
            }
        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retrieves all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.

            //var user = _context.Users.Where(u => u.Email == "afton@gmail.com");
            var products = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Email == "afton@gmail.com");
            foreach (ShoppingCart shoppingCart in products)
            {
                Console.WriteLine($"Name: {shoppingCart.Product.Name} Price:{shoppingCart.Product.Price} Quantity:{shoppingCart.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var products = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(products);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of users who have the role of "Employee"/2.
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            // Tables needed: Shopping Cart: UserId & ProductId | UserRoles: UserId & RoleId
            var employeeUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Employee").Select(ur => ur.User.Email).ToList();
            var employeeCarts = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Include(sc => sc.User.UserRoles);
            foreach (ShoppingCart shoppingCart in employeeCarts)
            {   
                if (employeeUsers.Contains(shoppingCart.User.Email))
                {
                    Console.WriteLine($"Employee Email: {shoppingCart.User.Email} | Product: {shoppingCart.Product.Name}, Price:${shoppingCart.Product.Price}, Qty: {shoppingCart.Quantity}");
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Xbox Series X",
                Description = "A next-generation gaming console.",
                Price = 500m,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Xbox Series X").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Xbox Series X").SingleOrDefault();
            product.Price = 400m;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var deleteUser = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(deleteUser);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            var users = _context.Users;
            foreach(var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    Console.WriteLine("Signed In!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                    break;
                }
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.

            var users = _context.Users.ToList();
            decimal combinedTotal = 0;
            //var userTotal = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.UserId == User.Id);
            

            foreach (var user in users)
            {
                //exception on line below
                var userTotal = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Id == user.Id).Select(sc => sc.Product.Price * sc.Quantity).Sum();
                Console.WriteLine($"Email: {user.Email} Total: ${userTotal}");
                combinedTotal += userTotal.Value;
            }
            Console.WriteLine($"Combined Total: ${combinedTotal}");
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user successfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not successfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials
            Console.WriteLine("Welcome! Please enter your email address and password to sign in.");
            bool signedIn = false;
            var users = _context.Users.ToList();
            int id = 0;
            Dictionary<string, string> validUsers = new Dictionary<string, string>();
            foreach (var user in users)
            {
                validUsers.Add(user.Email, user.Password);
            }

            do
            {
                Console.WriteLine("Enter email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();
                
                if (validUsers.ContainsKey(email) && validUsers.ContainsValue(password))
                {
                    signedIn = true;
                    Console.WriteLine("Successfully logged in!");
                }
                else
                {
                    Console.WriteLine("Invalid email or password. Please try again.");
                }
                
                
            }
            while (!signedIn);
            
            var signedInUser = users[id];
            var allProducts = _context.Products.ToList();
            var productsInCart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.UserId == signedInUser.Id).Select(sc => sc.Product).ToList();
            bool logOut = false;

            string mainMenu = $@" 
                Main Menu:
                Enter '1' to view all products in your shopping cart.
                Enter '2' to view all products that are available.
                Enter '3' to add a product to your shopping cart.
                Enter '4' to remove a product from your shopping cart.
                Enter '5' to log-out and end your session.
                ";

            do
            {
                Console.WriteLine(mainMenu);
                string menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        foreach (var product in productsInCart)
                        {
                            Console.WriteLine($"Product: {product.Name} | Price: ${product.Price}");
                        }
                        break;
                    case "2":
                        Console.WriteLine("All Products available for purchase:");
                        foreach (var product in allProducts)
                        {
                            Console.WriteLine($"Product: {product.Name} | ${product.Price}");
                        }
                        break;
                    case "3":

                        foreach (var product in allProducts)
                        {
                            Console.WriteLine($"ID: {product.Id} | {product.Name} | ${product.Price} ");
                        }
                        Console.WriteLine($"Choose a product by 'ID' to add to your cart");
                        int productId = Convert.ToInt32(Console.ReadLine());


                        if (productsInCart.Contains(allProducts[productId - 1]))
                        {
                            var cart = _context.ShoppingCarts.Where(sc => sc.UserId == signedInUser.Id && sc.ProductId == productId).SingleOrDefault();
                            //var cart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => sc.User.Id == signedInUser.Id && sc.Product.Id == productId).SingleOrDefault();

                            cart.UserId = signedInUser.Id;
                            cart.Quantity++;
                            cart.ProductId = productId;
                            _context.ShoppingCarts.Update(cart);
                        }
                        else
                        {
                            ShoppingCart newShoppingCart = new ShoppingCart()
                            {
                                UserId = signedInUser.Id,
                                ProductId = productId,
                                Quantity = 1
                            };
                            _context.ShoppingCarts.Add(newShoppingCart);
                        }
                        _context.SaveChanges();
                        break;
                    case "4":
                        foreach (var product in productsInCart)
                        {
                            Console.WriteLine($"ID: {product.Id} | {product.Name} | ${product.Price}");
                        }
                        Console.WriteLine($"Choose a product by 'ID' to REMOVE from your cart");
                        int removeProduct = Convert.ToInt32(Console.ReadLine());
                        var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.UserId == signedInUser.Id && sc.ProductId == removeProduct);
                        foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
                        {
                            _context.ShoppingCarts.Remove(userProductRelationship);
                        }
                        _context.SaveChanges();
                        break;
                    case "5":
                        logOut = true;
                        break;
                }
            }
            while (!logOut);
        }
    }
}
