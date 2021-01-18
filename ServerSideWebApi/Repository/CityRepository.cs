using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerSideWebApi.Db;
using ServerSideWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace ServerSideWebApi.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly CityContext _cityContext;
        public CityRepository(CityContext cityContext)
        {
            _cityContext = cityContext;
        }

        public CityContext cityContext {
            get => _cityContext;
        }

        public async Task<int> CreateCity(City city)
        {
            try
            {
                if (_cityContext != null)
                {
                    await _cityContext.City.AddAsync(city);
                    await _cityContext.SaveChangesAsync();
                    return city.ID;
                }
              

            }
            catch (Exception)
            {
                return -1;
            }

            return 0;
        }

        public async  Task<int> DeleteCity(int id)
        {
            int result = 0;
           
            if (_cityContext != null)
            {

                var post =  (from a in _cityContext.City
                                  where a.ID == id
                                  select a).FirstOrDefault();

                if (post != null)
                {
                    //Delete that post
                    _cityContext.City.Remove(post);

                    //Commit the transaction
                    result = await _cityContext.SaveChangesAsync();
                }
                return result;
            }

            return result;

        }

        public List<City> GetAllCities()
        {
            var city = _cityContext.City.ToList();

            return city;
        }

        public async Task<List<City>> GetCities()
        {

            var allcities = await _cityContext.City.ToListAsync();

            return allcities;
        }

        public async Task<City> GetCity(int id)
        {
            if (_cityContext != null)
            {
                return await (from a in _cityContext.City
                              where a.ID == id
                              select new City
                              {
                                  ID = a.ID,
                                  CountryCode = a.CountryCode , 
                                  District = a.District ,
                                  Name = a.Name,
                                  Population = a.Population
                              }).FirstOrDefaultAsync();


            
                
            }
            return null;
        }

        public City GetSingleCity(int id)
        {
            var city_data = _cityContext.City.FirstOrDefault(a => a.ID == id);

            if (city_data == null)
            {
                return null;
            }
            return city_data;
        }

        public async Task<int> PartialUpdate(int id, string Name)
        {
            var selectedCity = _cityContext.City.FirstOrDefaultAsync(a => a.ID == id);

            selectedCity.Result.Name = Name;

           int result = await _cityContext.SaveChangesAsync();

            return result;
        }

        public async Task<City> UpdateCity(int id, City city)
        {
            var selectedcity = (from a in _cityContext.City
                                where a.ID == id
                                select new City
                                {
                                    Name = a.Name , 
                                    CountryCode = a.CountryCode , 
                                    District = a.District , 
                                    ID = a.ID,
                                    Population = a.Population
                                }).FirstOrDefault();

            selectedcity.Name = city.Name;
            selectedcity.Population = city.Population;


            var updatecity = _cityContext.City.Update(selectedcity);

            await _cityContext.SaveChangesAsync();

            return city;
        }

      
    }
}
