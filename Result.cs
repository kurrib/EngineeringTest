namespace EngineeringTest
{
    public class Result
    {
        public string SourceIP { get; set; }
        public string Environment { get; set; }

        public Result(string sourceIp, string environment)
        {
            SourceIP = sourceIp;
            Environment = environment;
        }
    }
}
