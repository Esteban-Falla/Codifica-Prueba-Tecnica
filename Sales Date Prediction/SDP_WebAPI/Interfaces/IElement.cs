using Microsoft.Data.SqlClient;

namespace SDP_WebAPI.Interfaces;

public interface IElement
{
    public static abstract T FromADOReader<T>(SqlDataReader reader) where T : IElement;
}