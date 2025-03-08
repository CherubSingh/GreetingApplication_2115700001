using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }


        public string GetGreeting(string firstName = "", string lastName = "")
        {
            // Both first and last name provided
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"Hello {firstName} {lastName}";
            }
            // Only first name provided
            else if (!string.IsNullOrEmpty(firstName))
            {
                return $"Hello {firstName}";
            }
            // Only last name provided
            else if (!string.IsNullOrEmpty(lastName))
            {
                return $"Hello {lastName}";
            }
            // No names provided
            else
            {
                return "Hello World";
            }
        }

        public GreetingEntity AddGreeting(SaveGreetingModel greetRequest)
        {
            var result = _greetingRL.AddGreeting(greetRequest);
            return result;
        }

        public string GetGreetingById(int id)
        {
            var result = _greetingRL.GetGreetingById(id);
            return result;
        }
    }
}