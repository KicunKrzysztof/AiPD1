using System;
using System.Collections.Generic;
using System.Linq;
using AiPD_proj1.Models;
using NAudio.Wave;

namespace AiPD_proj1.Helpers
{
    public class AnalysisUnit
    {
        private readonly int frameTimespan = 25;
        private readonly int overlapTimespanDelta = 15;
        private readonly int halfSeconfSamplesAmount;
        private int frameSamplesAmount;
        private int overlapSamplesAmount;
        private double[] clip;
        private WaveFormat format;
        private string filename;
        private double[][] chart;

        private List<double> volume;
        private List<double> STE;
        private List<double> ZCR;
        private List<double> SR;//0 - silent, 1 - non silent

        private double VSTD;
        private double VDR;
        private double LSTER;
        private double ZSTD;
        private double HZCRR;
        public AnalysisUnit(double[] clip, WaveFormat form, string filename, double[][] chart)
        {
            this.clip = clip;
            format = form;
            this.filename = filename;
            this.chart = chart; 
            frameSamplesAmount = (int)(format.SampleRate / 1000.0) * frameTimespan;
            overlapSamplesAmount = (int)(format.SampleRate / 1000.0) * overlapTimespanDelta;
            halfSeconfSamplesAmount = (int)(format.SampleRate / 1000.0) * 500;
        }
        public void Analyse()
        {
            CalculateVolume();
            CalculateSTE();
            CalculateZCR();
            CalculateSR();
            CalculateVSTD();
            CalculateVDR();
            CalculateLSTER();
            CalculateZSTD();
            CalculateHZCRR();
        }
        public AnalysisResponse GetResponse()
        {
            return new AnalysisResponse()
            {
                filename = this.filename,
                Chart = this.chart,
                Volume = this.CreateChart(this.volume),
                STE = this.CreateChart(this.STE),
                ZCR = this.CreateChart(this.ZCR),
                SR = this.CreateChart(this.SR.Cast<double>().ToList()),
                VSTD = this.VSTD,
                VDR = this.VDR,
                LSTER = this.LSTER,
                ZSTD = this.ZSTD,
                HZCRR = this.HZCRR
            };
    }
        private double[][] CreateChart(List<double> sequence)
        {
            double overlapTimespanSeconds = overlapTimespanDelta * 0.001;
            var chart = new List<double[]>();
            for(int i = 0; i < sequence.Count; i++)
            {
                chart.Add(new double[] { i * overlapTimespanSeconds, sequence[i] });
            }
            return chart.ToArray();
        }

        private double CalculateSecondWindowAvarage(int index, List<double> sequence)
        {
            int startIndex = index - halfSeconfSamplesAmount < 0 ? 0 : index - halfSeconfSamplesAmount;
            int endIndex = index + halfSeconfSamplesAmount > sequence.Count ? sequence.Count : index + halfSeconfSamplesAmount;
            double subsum = 0;
            for(int i = startIndex; i < endIndex; i++)
                subsum += sequence[i];
            return subsum / (endIndex - startIndex);
        }

        private void CalculateVolume()
        {
            var vol = new List<double>();
            for(int i = 0; i + frameSamplesAmount < clip.Length; i += overlapSamplesAmount)
            {
                double subsum = 0;
                for(int j = 0; j < frameSamplesAmount; j++)
                {
                    subsum += clip[i + j] * clip[i + j];
                }
                vol.Add(Math.Sqrt(subsum / frameSamplesAmount));
            }
            this.volume = vol;
        }
        private void CalculateSTE()
        {
            var ste = new List<double>();
            for (int i = 0; i + frameSamplesAmount < clip.Length; i += overlapSamplesAmount)
            {
                double subsum = 0;
                for (int j = 0; j < frameSamplesAmount; j++)
                {
                    subsum += clip[i + j] * clip[i + j];
                }
                ste.Add(subsum);
            }
            this.STE = ste;
        }
        private void CalculateZCR()
        {
            var zcr = new List<double>();
            for (int i = 0; i + frameSamplesAmount < clip.Length; i += overlapSamplesAmount)
            {
                double subsum = 0;
                for (int j = 1; j < frameSamplesAmount; j++)
                {
                    subsum += Math.Abs(Math.Sign(clip[i + j]) - Math.Sign(clip[i + j - 1]));
                }
                zcr.Add((subsum * format.SampleRate) / (2 * frameSamplesAmount));
            }
            this.ZCR = zcr;
        }
        private void CalculateSR()
        {
            var sr = new List<double>();
            for(int i = 0; i < volume.Count; i++)
                sr.Add(volume[i] < 0.02 && ZCR[i] < 50 ? 0 : 1);//było 0.02 i 50 ale nie działało
            this.SR = sr;
        }
        private void CalculateVSTD()
        {
            double avg = volume.Average();
            double subsum = 0;
            for (int i = 0; i < volume.Count; i++)
                subsum += Math.Pow(volume[i] - avg, 2);
            VSTD = Math.Sqrt(subsum / volume.Count);
        }
        private void CalculateVDR()
        {
            var min = volume.Min();
            var max = volume.Max();
            VDR = (max - min) / max;
        }
        private void CalculateLSTER()
        {
            double subsum = 0;
            for (int i = 0; i < STE.Count; i++)
                subsum += Math.Sign(0.5 * CalculateSecondWindowAvarage(i, STE) - STE[i]) + 1;
            LSTER = subsum / (2 * STE.Count);
        }
        private void CalculateZSTD()
        {
            double avg = ZCR.Average();
            double subsum = 0;
            for (int i = 0; i < ZCR.Count; i++)
                subsum += Math.Pow(ZCR[i] - avg, 2);
            ZSTD = Math.Sqrt(subsum / ZCR.Count);
        }
        private void CalculateHZCRR()
        {
            double subsum = 0;
            for (int i = 0; i < ZCR.Count; i++)
                subsum += Math.Sign(ZCR[i] - 1.5 * CalculateSecondWindowAvarage(i, ZCR)) + 1;
            HZCRR = subsum / (2 * ZCR.Count);
        }
    }
}