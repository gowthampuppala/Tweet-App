using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using test.DAOLayer;
using test.DAOLayer.Entities;

namespace test.ServiceLayer
{
    public class UserReg
    {
        public void UserRegistration()
        {
            var db = new TweetDbContext();

            var user = new UserDetails();

            Console.WriteLine("Please enter User Details : ");
            Console.WriteLine("Enter your First Name");
            user.First_Name = Console.ReadLine();
            Console.WriteLine("Enter your Last Name");
            user.Last_Name = Console.ReadLine();
            Console.WriteLine("Enter your Gender");
            user.Gender = Console.ReadLine();
            Console.WriteLine("Enter your Dob");
            DateTime dateValue;
            DateTime.TryParse(Console.ReadLine(), out dateValue);
            user.DoB = dateValue;
            Console.WriteLine("Enter your E-Mail");
            user.Email = Console.ReadLine();
            Console.WriteLine("Enter your password");
            user.Password = Console.ReadLine();
            Console.WriteLine("Enter a security question");
            user.securityQuestion = Console.ReadLine();
            Console.WriteLine("Enter the Answer for the security question");
            user.Answer = Console.ReadLine();

            var userDetails = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userDetails is null)
            {
                var context = new ValidationContext(user, null, null);

                var valresult = new List<ValidationResult>();
                bool isValid = Validator.TryValidateObject(user, context, valresult, true);
                foreach (var item in valresult)
                {
                    Console.WriteLine(item.ErrorMessage);
                }
                if (isValid)
                {
                    try
                    {
                        db.Users.Add(user); 
                        db.SaveChanges();
                        Console.WriteLine("User Registration Successful");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("User Registration Failed");
                }
            }
            else
            {
                Console.WriteLine("User Already exists , try with another mail");
            }
        }
    }
}
