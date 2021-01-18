using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ServerSideWebApi.Db;
using ServerSideWebApi.Model;
using ServerSideWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var cities = _cityRepository.GetCities();

                return Ok(cities.Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured " + ex.Message);
            }
           
        }


        [HttpGet]
        [Route("getsinglecity")]
        public  IActionResult GetCity(int  id)
        {
            try
            {
               // int id_pass = int.Parse(id);
                var city =  _cityRepository.GetCity(id);
                if (city == null)
                {
                    return Ok("No Data Found ");
                }
                return Ok(city.Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured" + ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteCity")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            
            try
            {
                var deletedcity = await _cityRepository.DeleteCity(id);
                 return Ok("Data is Deleted " + deletedcity);
              
            }
            catch (Exception ex)
            {
                return  BadRequest("Error occured " + ex.Message);
            }

        }

        [HttpPut]
        [Route("updatecity")]
        public ActionResult UpdateCity (int id , [FromBody]City city)
        {
            try
            {
                var updatedCity = _cityRepository.UpdateCity(id, city);

                return Ok(updatedCity);
            }
            catch (Exception ex)
            {
                return BadRequest("Exception occured " + ex.Message);
            }
        }


        //JsonPatch Patch Example
        [HttpPatch]
        [Route("PatchCity")]
        public async  Task<IActionResult> UpdatePatch(int id , JsonPatchDocument<City> CityPatch)
        {
            var database =  _cityRepository.cityContext;
            var entity = _cityRepository.GetSingleCity(id);
            if (entity == null)
            {
                return NotFound();
            }

            CityPatch.ApplyTo(entity, ModelState);

            var isValid = TryValidateModel(CityPatch);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await database.SaveChangesAsync();

            return Ok(entity);
        }

        //Normal Patch way
        [HttpPatch]
        [Route("citypatch")]
        public IActionResult PatchCity(int id , [FromBody]string name)
        {
            var data = _cityRepository.PartialUpdate(id, name);

            if (data.Result > 0)
            {
                return Ok(data);
            }

            return BadRequest();


        }



    }
}
