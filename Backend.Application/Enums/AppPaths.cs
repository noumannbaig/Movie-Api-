using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Enums
{
    public sealed class AppPath
    {
        private AppPath() { }
        public const string ProfileImagePath = "profile-image/";
        public const string BestFitPath = "bestfit/";
        public const string HousingBestFitPath = "housingbestfit/";
        public const string MatchingProductPath = "matchingproduct/";
        public const string HousingMatchingProductPath = "housingmatchingproduct/";
        public const string AdminFilesPath = "uploads/configurations";
        public const string DealProductPath = "dealproducts/";
        public const string BountyProductPath = "bountyproducts/";
        public const string StaticUniqueRequest = "staticuniquerequest/";
        public const string SystemUniqueRequest = "systemuniquerequest/";
        public const string DynamicUniqueRequest = "dynamicuniquerequest/";
        public const string DynamicSystemGraph = "dynamicsystemgraph/";
        public const string StaticFiles = "tem_sim_static_pdf_download/";
        public const string BusinessProfile = "businessprofile/";
        public const string S3BaseUrl = "https://ksc-storage.s3.eu-central-1.amazonaws.com/";
        public const string ImageBaseUrl = "https://ili.Backend.de/";
        public const string RewardFiles = "rewardfiles/";
        public const string BaseUrl = "https://backend.ili.Backend.de/";
    }
}
