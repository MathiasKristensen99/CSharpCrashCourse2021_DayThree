using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CrashCourse2021ExercisesDayThree.DB.Impl;
using CrashCourse2021ExercisesDayThree.Models;

namespace CrashCourse2021ExercisesDayThree.Services
{
    public class CustomerService
    {
        CustomerTable db; 
        public CustomerService()
        {
            this.db = new CustomerTable();
        }

        //Create and return a Customer Object with all incoming properties (no ID)
        internal Customer Create(string firstName, string lastName, DateTime birthDate)
        {
            if (firstName.Length >= 2)
            {
                Customer customer = new Customer();
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.BirthDate = birthDate;
                return customer;
            }
            else
            {
                throw new ArgumentException(Constants.FirstNameException);
            }
            
            //throw new NotImplementedException();
        }

        //db has an Add function to add a new customer!! :D
        //We can reuse the Create function above..
        internal Customer CreateAndAdd(string firstName, string lastName, DateTime birthDate)
        {
            Customer customer = new Customer();
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.BirthDate = birthDate;
            db.AddCustomer(customer);
            return customer;
        }

        //Simple enough, Get the customers from the "Database" (db)
        internal List<Customer> GetCustomers()
        {
            //List<Customer> customers = new List<Customer>(db.GetCustomers());
            return db.GetCustomers();
        }

        //Maybe Check out how to find in a LIST in c# Maybe there is a Find function?
        public Customer FindCustomer(int customerId)
        {
            if (customerId < 0)
            {
                throw new InvalidDataException(Constants.CustomerIdMustBeAboveZero);
            }
            else
            {
                List<Customer> customers = GetCustomers();
                return customers.Find(c => c.Id == customerId);
            }
        }
        
        /*So many things can go wrong here...
          You need lots of exceptions handling in case of failure and
          a switch statement that decides what property of customer to use
          depending on the searchField. (ex. case searchfield = "id" we should look in customer.Id 
          Maybe add searchField.ToLower() to avoid upper/lowercase letters)
          Another thing is you should use FindAll here to get all that matches searchfield/searchvalue
          You could also make another search Method that only return One Customer
           and uses Find to get that customer and maybe even test it.
        */
        public List<Customer> SearchCustomer(string searchField, string searchValue)
        {
            List<Customer> customers = GetCustomers();
            //int result;
            //out result;

            if (searchField == null)
            {
                throw new InvalidDataException(Constants.CustomerSearchFieldCannotBeNull);
            }

            if (searchValue == null)
            {
                throw new InvalidDataException(Constants.CustomerSearchValueCannotBeNull);
            }

            switch (searchField.ToLower())
            {
                case "FirstName":
                {
                    return customers.FindAll(customer => customer.FirstName.ToLower().Contains(searchValue));
                    break;
                }
            }
            
            switch (searchField)
            {
                case "LastName":
                {
                    return customers.FindAll(customer => customer.LastName.Contains(searchValue));
                    break;
                }
                case "FirstName":
                {
                    return customers.FindAll(customer => customer.FirstName.Contains(searchValue));
                }
                case "Id":
                {
                    return customers.FindAll(customer => customer.Id.Equals(int.Parse(searchValue)));
                }
            }

            throw new NotImplementedException();
        }
    }
}
