using Microsoft.AspNetCore.Mvc;

namespace WhatSappWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Aplicação rodando....");
        }
    }
}
