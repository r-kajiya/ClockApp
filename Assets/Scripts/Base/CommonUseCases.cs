using System.Collections.Generic;

namespace ClockApp
{
    static class CommonUseCaseType
    {
        public const int FOOTER = 0;
        public const int SCENE = 1;
        public const int MAX = 2;
    }
    
    public class ContextContainer
    {
        public CommonUseCases CommonUseCases { get; }

        public ContextContainer(Dictionary<int, IUseCase> useCases)
        {
            CommonUseCases = new CommonUseCases(useCases);
        }
    }
    
    public class CommonUseCases
    {
        public Dictionary<int, IUseCase> Map { get; }
        
        public CommonUseCases(Dictionary<int, IUseCase> commonUseCases)
        {
            Map = commonUseCases;
        }
    }
}