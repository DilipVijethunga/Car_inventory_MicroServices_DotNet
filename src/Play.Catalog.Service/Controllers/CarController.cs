using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarController : ControllerBase
    {
        private static readonly List<CarDto> cars = new()
        {
            new CarDto(Guid.NewGuid(), "Honda", "Civic", "2000 ES8 model car", 3),
            new CarDto(Guid.NewGuid(), "Toyota", "Aqua", "2013 white color", 5),
            new CarDto(Guid.NewGuid(), "Mazda", "123", "Black color", 7)
        };

        //display Data
        [HttpGet]
        public IEnumerable<CarDto> Get()
        {
            return cars;
        }

        //search an item with ID
        [HttpGet("{id}")]
        public CarDto GetById(Guid id)
        {
            var car = cars.Where(car => car.Id == id).SingleOrDefault();
            return car;
        }

        //add a new car
        [HttpPost]
        public ActionResult<CarDto> Post(CreateCarDto createCarDto)
        {
            var car = new CarDto(Guid.NewGuid(), createCarDto.Brand, createCarDto.Model, createCarDto.Description, createCarDto.Price);
            cars.Add(car);

            return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
        }

        //Update an existing car details
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateCarDto updateCarDto)
        {
            var existingCar = cars.Where(car => car.Id == id).SingleOrDefault();

            var updatedCar = existingCar with
            {
                Brand = updateCarDto.Brand,
                Model = updateCarDto.Model,
                Description = updateCarDto.Description,
                Price = updateCarDto.Price
            };

            var index = cars.FindIndex(existingCar => existingCar.Id == id);
            cars[index] = updatedCar;

            return NoContent();
        }

        //Delete an item
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = cars.FindIndex(existingCar => existingCar.Id == id);
            cars.RemoveAt(index);

            return NoContent();
        }
    }
}