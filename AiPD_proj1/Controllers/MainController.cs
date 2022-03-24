using Microsoft.AspNetCore.Mvc;
using System;
using AiPD_proj1.Services;
using AiPD_proj1.Models;
using NAudio.Wave;
using AiPD_proj1.Helpers;

namespace AiPD_proj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private IFileService charts;

        public MainController(IFileService chartService)
        {
            charts = chartService;
        }
        [HttpPost]
        [Route("analysis")]
        public AnalysisResponse GetAnalysis(FileName name)
        {
            (float[], WaveFormat) clip = charts.GetClip(name.Name);
            AnalysisUnit unit = new AnalysisUnit(Array.ConvertAll(clip.Item1, x => (double)x), clip.Item2, name.Name, charts.GetChart(name.Name));
            unit.Analyse();
            return unit.GetResponse();
        }
        [HttpPost]
        [Route("save")]
        public bool SaveCSV(FileName name)
        {
            (float[], WaveFormat) clip = charts.GetClip(name.Name);
            AnalysisUnit unit = new AnalysisUnit(Array.ConvertAll(clip.Item1, x => (double)x), clip.Item2, name.Name, charts.GetChart(name.Name));
            unit.Analyse();
            return charts.SaveCSV(unit.GetResponse());
        }
        [HttpGet]
        [Route("files")]
        public string[] GetFiles()
        {
            return charts.GetFileNames();
        }
    }
}
