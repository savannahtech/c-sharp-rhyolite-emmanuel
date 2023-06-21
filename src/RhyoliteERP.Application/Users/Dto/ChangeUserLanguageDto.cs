using System.ComponentModel.DataAnnotations;

namespace RhyoliteERP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}