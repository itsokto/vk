using System.Threading;
using System.Threading.Tasks;
using Flurl;

namespace VkNet.Infrastructure.Authorization.ImplicitFlow
{
	/// <summary>
	/// Authorization Form Html Parser
	/// </summary>
	public interface IAuthorizationFormHtmlParser
	{
		/// <summary>
		/// Get form data
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Результат с данными формы</returns>
		Task<VkHtmlFormResult> GetFormAsync(Url url, CancellationToken cancellationToken = default);
	}
}