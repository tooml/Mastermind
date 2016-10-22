using nblackbox.contract;

namespace mastermind.contracts.events
{
    public class SecretCodeGenerated : IEvent
    {
        public string Context { get; }
        public string Data { get; }
        public string Name { get; }

        public SecretCodeGenerated(string context, string name, string data)
        {
            Context = context;
            Name = name;
            Data = data;
        }
    }
}
