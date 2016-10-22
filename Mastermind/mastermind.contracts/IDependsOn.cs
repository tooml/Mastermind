using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts
{
    public interface IDependsOn<T>
    {
        void Inject(T viewmodel);
    }
}
