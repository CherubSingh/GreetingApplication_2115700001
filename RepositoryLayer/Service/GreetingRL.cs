using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class GreetingRL:IGreetingRL
    {
        private readonly GreetingDbContext _dbContext;
        public GreetingRL(GreetingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GreetingEntity AddGreeting(SaveGreetingModel greetRequest)
        {
            var result = _dbContext.Greetings.FirstOrDefault<GreetingEntity>(e => e.GreetingMessage == greetRequest.GreetingMessage);
            if (result == null)
            {
                var greet = new GreetingEntity
                {
                    GreetingMessage = greetRequest.GreetingMessage
                };


                _dbContext.Add(greet);
                _dbContext.SaveChanges();
                return greet;
            }
            return result;
        }
    }
}
