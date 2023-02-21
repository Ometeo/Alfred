using Alfred.Sensors;

using AlfredUtilities.Sensors;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Alfred.Controllers
{
    /// <summary>
    /// Controller providing the REST API for the sensor inside Alfred.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Get all sensors. The sensors are fetched from an injected <see cref="ISensorService"/>.
        /// </summary>
        /// <returns><see cref="OkObjectResult"/> containing the sensors list.</returns>
        [HttpGet]
        public IActionResult GetAll() =>
                Ok(_sensorService.Sensors.ToList());


        /// <summary>
        /// Get a <see cref="Sensor"/> by its <see cref="Guid"/> Id.
        /// </summary>
        /// <param name="id">Id of the wanted sensor.</param>
        /// <returns><see cref="OkObjectResult"/> containing the <see cref="Sensor"/> as its value if the id is found in the sensors collection.
        /// <para><see cref="NotFoundResult"/> otherwise.</para></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Sensor sensor = _sensorService.Read(id);

            return Sensor.Null == sensor 
                ? NotFound() 
                : Ok(sensor);
        }


        /// <summary>
        /// Update a sensor.
        /// </summary>
        /// <param name="id">Id of the sensor to update.</param>
        /// <param name="sensor">New value of the sensor.</param>
        /// <returns><see cref="OkObjectResult"/> containing the updated <see cref="Sensor"/>.
        /// <para><see cref="NotFoundResult"/> if the sensor's id is unknown.</para>
        /// <para><see cref="BadRequestResult"/> if the sensor's id and the id in the request aren't the same.</para></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }

            return _sensorService.Update(id, sensor) 
                ? Ok(_sensorService.Read(id)) 
                : NotFound();
        }
    }
}


