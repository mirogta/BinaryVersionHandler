using System.IO;
using BinaryVersion.Handler.Model;
using System;

namespace BinaryVersion.Handler.Response
{
    /// <summary>
    /// Interface for response handlers.
    /// </summary>
    public interface IResponseHandler
    {
        /// <summary>
        /// ContentType of the response
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Delegate to serialize the result to the output stream
        /// </summary>
        SerializeResponse Serialize { get; set; }
    }

    /// <summary>
    /// SerializeResponse handler is serializing the <paramref name="obj"/> to the <paramref name="stream"/>.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="obj"></param>
    /// <remarks>
    /// I have started with Func in .NET 4.5, then Action in .NET 3.5 and ended up using a basic delegate to make the library work in .NET 2.0.
    /// </remarks>
    public delegate void SerializeResponse(Stream stream, IResponseType obj);
}