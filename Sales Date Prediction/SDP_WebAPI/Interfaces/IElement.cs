using Microsoft.Data.SqlClient;

namespace SDP_WebAPI.Interfaces;

public interface IElement
{
    public static abstract IElement FromADOReader(SqlDataReader reader);
}