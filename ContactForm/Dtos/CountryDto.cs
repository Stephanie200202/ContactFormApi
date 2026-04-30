namespace ContactForm.Dtos
{
    public class CountryDto
    {
        public string Name { get; set; } = string.Empty;
        public string OfficialName { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Subregion { get; set; } = string.Empty;
        public long Population { get; set; } 
        public string FlagUrl { get; set; } = string.Empty;
    }
}
