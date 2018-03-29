using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Common.Extension
{
    public static class MetadataExtension
    { 
        /// <summary>
        /// Get metdata entry. If does not exists, return null.
        /// </summary>
    public static Metadata.Entry Get(this Metadata metadata, string key, bool ignoreCase = true)
    {
        foreach (var entry in metadata)
        {
            if ((ignoreCase && entry.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                || entry.Key == key)
            {
                return entry;
            }
        }

        return null;
    }


    /// <summary>
    /// Get metadata value.
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="key">Metadata key</param>
    /// <param name="ignoreCase">Case ingre</param>
    /// <returns>If the key does not exists, return null.</returns>
    public static string GetValue(this Metadata metadata, string key, bool ignoreCase = true)
    {
        return Get(metadata, key, ignoreCase)?.Value;
    }

    /// <summary>
    /// Get metadata requestId value.
    /// </summary>
    /// <param name="metadata"></param>
    /// <returns>If it does not exists, return null.</returns>
    public static string GetRequestId(this Metadata metadata)
    {
        return GetValue(metadata, Constant.RequestIdName);
    }

    public static string GetCorrelationId(this Metadata metadata)
    {
        return GetValue(metadata, Constant.CorrelationIdName);
    }

    public static string GetVersion(this Metadata metadata)
    {
        return GetValue(metadata, Constant.VersionName);
    }

    public static Metadata AddVersion(this Metadata metadata, string version)
    {
        metadata.Add(Constant.VersionName, version);
        return metadata;
    }

    public static Metadata AddRequestId(this Metadata metadata, string requestId)
    {
        metadata.Add(Constant.RequestIdName, requestId);
        return metadata;
    }

    public static Metadata AddCorrelationId(this Metadata metadata, string correlationId)
    {
        metadata.Add(Constant.CorrelationIdName, correlationId);
        return metadata;
    }
}
}
