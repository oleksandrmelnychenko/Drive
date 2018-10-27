namespace Drive.Client.Models.Identities.Posts {
    public class MediaPost : PostBase {
        public MediaPost() {
            PostType = PostTypes.MediaPost;
        }

        public string MediaUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQYk-142tJ_3kch9mdKKBHhc-hgs0VyCK02i5xcUBiCvXFRgGhj";
    }
}
