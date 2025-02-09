﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MediaServices.Client;
using Microsoft.WindowsAzure.MediaServices.Client.ContentKeyAuthorization;
using Microsoft.WindowsAzure.MediaServices.Client.DynamicEncryption;
using System.Configuration;
using System.IO;
using System.Threading;
using Microsoft.WindowsAzure;
using System.Security.Cryptography;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Xml.Linq;
using System.Web;
using System.Globalization;

namespace AMSExplorer
{

    class DynamicEncryption
    {
        
        /// <summary>
        /// Configures authorization policy. 
        /// Creates a content key. 
        /// Updates the PlayReady configuration XML file.
        /// </summary>
        /// <returns>The content key.</returns>
        public static IContentKey ConfigureKeyDeliveryServiceForPlayReady(CloudMediaContext _context, Guid keyId, byte[] keyValue, PlayReadyLicenseTemplate licenseTemplate,
            ContentKeyRestrictionType PlayReadyKeyRestriction, string PlayReadyPolicyName)
        {

            var contentKey = _context.ContentKeys.Create(keyId, keyValue, "key test", ContentKeyType.CommonEncryption);

            var restrictions = new List<ContentKeyAuthorizationPolicyRestriction>
                {
                    new ContentKeyAuthorizationPolicyRestriction { Requirements = null, Name = Enum.GetName(typeof(ContentKeyRestrictionType),PlayReadyKeyRestriction), 
                        KeyRestrictionType = (int)PlayReadyKeyRestriction }
                };

            IContentKeyAuthorizationPolicy contentKeyAuthorizationPolicy = _context.
                        ContentKeyAuthorizationPolicies.
                        CreateAsync("Deliver Common Content Key with no restrictions").
                        Result;

            // Configure PlayReady license template.

            PlayReadyLicenseResponseTemplate responseTemplate = new PlayReadyLicenseResponseTemplate();

            responseTemplate.LicenseTemplates.Add(licenseTemplate);

            string newLicenseTemplate = MediaServicesLicenseTemplateSerializer.Serialize(responseTemplate);

            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                PlayReadyPolicyName,
                    ContentKeyDeliveryType.PlayReadyLicense,
                        restrictions, newLicenseTemplate);

            contentKeyAuthorizationPolicy.Options.Add(policyOption);

            // Associate the content key authorization policy with the content key
            contentKey.AuthorizationPolicyId = contentKeyAuthorizationPolicy.Id;
            contentKey = contentKey.UpdateAsync().Result;

            return contentKey;
        }



        static public byte[] GetRandomBuffer(int size)
        {
            byte[] randomBytes = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        static public IContentKey CreateCommonTypeContentKey(IAsset asset, CloudMediaContext _context)
        {
            // Create envelope encryption content key
            Guid keyId = Guid.NewGuid();
            byte[] contentKey = GetRandomBuffer(16);

            IContentKey key = _context.ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    "ContentKey CENC",
                                    ContentKeyType.CommonEncryption);

            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }



        static public IContentKey CreateEnvelopeTypeContentKey(IAsset asset)
        {
            // Create envelope encryption content key
            Guid keyId = Guid.NewGuid();
            byte[] contentKey = GetRandomBuffer(16);

            IContentKey key = asset.GetMediaContext().ContentKeys.Create(
                                    keyId,
                                    contentKey,
                                    "ContentKey Envelope",
                                    ContentKeyType.EnvelopeEncryption);
            // Associate the key with the asset.
            asset.ContentKeys.Add(key);

            return key;
        }




        static public IContentKeyAuthorizationPolicy AddOpenAuthorizationPolicy(IContentKey contentKey, ContentKeyDeliveryType contentkeydeliverytype, string keydeliveryconfig, CloudMediaContext _context)
        {
            // Create ContentKeyAuthorizationPolicy with Open restrictions 
            // and create authorization policy             
            IContentKeyAuthorizationPolicy policy = _context.
                                    ContentKeyAuthorizationPolicies.
                                    CreateAsync("Open Authorization Policy").Result;

            List<ContentKeyAuthorizationPolicyRestriction> restrictions =
                new List<ContentKeyAuthorizationPolicyRestriction>();

            ContentKeyAuthorizationPolicyRestriction restriction =
                new ContentKeyAuthorizationPolicyRestriction
                {
                    Name = "Open Authorization Policy",
                    KeyRestrictionType = (int)ContentKeyRestrictionType.Open,
                    Requirements = null // no requirements needed 
                };

            restrictions.Add(restriction);

            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                "policy",
                contentkeydeliverytype,
                restrictions,
                keydeliveryconfig);

            policy.Options.Add(policyOption);

            // Add ContentKeyAutorizationPolicy to ContentKey
            contentKey.AuthorizationPolicyId = policy.Id;
            IContentKey updatedKey = contentKey.UpdateAsync().Result;
            return policy;
        }



        public static string AddTokenRestrictedAuthorizationPolicy(IContentKey contentKey, Uri Audience, Uri Issuer, CloudMediaContext _context)
        {
            string tokenTemplateString = GenerateTokenRequirements(Audience, Issuer);

            IContentKeyAuthorizationPolicy policy = _context.
                                    ContentKeyAuthorizationPolicies.
                                    CreateAsync("HLS token restricted authorization policy").Result;

            List<ContentKeyAuthorizationPolicyRestriction> restrictions =
                    new List<ContentKeyAuthorizationPolicyRestriction>();

            ContentKeyAuthorizationPolicyRestriction restriction =
                    new ContentKeyAuthorizationPolicyRestriction
                    {
                        Name = "Token Authorization Policy",
                        KeyRestrictionType = (int)ContentKeyRestrictionType.TokenRestricted,
                        Requirements = tokenTemplateString
                    };

            restrictions.Add(restriction);

            //You could have multiple options 
            IContentKeyAuthorizationPolicyOption policyOption =
                _context.ContentKeyAuthorizationPolicyOptions.Create(
                    "Token option for HLS",
                    ContentKeyDeliveryType.BaselineHttp,
                    restrictions,
                    null  // no key delivery data is needed for HLS
                    );

            policy.Options.Add(policyOption);

            // Add ContentKeyAutorizationPolicy to ContentKey
            contentKey.AuthorizationPolicyId = policy.Id;
            IContentKey updatedKey = contentKey.UpdateAsync().Result;
            Console.WriteLine("Adding Key to Asset: Key ID is " + updatedKey.Id);

            return tokenTemplateString;
        }


        static private string GenerateTokenRequirements(Uri _sampleAudience, Uri _sampleIssuer)
        {
            TokenRestrictionTemplate template = new TokenRestrictionTemplate();

            template.PrimaryVerificationKey = new SymmetricVerificationKey();
            template.AlternateVerificationKeys.Add(new SymmetricVerificationKey());
            template.Audience = _sampleAudience;
            template.Issuer = _sampleIssuer;
            template.RequiredClaims.Add(TokenClaim.ContentKeyIdentifierClaim);

            return TokenRestrictionTemplateSerializer.Serialize(template);
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyAES(IAsset asset, IContentKey key, AssetDeliveryProtocol assetdeliveryprotocol, string name, CloudMediaContext _context)
        {
            Uri keyAcquisitionUri = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.BaselineHttp);

            string envelopeEncryptionIV = Convert.ToBase64String(GetRandomBuffer(16));

            // The following policy configuration specifies: 
            //   key url that will have KID=<Guid> appended to the envelope and
            //   the Initialization Vector (IV) to use for the envelope encryption.
            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration =
                new Dictionary<AssetDeliveryPolicyConfigurationKey, string> 
            {
                {AssetDeliveryPolicyConfigurationKey.EnvelopeKeyAcquisitionUrl, keyAcquisitionUri.ToString()},
                {AssetDeliveryPolicyConfigurationKey.EnvelopeEncryptionIVAsBase64, envelopeEncryptionIV}
            };

            IAssetDeliveryPolicy assetDeliveryPolicy =
                _context.AssetDeliveryPolicies.Create(
                            name,
                            AssetDeliveryPolicyType.DynamicEnvelopeEncryption,
                            assetdeliveryprotocol,
                            assetDeliveryPolicyConfiguration);

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);
            return assetDeliveryPolicy;
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyNoDynEnc(IAsset asset, AssetDeliveryProtocol assetdeliveryprotocol, CloudMediaContext _context)
        {

            IAssetDeliveryPolicy assetDeliveryPolicy =
                _context.AssetDeliveryPolicies.Create(
                            "AssetDeliveryPolicy NoDynEnc",
                            AssetDeliveryPolicyType.NoDynamicEncryption,
                            assetdeliveryprotocol,
                            null); //  no dyn enc then no need for configuration

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);
            return assetDeliveryPolicy;
        }

        static public IAssetDeliveryPolicy CreateAssetDeliveryPolicyCENC(IAsset asset, IContentKey key, AssetDeliveryProtocol assetdeliveryprotocol, string name, CloudMediaContext _context)
        {
            Uri acquisitionUrl = key.GetKeyDeliveryUrl(ContentKeyDeliveryType.PlayReadyLicense);

            Dictionary<AssetDeliveryPolicyConfigurationKey, string> assetDeliveryPolicyConfiguration = new Dictionary<AssetDeliveryPolicyConfigurationKey, string>
    {
        {AssetDeliveryPolicyConfigurationKey.PlayReadyLicenseAcquisitionUrl, acquisitionUrl.ToString()},
    };

            var assetDeliveryPolicy = _context.AssetDeliveryPolicies.Create(
                name,
                AssetDeliveryPolicyType.DynamicCommonEncryption,
                assetdeliveryprotocol,
                assetDeliveryPolicyConfiguration);

            // Add AssetDelivery Policy to the asset
            asset.DeliveryPolicies.Add(assetDeliveryPolicy);

            return assetDeliveryPolicy;
        }



        static public string ConfigurePlayReadyLicenseTemplate(PlayReadyLicenseTemplate licenseTemplate)
        {
            // The following code configures PlayReady License Template using .NET classes
            // and returns the XML string.

            PlayReadyLicenseResponseTemplate responseTemplate = new PlayReadyLicenseResponseTemplate();

            responseTemplate.LicenseTemplates.Add(licenseTemplate);

            return MediaServicesLicenseTemplateSerializer.Serialize(responseTemplate);
        }



    }
}
