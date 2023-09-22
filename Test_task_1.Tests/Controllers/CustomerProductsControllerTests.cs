using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_task_1.Services;

namespace Test_task_1.Tests.Controllers
{
    public class CustomerProductsControllerTests
    {
        private readonly MongoDBService _mongoDBService;
        public CustomerProductsControllerTests(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }
    }
}
