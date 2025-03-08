using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreeting(string firstName = "", string lastName = "");
        GreetingEntity AddGreeting(SaveGreetingModel greetRequest);
        string GetGreetingById(int id);
        List<string> GetAllGreetingMessage();
        GreetingEntity UpdateGreeting(int id, string newMessage);
    }
}