namespace YKToolkit.Sample.Models
{
    /// <summary>
    /// 人物データを表します。
    /// </summary>
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool IsValid { get; set; }
    }
}
