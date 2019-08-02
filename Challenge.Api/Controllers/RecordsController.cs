using System;
using System.Collections.Generic;
using System.IO;
using Challenge.Api.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {

        [HttpGet]
        [Route("list")]
        public ActionResult<IEnumerable<RecordObjectModel>> MainRecords()
        {
            if (System.IO.File.Exists("dataArchive.csv"))
            {
                int counter = 0;
                List<RecordObjectModel> recordObjectsList = new List<RecordObjectModel>();
                try
                {
                    using (StreamReader reader = new StreamReader("dataArchive.csv"))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] line = reader.ReadLine().Split('\t');
                            if (counter++ > 0)
                            {
                                var record = new RecordObjectModel { Id = int.Parse(line[0]), Name = line[1], Status = bool.Parse(line[2]) };
                                recordObjectsList.Add(record);
                            }
                        }
                    }
                    return Ok(recordObjectsList);
                }
                catch (Exception excep)
                {
                    throw new ApplicationException($"Ocorreu um erro ao ler os dados no arquivo CSV, causados por: {excep}");
                }
            }
            else
            {
                return BadRequest("Não existe arquivo CSV criado, verifique se o POST JSON já foi realizado.");
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult PostRecords([FromBody]List<RecordObjectModel> jsonReceived)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (TextWriter writer = new StreamWriter("dataArchive.csv", false, System.Text.Encoding.UTF8))
                    {
                        var csvWriter = new CsvWriter(writer);
                        csvWriter.WriteRecords(jsonReceived);
                        writer.Close();
                    }
                }
                catch (Exception excep)
                {
                    throw new ApplicationException($"Ocorreu um erro ao gravar os dados no arquivo CSV, causados por: {excep}");
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}