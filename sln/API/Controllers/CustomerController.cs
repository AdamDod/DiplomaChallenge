using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using Classes;

namespace API
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerHandler _CustomerHandler = new CustomerHandler();

        
        [HttpGet]
        [EnableCors("MyPolicy")]
        [Route("/Customer")]
        public IEnumerable<Customer> Test()
        {
            return _CustomerHandler.GetCustomer();
        }
      
        
    }
}