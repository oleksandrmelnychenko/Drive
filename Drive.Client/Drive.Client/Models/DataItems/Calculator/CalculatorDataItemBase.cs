namespace Drive.Client.Models.DataItems.Calculator {
    public class CalculatorDataItemBase<TData> {

        public string IconPath { get; set; }

        /// <summary>
        /// TODO: replace on `STRING SOURCE`
        /// </summary>
        public string Titile { get; set; }

        public TData Data { get; set; }
    }
}
