using System;
using System.Threading.Tasks;

namespace EGAZT.Helper
{
	public interface IForceUpdate
	{
        Task FetchAndActivateAsync();
        string GetValue(string key);

    }
}

