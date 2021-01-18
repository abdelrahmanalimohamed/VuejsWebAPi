using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ServerSideWebApi.Db;
using ServerSideWebApi.Model;

namespace ServerSideWebApi.Repository
{
    public interface ICityRepository
    {
        CityContext cityContext { get; }


        List<City> GetAllCities();
        Task <List<City>> GetCities();

        Task <City>  GetCity(int id);

        City GetSingleCity(int id);

        Task<int>  CreateCity(City city);

       Task<int> DeleteCity(int id);


        Task<City> UpdateCity(int id , City city);


        Task<int> PartialUpdate(int id, string Name);


    }
}
