namespace Drive.Client.Models.Medias {
    public abstract class PickedMediaBase {
        public string DataBase64 { get; set; }

        public string Name { get; set; }

        public byte[] Body { get; set; }
    }
}
