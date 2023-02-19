using Alfred.Sensors;

using AlfredUtilities.Sensors;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Alfred.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {
        private ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public ActionResult<List<Sensor>> GetAll()
            => _sensorService.Sensors.ToList();

        [HttpGet("{id}")]
        public ActionResult<Sensor> Get(Guid id)
        {
            Sensor sensor = _sensorService.Read(id);

            return sensor;
        }

        [HttpPut("{id}")]
        public ActionResult<Sensor> Update(Guid id, Sensor sensor)
        {
            if (_sensorService.Update(id, sensor))
            {
                return _sensorService.Read(id);
            }

            return sensor;
        }
    }
}


