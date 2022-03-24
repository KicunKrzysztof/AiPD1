namespace AiPD_proj1.Models
{
    public class AnalysisResponse
    {
        public string filename { get; set; }
        public double[][] Volume { get; set; }
        public double[][] STE { get; set; }
        public double[][] ZCR { get; set; }
        public double[][] SR { get; set; }
        public double[][] Chart { get; set; }
        public double VSTD { get; set; }
        public double VDR { get; set; }
        public double LSTER { get; set; }
        public double ZSTD { get; set; }
        public double HZCRR { get; set; }
    }
}