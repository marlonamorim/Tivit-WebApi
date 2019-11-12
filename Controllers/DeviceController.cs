using System.Collections.Generic;
using System.Threading.Tasks;
using Tivit.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Tivit.WebApi.Services;
using System;
using Microsoft.AspNetCore.Cors;

namespace Tivit.WebApi.Controllers
{
    [ApiController]
    [Route("v1/devices")]
    public class DeviceController : ControllerBase
    {
        [EnableCors("AllowSpecific")]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Device>>> Get([FromServices] DeviceService service)
        {
            var devices = await service.Get();
            return devices;
        }

        [EnableCors("AllowSpecific")]
        [HttpGet]
        [Route("dataofdevices")]
        public async Task<ActionResult<List<DataOfDevice>>> GetDataOfDevices([FromServices] DataOfDeviceService dataOfDeviceService, 
        DateTime? dateIni = null, DateTime? dateEnd = null)
        {
            var dataOfDevices = await dataOfDeviceService.Get(dateIni, dateEnd);
            return dataOfDevices;
        }

        [EnableCors("AllowSpecific")]
        [HttpGet]
        [Route("dataofdevices/{id}")]
        public async Task<ActionResult<List<DataOfDevice>>> GetDataOfDevicesById([FromServices] DataOfDeviceService dataOfDeviceService, string id)
        {
            var dataOfDevices = await dataOfDeviceService.GetByDeviceId(id);
            return dataOfDevices;
        }

        [EnableCors("AllowSpecific")]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Device>> Post([FromServices] DeviceService service, [FromBody] Device model)
        {
            if(ModelState.IsValid)
            {
                service.Create(model);
                await service.Get(model.Id);
                return model;
            }
            else
                return BadRequest(ModelState);
        }

        [EnableCors("AllowSpecific")]
        [HttpPost]
        [Route("dataofdevices/{id}")]
        public async Task<ActionResult<DataOfDevice>> CollectOfData([FromServices] DeviceService service, 
            [FromServices] DataOfDeviceService dataOfDeviceService, 
            [FromBody] DataOfDevice model, string id)
        {
            if(ModelState.IsValid)
            {
                var device = await service.Get(id);
                model.Device = device;
                model.CreationDate = DateTime.Now.Date;

                dataOfDeviceService.Create(model);
                return model;
            }
            else
                return BadRequest(ModelState);
        }
    }
}