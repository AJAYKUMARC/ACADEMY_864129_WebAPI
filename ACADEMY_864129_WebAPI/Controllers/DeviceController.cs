using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACADEMY_864129_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACADEMY_864129_WebAPI.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ITableStorage tableStorage;
        public DeviceController(ITableStorage tableStorage)
        {
            this.tableStorage = tableStorage;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
