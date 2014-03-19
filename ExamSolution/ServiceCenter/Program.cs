using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exam.Model;
using Exam.Repositories;

namespace ServiceCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorEntity a = ActorRepository.Instance.GetActor(1);
        }
    }
}
