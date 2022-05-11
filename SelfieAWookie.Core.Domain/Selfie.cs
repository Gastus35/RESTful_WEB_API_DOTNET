namespace SelfieAWookie.Core.Selfies.Domain
{
    // Represents a selfy of a wookie
    public class Selfie
    {
        #region Properties
        public int Id { get; set; }

        public String Title { get; set; }
        public String Description { get; set; }

        public String? ImagePath { get; set; }

        public int WookieId { get; set; }
        public Wookie Wookie { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; } 
        #endregion
    }
}
