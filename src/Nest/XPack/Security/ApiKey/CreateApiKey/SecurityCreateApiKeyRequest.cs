﻿using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(SecurityCreateApiKeyRequestJsonConverter))]
	public partial interface ISecurityCreateApiKeyRequest
	{
		/// <summary>
		/// Name for this API key
		/// </summary>
		[JsonProperty("name")]
		string Name { get; set; }

		/// <summary>
		/// Optional expiration for the API key being generated.
		/// If expiration is not provided then the API keys do not expire.
		/// </summary>
		[JsonProperty("expiration")]
		Time Expiration { get; set; }

		/// <summary>
		/// Optional role descriptors for this API key, if not provided then permissions of authenticated user are applied.
		/// </summary>
		[JsonProperty("role_descriptors")]
		RoleDescriptors RoleDescriptors { get; set; }
	}

	public partial class SecurityCreateApiKeyRequest
	{
		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public Time Expiration { get; set; }

		/// <inheritdoc />
		public RoleDescriptors RoleDescriptors { get; set; }
	}

	[DescriptorFor("SecurityCreateApiKey")]
	public partial class SecurityCreateApiKeyDescriptor
	{
		/// <inheritdoc />
		string ISecurityCreateApiKeyRequest.Name { get; set; }

		/// <inheritdoc />
		Time ISecurityCreateApiKeyRequest.Expiration { get; set; }

		/// <inheritdoc />
		RoleDescriptors ISecurityCreateApiKeyRequest.RoleDescriptors { get; set; }

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Name" />
		public SecurityCreateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Expiration" />
		public SecurityCreateApiKeyDescriptor Expiration(Time expiration) => Assign(expiration, (a, v) => a.Expiration = v);
	}

	public class RoleDescriptors : IsADictionaryBase<string, RoleDescriptor>
	{

	}

	public class RoleDescriptor
	{

	}

	internal class SecurityCreateApiKeyRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var apiKeyRequest = value as ISecurityCreateApiKeyRequest;
			writer.WriteStartObject();

			if (!string.IsNullOrEmpty(apiKeyRequest.Name))
			{
				writer.WritePropertyName("name");
				serializer.Serialize(writer, apiKeyRequest.Name);
			}

			if (apiKeyRequest.Expiration != null)
			{
				writer.WritePropertyName("expiration");
				serializer.Serialize(writer, apiKeyRequest.Expiration);
			}

			writer.WritePropertyName("role_descriptors");
			serializer.Serialize(writer, apiKeyRequest.RoleDescriptors ?? new object()); // server expects {} if null

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
