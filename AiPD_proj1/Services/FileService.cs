using System.Collections.Generic;
using AiPD_proj1.Models;
using NAudio.Wave;
using System.IO;

namespace AiPD_proj1.Services
{
    public interface IFileService
    {
        public double[][] GetChart(string path);
        public string[] GetFileNames();
        public (float[], WaveFormat) GetClip(string filename);
        public bool SaveCSV(AnalysisResponse data);
    }
    public class FileService : IFileService
    {
        public double[][] GetChart(string name)
        {
            string fullPath = $"..\\records\\{name}";
            var res = new List<double[]>();
            if (!File.Exists(fullPath)) return null;
            using (WaveFileReader reader = new WaveFileReader(fullPath))
            {
                var sampleProvider = reader.ToSampleProvider();
                var format = sampleProvider.WaveFormat;
                float[] floatsBuffer = new float[reader.Length / 2];
                sampleProvider.Read(floatsBuffer, 0, floatsBuffer.Length);
                double sampleSecDelta = (double)1.0 / format.SampleRate;
                for (int i = 0; i < floatsBuffer.Length; i++)
                {
                    if(i%10 == 0)
                        res.Add(new double[] { i * sampleSecDelta, floatsBuffer[i]});
                }
            }
            return res.ToArray();
        }

        public (float[], WaveFormat) GetClip(string name)
        {
            float[] floatsBuffer;
            WaveFormat format;
            string fullPath = $"..\\records\\{name}";
            if (!File.Exists(fullPath)) return (null, null);
            var res = new List<double[]>();
            if (!File.Exists(fullPath)) return (null, null);
            using (WaveFileReader reader = new WaveFileReader(fullPath))
            {
                var sampleProvider = reader.ToSampleProvider();
                format = sampleProvider.WaveFormat;
                floatsBuffer = new float[reader.Length / 2];
                sampleProvider.Read(floatsBuffer, 0, floatsBuffer.Length);
            }
            return (floatsBuffer, format);
        }


        public string[] GetFileNames()
        {
            string path = $"..\\records";
            var fullNames = Directory.GetFiles(path);
            List<string> names = new List<string>();
            foreach( string fullName in fullNames)
            {
                var splitted = fullName.Split('\\');
                names.Add(splitted[splitted.Length - 1]);
            }
            return names.ToArray();
        }

        public bool SaveCSV(AnalysisResponse data)
        {
            string fullPath = $"..\\csv_files\\{data.filename}.csv";
            using (var w = new StreamWriter(fullPath))
            {
                var line = $"\"time\", \"volume\", \"ste\", \"zcr\", \"sr\", \"vstd\", \"vdr\", \"lster\", \"zstd\", \"hzcrr\"";
                w.WriteLine(line);
                w.Flush();
                var time = data.Volume[0][0];
                var vol = data.Volume[0][1];
                var ste = data.STE[0][1];
                var zcr = data.ZCR[0][1];
                var sr = data.SR[0][1];
                var vstd = data.VSTD;
                var vdr = data.VDR;
                var lster = data.LSTER;
                var zstd = data.ZSTD;
                var hzcrr = data.HZCRR;
                line = $"\"{time}\", \"{vol}\", \"{ste}\", \"{zcr}\", \"{sr}\", \"{vstd}\", \"{vdr}\", \"{lster}\", \"{zstd}\", \"{hzcrr}\"";
                w.WriteLine(line);
                w.Flush();
                for (int i = 1; i < data.SR.Length; i++)
                {
                    time = data.Volume[i][0];
                    vol = data.Volume[i][1];
                    ste = data.STE[i][1];
                    zcr = data.ZCR[i][1];
                    sr = data.SR[i][1];
                    line = $"\"{time}\", \"{vol}\", \"{ste}\", \"{zcr}\", \"{sr}\"\"";
                    w.WriteLine(line);
                    w.Flush();
                }
            }
            return true;
        }
    }
}