﻿using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace TeslaCamPlayer.BlazorHosted.Client.Helpers;

public static class HttpClientNewtonsoftJsonHelper
{
    public static async Task<TValue> GetFromNewtonsoftJsonAsync<TValue>(
        this HttpClient client,
        [StringSyntax(StringSyntaxAttribute.Uri)] string requestUri) =>
        JsonConvert.DeserializeObject<TValue>(await client.GetStringAsync(requestUri));
}
