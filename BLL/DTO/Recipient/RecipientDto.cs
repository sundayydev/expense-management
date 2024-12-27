using DAL.Utils;
using System;
using System.Windows.Media;

namespace BLL.DTO.Recipient
{
    public class RecipientDto
    {
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        
        public string Character
        {
            get
            {
                return !string.IsNullOrEmpty(RecipientName) ? RecipientName[0].ToString() : string.Empty;
            }
        }

        public Brush CharacterColor
        {
            get
            {
                return new RandomColorGenerator().GetRandomColor();
            }
        }
    }
}
