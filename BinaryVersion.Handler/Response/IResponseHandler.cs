using BinaryVersion.Handler.Model;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// Interface for response handlers.
    /// </summary>
    public interface IResponseHandler
    {
        /// <summary>
        /// Writes the object to the response.
        /// </summary>
        /// <param name="obj"></param>
        void Write(IResponseType obj);
    }
}