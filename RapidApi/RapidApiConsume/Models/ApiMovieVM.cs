namespace RapidApiConsume.Models
{
    /// <summary>
    /// rapid api üzerinden gelen datalar ile isimler aynı olmalı
    /// </summary>
    public class ApiMovieVM
    {
        public int rank { get; set; }
        public string title { get; set; }
        public string rating { get; set; }
        public string trailer { get; set; }
    }
}
